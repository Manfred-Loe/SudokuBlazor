﻿using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using SudokuLib.IO;
using SudokuLib.Puzzle;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;

namespace MudSudoku.Pages;

public partial class SudokuSolver
{
	[Parameter]
	public int UpdateFrequency { get; set; } = 10;
	[Parameter]
	public bool UpdateEvery { get; set; } = false;
	[Parameter]
	public string? InputSudoku { get; set; }
	public bool LoadedSudoku { get; set; } = false;

	Sudoku _sudoku = new();
	string _message = string.Empty;
	int fullcounter = 0;

	protected async override Task OnParametersSetAsync()
	{
		//setup file for new puzzles for webapp
		var s = await Http.GetStringAsync("sudokus.txt");
		SudokuFactory.ConvertFromList(s);

		//initialize sudoku
		if (InputSudoku != null)
		{
			if (ValidInputSudoku(InputSudoku))
			{
				_sudoku = SudokuFactory.ConvertFromString(InputSudoku);
				LoadedSudoku = true;
			}

			
		}
		else if(InputSudoku == null || !LoadedSudoku)
		{
			_sudoku = SudokuFactory.GetRandomSudoku();
			LoadedSudoku = true;
		}
		StateHasChanged();
	}
	private bool ValidInputSudoku(string s)
	{
		var regex = new Regex(@"[0-9]{81}");
		if (regex.IsMatch(s)) { return true; }
		return false;
	}
	public void LoadNewPuzzle()
	{
		_sudoku = SudokuFactory.GetRandomSudoku();
	}
	public async void SolveBacktrace()
	{
		int counter = 0;
		if (UpdateEvery) { UpdateFrequency = 1; }

		if (!SudokuUtils.ValidateSudoku(_sudoku))
		{
			_message = "Invalid Sudoku Entered";
		}

		foreach (var puzzle in Solver.YieldBacktrace(_sudoku))
		{
			counter++;
			fullcounter++;
			_sudoku = puzzle;
			if (counter == UpdateFrequency)
			{
				counter = 0;
				StateHasChanged();
				await Task.Delay(1);
			}
		}
		_message = $"Total Steps: {fullcounter}";
		StateHasChanged();
	}
	public async void SolveWaveCollapse()
	{
		int counter = 0;
		if (UpdateEvery) { UpdateFrequency = 1; }

		foreach (var puzzle in Solver.YieldWaveCollapse(_sudoku))
		{
			counter++;
			fullcounter++;
			_sudoku = puzzle;
			if (counter == UpdateFrequency)
			{
				counter = 0;
				StateHasChanged();
				await Task.Delay(1);
			}
		}
		_message = $"Total Steps: {fullcounter}";
		StateHasChanged();
	}
	public async void PopulateNotesAsync()
	{
		await Task.Run(() =>
		{
			SudokuUtils.RecalculateCandidates(_sudoku);
		});
		StateHasChanged();
	}
	private string GetBorderStyle(int x, int y)
	{
		string borderstyle = "d-flex align-center justify-center";
		if (x == 0 || x % 3 == 0)
		{
			borderstyle += " border-l-2 ml-1";
		}
		if (y == 0 || y % 3 == 0)
		{
			borderstyle += " border-t-2 mt-1";
		}
		if ((y + 1) % 3 == 0)
		{
			borderstyle += " border-b-2 mb-1";
		}
		if ((x + 1) % 3 == 0)
		{
			borderstyle += " border-r-2 mr-2";
		}
		return borderstyle;
	}



}
