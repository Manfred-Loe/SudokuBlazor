namespace SudokuLib.Solvers;

internal class WaveCollapse
{
	private readonly Stack<Choice> choices = new();
	private Choice previousChoice;
	int counter = 0;
	public WaveCollapse() { }

	public Sudoku Solve(Sudoku p)
	{
		previousChoice = new();

		while (!SudokuHelper.CheckIfFull(p))
		{
			SudokuHelper.RecalculateCandidates(p);
			if (CheckForEmptyNotes(p)) { StepBack(p); continue; }
			if (CheckForSingles(p)) { continue; }
			SetNumber(p, GetLowestEntropy(p));
		}
		return p;
	}


	public IEnumerable<Sudoku> SolveYield(Sudoku p)
	{
		previousChoice = new();

		while (!SudokuHelper.CheckIfFull(p))
		{
			counter++;
			SudokuHelper.RecalculateCandidates(p);
			if (CheckForEmptyNotes(p)) { StepBack(p); continue; }
			if (CheckForSingles(p)) { continue; }
			SetNumber(p, GetLowestEntropy(p));
			yield return p;
		}
		yield return p;
	}

	//checks the puzzle for any cells that have no notes - 
	//meaning invalid choice was made somewhere, step back afterwards
	private static bool CheckForEmptyNotes(Sudoku p)
	{

		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				if (p.Cells[x, y].Number == 0)
				{
					if (p.Cells[x, y].Candidates.Count == 0)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private bool CheckForSingles(Sudoku p)
	{
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				if (p.Cells[x, y].Number == 0 && p.Cells[x, y].Candidates.Count == 1)
				{
					SetNumber(p, new Choice(x, y, p.Cells[x, y].Candidates[0]));
					return true;
				}

			}
		}
		return false;
	}

	private static Choice GetLowestEntropy(Sudoku p)
	{
		Choice c = new();
		bool first = true;

		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				if (p.Cells[x, y].Number == 0)
				{
					//if first 0, save and continue;
					if (first)
					{
						c.X = x;
						c.Y = y;
						first = false;
						continue;
					}
					if (p.Cells[x, y].Candidates.Count < p.Cells[c.X, c.Y].Candidates.Count)
					{
						c.X = x;
						c.Y = y;
					}
				}
			}
		}
		return c;
	}

	private void SetNumber(Sudoku p, Choice c)
	{
		//go up to next number in the notes
		for (int i = 0; i < p.Cells[c.X, c.Y].Candidates.Count; i++)
		{
			//check if previous cell, if so make sure we increment from there
			//if not, start at the bottom
			if (c.X == previousChoice.X && c.Y == previousChoice.Y)
			{
				if (p.Cells[c.X, c.Y].Candidates[i] > previousChoice.Number)
				{
					Choice current = new(c.X, c.Y, p.Cells[c.X, c.Y].Candidates[i]);
					choices.Push(current);
					p.Cells[c.X, c.Y].Number = p.Cells[c.X, c.Y].Candidates[i];
					SudokuHelper.RecalculateCandidates(p);
					return;
				}
			}
			else
			{
				Choice current = new(c.X, c.Y, p.Cells[c.X, c.Y].Candidates[i]);
				choices.Push(current);
				p.Cells[c.X, c.Y].Number = p.Cells[c.X, c.Y].Candidates[i];
				SudokuHelper.RecalculateCandidates(p);
				return;
			}

		}
		StepBack(p);
	}

	private void StepBack(Sudoku p)
	{
		if (choices.Count != 0)
		{

			Choice stepBack = choices.Pop();
			previousChoice = stepBack;
			//reset last attempt back to 0
			p.Cells[stepBack.X, stepBack.Y].Number = 0;
		}
		else
		{
			Console.WriteLine($"Broken Puzzle, Counter: {counter}");
		}
	}


}
