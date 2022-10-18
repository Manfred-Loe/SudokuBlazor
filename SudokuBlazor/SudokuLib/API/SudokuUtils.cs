namespace SudokuLib.API;

public static class SudokuUtils
{
    //Checks to see if the sudoku is valid. If a valid sudoku, returns true. If not, returns false.
    public static bool ValidateSudoku(Sudoku s)
    {
        return SudokuHelper.ValidatePuzzle(s);
    }

    public static bool ValidateSingleSolution(Sudoku s)
    {
        return SudokuHelper.TrueValidation(s, out var _, 2);
    }
    public static int CountSolutions(Sudoku s, int solutionTarget = 0)
    {
        SudokuHelper.TrueValidation(s, out int solutions, solutionTarget);
        return solutions;
    }

    public static int CountClues(Sudoku s)
    {
        return SudokuHelper.CountClues(s, false);
    }
    public static int CountClues(Sudoku s, bool originalCluesOnly)
    {
        return SudokuHelper.CountClues(s, originalCluesOnly);
    }
    public static void PopulateCandidates(Sudoku s)
    {
        SudokuHelper.PopulateCandidates(s);
    }
    public static void ClearCandidates(Sudoku s)
    {
        SudokuHelper.ClearCandidates(s);
    }
    public static void RecalculateCandidates(Sudoku s)
    {
        SudokuHelper.RecalculateCandidates(s);
    }
    public static bool CheckIfFull(Sudoku s)
    {
        return SudokuHelper.CheckIfFull(s);
    }
    public static Sudoku RandomizeSudoku(Sudoku s)
    {
        return SudokuManipulator.RandomizeSudoku(s);
    }
    public static Sudoku Clone(Sudoku s)
    {
        return SudokuManipulator.Clone(s);
    }
}
