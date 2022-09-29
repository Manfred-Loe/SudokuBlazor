namespace SudokuLib.Puzzle;

public class Sudoku
{
	public Cell[,] Cells;
	//difficulty rating - to be implemented
	public int Difficulty = 0;

	public Sudoku()
	{
		Cells = new Cell[9, 9];
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				Cells[x, y] = new Cell(0);
			}
		}
	}

	public Sudoku(Cell[,] cells)
	{
		Cells = cells;
	}

	public Sudoku(int[,] cells)
	{
		Cells = new Cell[9, 9];

		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				Cells[x, y] = new Cell(cells[x, y]);
			}
		}
	}

	public override string ToString()
	{
		string s = "";
		for(int y=0; y < 9; y++)
		{
			for(int x=0; x < 9; x++)
			{
				s += Cells[x, y].Number.ToString();
			}
		}
		return s;
	}

}
