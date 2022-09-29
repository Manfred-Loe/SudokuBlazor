using SudokuLib;
using SudokuLib.API;
using SudokuLib.Puzzle;

namespace ConsoleTester.Test;

internal class SudokuTester
{

	public SudokuTester() { }

	public void Run()
	{
		SudokuFactory sudokuFetcher = new SudokuFactory();
		Sudoku s = sudokuFetcher.GetRandomSudoku();
		Solver solver = new Solver();
		DrawSudoku.Print(s);
		s = solver.Backtrace(s);
		DrawSudoku.Print(s);

	}



}
