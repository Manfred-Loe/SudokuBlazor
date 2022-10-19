namespace SudokuLib.Solvers;

internal class Natural
{
	public int DifficultyScore { get; private set; }
	public Natural() { DifficultyScore = 0; }
	public Sudoku Solve(Sudoku s)
	{
		DifficultyScore = 0;
		//calculate candidates - should only need once
		//should be removing candidates as we go
		SudokuHelper.RecalculateCandidates(s);

		while (!SudokuHelper.CheckIfFull(s))
		{
			//check for singles and fill (cells with only one candidate)
			//check for simple things (pinned/last number in row etc)
			//check for naked candidates / naked pairs
			//check for hidden candidates / hidden pairs
			//intersection removal
			//X wing
			//Singles Chains
			//Y Wing
			//Swordfish
			//XYZ-Wing
			//BUG
			//Avoidable Rectangles
			if (Singles(s)) { SudokuHelper.RecalculateCandidates(s); continue; }
			if (Simple(s)) { continue; }
			if (Nakeds(s)) { continue; }
			if (Hidden(s)) { continue; }
			if (Intersections(s)) { continue; }
			if (XWing(s)) { continue; }
			if (SinglesChains(s)) { continue; }
			if (YWing(s)) { continue; }
			if (Swordfish(s)) { continue; }
			if (XYZWing(s)) { continue; }
			if (BUG(s)) { continue; }
			if (AvoidableRectangles(s)) { continue; }
		}
		return s;
	}
	//Simple checks for 
	private bool Simple(Sudoku s)
	{
		return true;
	}
	//singles checks for any cells with only one candidate and fills them
	private bool Singles(Sudoku s)
	{
		bool filled = false;
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				if (s.Cells[x, y].Number == 0 && s.Cells[x, y].Candidates.Count == 1)
				{
					s.Cells[x, y].Number = s.Cells[x, y].Candidates[0];
					filled = true;
					DifficultyScore--;
				}
			}
		}
		return filled;
	}
	private bool Nakeds(Sudoku s)
	{
		return true;
	}
	private bool Hidden(Sudoku s)
	{
		return true;
	}
	private bool Intersections(Sudoku s)
	{
		return true;
	}
	private bool XWing(Sudoku s)
	{
		return true;
	}
	private bool SinglesChains(Sudoku s)
	{
		return true;
	}
	private bool YWing(Sudoku s)
	{
		return true;
	}
	private bool Swordfish(Sudoku s)
	{
		return true;
	}
	private bool XYZWing(Sudoku s)
	{
		return true;
	}
	private bool BUG(Sudoku s)
	{
		return true;
	}
	private bool AvoidableRectangles(Sudoku s)
	{
		return true;
	}
}
