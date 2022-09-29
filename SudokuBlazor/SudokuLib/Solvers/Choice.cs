namespace SudokuLib.Solvers;

internal struct Choice
{
	internal int X { get; set; }
	internal int Y { get; set; }
	internal int Number { get; }

	internal Choice(int x, int y, int number)
	{
		X = x;
		Y = y;
		Number = number;
	}
}
