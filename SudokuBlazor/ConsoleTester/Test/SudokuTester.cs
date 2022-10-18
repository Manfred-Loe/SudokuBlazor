using SudokuLib;
using SudokuLib.API;
using SudokuLib.Puzzle;

namespace ConsoleTester.Test;

internal class SudokuTester
{
    public SudokuTester() { }

    public static void Run()
    {
        var sudokuFetcher = new SudokuFactory();
        sudokuFetcher.LoadSudokusFromFile();
        Sudoku s = sudokuFetcher.GetRandomSudoku();
        var solver = new Solver();
        DrawSudoku.Print(s);
        s = solver.Backtrace(s);
        DrawSudoku.Print(s);
    }
}
