namespace SudokuLib.Solvers;

internal class MultiChecker
{
	private readonly Stack<Choice> choices = new();

	public MultiChecker() { }

	public int CheckSolutions(Sudoku puzzle,int solutionTarget = 0)
	{
		choices.Clear();
		//n tracks which number we're plugging into the next spot
		int n = 1;
		int solutions = 0;
		Sudoku p = SudokuManipulator.Clone(puzzle);
		SudokuHelper.RecalculateCandidates(p);
		//loop through puzzle
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				//check for empty as long as n <=9
				if (p.Cells[x, y].Number == 0 && n <= 9) //why does it matter what n is here
				{
					//check if the notes contain n, if they do try n
					//if not, continue
					if (p.Cells[x, y].Candidates.Contains(n))
					{
						//push our "current" choice to the choices stack
						choices.Push(new Choice(x, y, n));
						//set cell to n
						p.Cells[x, y].Number = n;
					}
					else { n++; x--; continue; }

					//check if invalid after and step it back one if it's invalid
					bool validPuzzle = SudokuHelper.ValidatePuzzle(p);
					if (!validPuzzle)
					{
						Choice tempChoice = choices.Pop();
						p.Cells[tempChoice.X, tempChoice.Y].Number = 0;
						n++;
						x--;
					}
					else if (validPuzzle && SudokuHelper.CheckIfFull(p))
					{
						Choice tempChoice = choices.Pop();
						p.Cells[tempChoice.X, tempChoice.Y].Number = 0;
						n++;
						x--;
						solutions++;
						if (solutions >= solutionTarget && solutionTarget != 0)
						{
							return solutions;
						}
					}
					else if (validPuzzle && !SudokuHelper.CheckIfFull(p))
					{
						n = 1;
					}
				}
				else if (n > 9 && choices.Count > 0)
				{
					//make sure there's more than 
					Choice stepBack = choices.Pop();
					n = stepBack.Number + 1;
					p.Cells[stepBack.X, stepBack.Y].Number = 0;
					y = stepBack.Y;
					x = stepBack.X - 1;

				}
			}
		}
		return solutions;
	}


}
