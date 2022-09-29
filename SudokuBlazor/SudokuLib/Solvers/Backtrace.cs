namespace SudokuLib.Solvers;

internal class Backtrace
{
	private readonly Stack<Choice> choices = new();
	internal Backtrace(){ }

	//these are pure bruteforce backtraces. Not quite as efficient, but an example nonetheless
	internal Sudoku Solve(Sudoku p)
	{
		choices.Clear();
		int counter = 0;
		int n = 1;
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				counter++;
				//check for 0s (a.k.a. unfilled)
				if (p.Cells[x, y].Number == 0 && n <= 9)
				{
					choices.Push(new Choice(x, y, n));
					p.Cells[x, y].Number = n;
					//check if invalid after and step it back one
					if (!SudokuHelper.ValidatePuzzle(p))
					{
						Choice tempChoice = choices.Pop();
						p.Cells[tempChoice.X, tempChoice.Y].Number = 0;
						n++;
						x--;
					}
					else
					{
						n = 1;
					}
				}
				else if (n > 9 && choices.Count > 0)
				{
					Choice stepBack = choices.Pop();
					n = stepBack.Number + 1;
					p.Cells[stepBack.X, stepBack.Y].Number = 0;
					x = stepBack.X - 1;
					y = stepBack.Y;
				}
			}
		}
		return p;
	}

	//enumerable to get individual steps
	internal IEnumerable<Sudoku> SolveYield(Sudoku p)
	{
		choices.Clear();
		int counter = 0;
		int n = 1;
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				counter++;
				//check for 0s (a.k.a. unfilled)
				if (p.Cells[x, y].Number == 0 && n <= 9)
				{
					choices.Push(new Choice(x, y, n));
					p.Cells[x, y].Number = n;
					//check if invalid after and step it back one
					if (!SudokuHelper.ValidatePuzzle(p))
					{
						Choice tempChoice = choices.Pop();
						p.Cells[tempChoice.X, tempChoice.Y].Number = 0;
						n++;
						x--;
					}
					else
					{
						n = 1;
					}
				}
				else if (n > 9 && choices.Count > 0)
				{
					Choice stepBack = choices.Pop();
					n = stepBack.Number + 1;
					p.Cells[stepBack.X, stepBack.Y].Number = 0;
					x = stepBack.X - 1;
					y = stepBack.Y;
				}
				yield return p;
			}
		}
		yield return p;
	}


}
