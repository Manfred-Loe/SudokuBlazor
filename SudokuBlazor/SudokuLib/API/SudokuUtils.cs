namespace SudokuLib.API;

public class SudokuUtils
{
	//Checks to see if the sudoku is valid. If a valid sudoku, returns true. If not, returns false.
	public bool ValidateSudoku(Sudoku s)
	{
		return SudokuHelper.ValidatePuzzle(s);
	}

	public bool ValidateSingleSolution(Sudoku s)
	{

		return SudokuHelper.TrueValidation(s, out var _ , 2);
	}
	public int CountSolutions(Sudoku s, int solutionTarget = 0)
	{
		SudokuHelper.TrueValidation(s, out int solutions, solutionTarget);
		return solutions;
	}

	public int CountClues(Sudoku s)
	{
		return SudokuHelper.CountClues(s, false);
	}
	public int CountClues(Sudoku s, bool originalCluesOnly)
	{
		return SudokuHelper.CountClues(s, originalCluesOnly);
	}
	public void PopulateCandidates(Sudoku s)
	{
		SudokuHelper.PopulateCandidates(s);
	}
	public void ClearCandidates(Sudoku s)
	{
		SudokuHelper.ClearCandidates(s);
	}
	public void RecalculateCandidates(Sudoku s)
	{
		SudokuHelper.RecalculateCandidates(s);
	}
	public bool CheckIfFull(Sudoku s)
	{
		return SudokuHelper.CheckIfFull(s);
	}
	public Sudoku RandomizeSudoku(Sudoku s)
	{
		return SudokuManipulator.RandomizeSudoku(s);
	}
	public Sudoku Clone(Sudoku s)
	{
		return SudokuManipulator.Clone(s);
	}


}
