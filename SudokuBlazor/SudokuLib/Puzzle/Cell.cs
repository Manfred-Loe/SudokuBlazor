namespace SudokuLib.Puzzle;

public class Cell
{
	public int Number { get; set; }
	public List<int> Candidates { get; set; }
	public bool Clue { get; private set; }

	public Cell() : this(0, new() { 0, 0, 0, 0, 0, 0, 0, 0, 0 }) { }

	public Cell(int number, List<int> candidates)
	{
		Number = number;
		Candidates = candidates;
		Clue = number != 0;
	}

	public Cell(int number) : this(number, new() { 0, 0, 0, 0, 0, 0, 0, 0, 0 }) { }
}
