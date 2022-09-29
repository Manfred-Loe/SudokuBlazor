namespace SudokuLib.IO;

internal class SudokuReader
{
	private readonly List<Sudoku> list;
	private readonly string filepath;

	internal SudokuReader(string file = "validatedsudokus.txt")
	{
		list = new List<Sudoku>();
		filepath = file;
	}

	internal List<Sudoku> GetSudokus()
	{
		list.Clear();
		foreach (string line in File.ReadLines(filepath))
		{
			list.Add(ConvertString(line));
		}
		return list;
	}

	internal static Sudoku ConvertString(string s)
	{
		List<Cell> cells = new();
		for (int i = 0; i < s.Length; i++)
		{
			int x = i;
			var value = int.Parse(s[x].ToString());
			cells.Add(new(value));
		}

		Sudoku puzzle = new Sudoku();
		int counter = 0;
		for (int y = 0; y < 9; y++)
		{
			for (int x = 0; x < 9; x++)
			{
				puzzle.Cells[x, y] = cells[counter];
				counter++;
			}
		}
		return puzzle;
	}




}
