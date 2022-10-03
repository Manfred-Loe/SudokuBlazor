namespace SudokuLib.Utils;

internal static class SudokuHelper
{
	public static bool ValidatePuzzle(Sudoku s)
	{
		for (int i = 0; i < 9; i++)
		{
			if (!ValidateRow(s, i)) { return false; }
			else if (!ValidateCol(s, i)) { return false; }
			else if (!ValidateBlock(s, i + 1)) { return false; }
		}
		return true;
	}

	internal static bool ValidateRow(Sudoku s, int r)
	{
		List<int> _values = new();
		for (int i = 0; i < 9; i++)
		{
			if (s.Cells[r, i].Number != 0 && _values.Contains(s.Cells[r, i].Number)) { return false; }
			else
			{
				if (s.Cells[r, i].Number != 0)
				{
					_values.Add(s.Cells[r, i].Number);
				}
			}
		}
		return true;
	}

	internal static bool ValidateCol(Sudoku s, int c)
	{
		List<int> _values = new();
		for (int i = 0; i < 9; i++)
		{
			if (s.Cells[i, c].Number != 0 && _values.Contains(s.Cells[i, c].Number)) { return false; }
			else
			{
				if (s.Cells[i, c].Number != 0)
				{
					_values.Add(s.Cells[i, c].Number);
				}
			}
		}
		return true;
	}

	internal static bool ValidateBlock(Sudoku s, int b)
	{
		int xMod = 0;
		int yMod = 0;

		switch (b)
		{
			case 1: break;
			case 2: yMod = 3; break;
			case 3: yMod = 6; break;
			case 4: xMod = 3; break;
			case 5: xMod = 3; yMod = 3; break;
			case 6: xMod = 3; yMod = 6; break;
			case 7: xMod = 6; break;
			case 8: xMod = 6; yMod = 3; break;
			case 9: xMod = 6; yMod = 6; break;
			default: break;
		}

		List<int> _values = new();
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				if (s.Cells[x + xMod, y + yMod].Number != 0 && _values.Contains(s.Cells[x + xMod, y + yMod].Number)) { return false; }
				else
				{
					if (s.Cells[x + xMod, y + yMod].Number != 0)
					{
						_values.Add(s.Cells[x + xMod, y + yMod].Number);
					}
				}
			}
		}
		return true;
	}
	//True Validation returns true if there is only one solution, either way returns number of solutions
	public static bool TrueValidation(Sudoku s, out int realCount, int targetCount = 0)
	{
		realCount = 0;
		if (ValidatePuzzle(s))
		{
			MultiChecker mc = new();
			realCount = mc.CheckSolutions(s, targetCount);
			if(realCount == 1)
			{
				return true;
			}
		}
		return false;
	}


	public static bool NumberInRow(Sudoku p, int row, int n)
	{
		for (int y = 0; y < 9; y++)
		{
			if (p.Cells[row, y].Number == n)
			{
				return true;
			}
		}
		return false;
	}
	public static bool NumberInCol(Sudoku p, int col, int n)
	{
		for (int x = 0; x < 9; x++)
		{
			if (p.Cells[x, col].Number == n)
			{
				return true;
			}
		}
		return false;
	}
	public static bool NumberInBlock(Sudoku p, int b, int n)
	{
		int xMod = 0;
		int yMod = 0;

		switch (b)
		{
			case 1: break;
			case 2: yMod = 3; break;
			case 3: yMod = 6; break;
			case 4: xMod = 3; break;
			case 5: xMod = 3; yMod = 3; break;
			case 6: xMod = 3; yMod = 6; break;
			case 7: xMod = 6; break;
			case 8: xMod = 6; yMod = 3; break;
			case 9: xMod = 6; yMod = 6; break;
			default: break;
		}
		for (int x = 0; x < 3; x++)
		{
			for (int y = 0; y < 3; y++)
			{
				if (p.Cells[x + xMod, y + yMod].Number == n)
				{
					return true;
				}

			}
		}

		return false;
	}
	public static int DetermineBlock(int x, int y)
	{
		int tmpX = x / 3;
		int tmpY = y / 3;

		if (tmpX == 0)
		{
			if (tmpY == 0)
			{
				return 1;
			}
			else if (tmpY == 1)
			{
				return 2;
			}
			else if (tmpY == 2)
			{
				return 3;
			}
		}
		else if (tmpX == 1)
		{
			if (tmpY == 0)
			{
				return 4;
			}
			else if (tmpY == 1)
			{
				return 5;
			}
			else if (tmpY == 2)
			{
				return 6;
			}
		}
		else if (tmpX == 2)
		{
			if (tmpY == 0)
			{
				return 7;
			}
			else if (tmpY == 1)
			{
				return 8;
			}
			else if (tmpY == 2)
			{
				return 9;
			}
		}

		return -1;
	}

	public static int CountClues(Sudoku p, bool trueclues)
	{
		int n = 0;
		if (!trueclues)
		{
			foreach (var c in p.Cells)
			{
				if (c.Number > 0) { n++; }
			}
			return n; ;
		}
		else
		{
			foreach (var c in p.Cells)
			{
				if (c.Clue) { n++; }
			}
			return n;
		}
	}
	public static void PopulateCandidates(Sudoku p)
	{
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				if (p.Cells[x, y].Number == 0 && p.Cells[x, y].Clue == false)
				{
					for (int i = 1; i < 10; i++)
					{
						if (!SudokuHelper.NumberInRow(p, x, i) && !SudokuHelper.NumberInCol(p, y, i) && !SudokuHelper.NumberInBlock(p, SudokuHelper.DetermineBlock(x, y), i))
						{
							p.Cells[x, y].Number = i;
							if (SudokuHelper.ValidatePuzzle(p))
							{
								p.Cells[x, y].Candidates.Add(i);
							}
						}
						if (i == 9)
						{
							p.Cells[x, y].Number = 0;
						}
					}
				}
			}
		}
	}
	public static void ClearCandidates(Sudoku p)
	{
		foreach (var cell in p.Cells) { cell.Candidates.Clear(); }
	}

	public static void RecalculateCandidates(Sudoku p)
	{
		ClearCandidates(p);
		PopulateCandidates(p);
	}

	public static bool CheckIfFull(Sudoku p)
	{
		foreach (var cell in p.Cells) { if (cell.Number == 0) { return false; } }
		return true;
	}



}
