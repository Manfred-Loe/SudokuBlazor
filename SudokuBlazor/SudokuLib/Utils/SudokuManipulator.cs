namespace SudokuLib.Utils;

internal static class SudokuManipulator
{
	public static Sudoku RandomizeSudoku(Sudoku p)
	{
		Random r = new Random();
		int z = r.Next(20);
		for (int i = 0; i < z; i++)
		{
			int y = r.Next(5);
			switch (y)
			{
				case 0: p = Substitution(p); break;
				case 1: p = FlipXAxis(p); break;
				case 2: p = FlipYAxis(p); break;
				case 3: p = RotateClockwise(p); break;
				case 4: p = RotateCounterClockwise(p); break;
				default: break;
			}
		}

		return p;
	}
	public static Sudoku Substitution(Sudoku p)
	{
		Sudoku puzzle = new();
		char[,] c = new char[9, 9];
		int[] assignment = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

		for (int x = 0; x < 9; x++)
		{
			for (int y = 0; y < 9; y++)
			{
				c[x, y] = p.Cells[x, y].Number switch
				{
					1 => 'a',
					2 => 'b',
					3 => 'c',
					4 => 'd',
					5 => 'e',
					6 => 'f',
					7 => 'g',
					8 => 'h',
					9 => 'i',
					_ => '0',
				};
			}
		}

		Random rnd = new();
		//shuffle our assignment list
		int n = 9;
		while (n > 1)
		{
			int k = rnd.Next(n--);
			(assignment[k], assignment[n]) = (assignment[n], assignment[k]);
		}

		//reassign the chars to the now shuffled numbers
		for (int x = 0; x < 9; x++)
		{
			for (int y = 0; y < 9; y++)
			{
				puzzle.Cells[x, y] = c[x, y] switch
				{
					'a' => new Cell(assignment[0]),
					'b' => new Cell(assignment[1]),
					'c' => new Cell(assignment[2]),
					'd' => new Cell(assignment[3]),
					'e' => new Cell(assignment[4]),
					'f' => new Cell(assignment[5]),
					'g' => new Cell(assignment[6]),
					'h' => new Cell(assignment[7]),
					'i' => new Cell(assignment[8]),
					_ => new Cell(0),
				};
			}
		}
		return puzzle;
	}
	public static Sudoku FlipYAxis(Sudoku p)
	{
		Sudoku puzzle = new();
		for (int x = 0; x < 9; x++)
		{
			for (int y = 0; y < 9; y++)
			{
				puzzle.Cells[x, y] = p.Cells[x, 8 - y];
			}
		}
		return puzzle;
	}
	public static Sudoku FlipXAxis(Sudoku p)
	{
		Sudoku puzzle = new();
		for (int x = 0; x < 9; x++)
		{
			for (int y = 0; y < 9; y++)
			{
				puzzle.Cells[x, y] = p.Cells[8 - x, y];
			}
		}
		return puzzle;
	}
	public static Sudoku RotateClockwise(Sudoku p)
	{
		Sudoku puzzle = new();
		for (int x = 0; x < 9; x++)
		{
			for (int y = 0; y < 9; y++)
			{
				puzzle.Cells[y, 8 - x] = p.Cells[x, y];
			}
		}
		return puzzle;
	}
	//this is absolutely incorrect but hilarious so I'm leaving it
	public static Sudoku RotateCounterClockwise(Sudoku p)
	{
		return RotateClockwise(RotateClockwise(RotateClockwise(p)));
	}
	public static Sudoku Clone(Sudoku p)
	{
		int[,] values = new int[9, 9];
		for (int x = 0; x < 9; x++)
		{
			for (int y = 0; y < 9; y++)
			{
				values[x, y] = p.Cells[x, y].Number;
			}
		}
		return new Sudoku(values);
	}

}
