using Microsoft.AspNetCore.Components;
using MudBlazor;
using SudokuLib.Puzzle;

namespace MudSudoku.Pages;

public partial class UserInputSudoku
{
	private bool _parameterSetFromOutside;
	private bool _typeMode = true;
	private Cell[,] _sudokuCells = new Cell[9, 9];
	[Parameter]
	public Cell[,] SudokuCells
	{
		get => _sudokuCells;
		set
		{
			_parameterSetFromOutside = true;
			_sudokuCells = value;
		}
	}
	protected override async Task OnParametersSetAsync()
	{
		if (!_parameterSetFromOutside)
		{
			InitializeCells();
		}
		await Task.Delay(100);
		StateHasChanged();
	}
	private void SubmitToSolver()
	{
		var sudoku = SudokuFactory.ConvertFromString(BuildString());
		if (!SudokuUtils.ValidateSingleSolution(sudoku))
		{
			InvalidBoard();
		}
		else
		{
			UriHelper.NavigateTo($"/solver/{sudoku}");
		}
	}
	private string BuildString()
	{
		string Sudoku = "";
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				Sudoku += SudokuCells[x, y].Number.ToString();
			}

		}
		Console.WriteLine($"Sudoku: {Sudoku}");
		return Sudoku;
	}

	private async void InvalidBoard()
	{
		await DialogService.ShowMessageBox("Warning", "Sudoku is invalid, please verify numbers are correct.", yesText: "Ok", cancelText: "Return");
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
	private void InitializeCells()
	{
		for (int y = 0; y < 9; y++)
		{
			int ytemp = y;

			for (int x = 0; x < 9; x++)
			{
				int xtemp = x;
				_sudokuCells[xtemp, ytemp] = new Cell(0);

			}
		}
	}
}
