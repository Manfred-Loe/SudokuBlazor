namespace SudokuLib.IO;

internal class SudokuWriter
{
    private readonly string filepath;

    internal SudokuWriter(string file = "validatedsudokus.txt")
    {
        filepath = file;
    }

    internal void WriteSudokus(List<Sudoku> list)
    {
        using StreamWriter file = new(filepath, true);
        foreach (Sudoku s in list)
        {
            string puzzle = s.ToString();
            file.WriteLine(puzzle);
        }
    }

    internal void WriteSudoku(Sudoku s)
    {
        WriteSudoku(s);
    }
    internal void WriteSudoku(string s)
    {
        using StreamWriter file = new(filepath, true);
        file.WriteLine(s);
        file.Close();
    }
}
