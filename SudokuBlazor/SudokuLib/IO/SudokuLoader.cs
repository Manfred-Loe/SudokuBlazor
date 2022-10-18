namespace SudokuLib.IO;

public class SudokuLoader
{
    private List<Sudoku> _puzzles;
    internal SudokuLoader() { _puzzles = new(); }
    internal SudokuLoader(List<Sudoku> puzzles)
    {
        _puzzles = puzzles;
    }
    internal Sudoku GetRandomPuzzle()
    {
        Random rand = new();
        int puzzleIndex = rand.Next(0, _puzzles.Count);
        return SudokuManipulator.RandomizeSudoku(_puzzles[puzzleIndex]);
    }

    internal void LoadSudokusFromFile(string filelocation)
    {
        var reader = new SudokuReader(filelocation);
        _puzzles.Clear();
        _puzzles = reader.GetSudokus();
    }

    internal void ConvertFileList(string s)
    {
        string[] read = s.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in read)
        {
            _puzzles.Add(SudokuReader.ConvertString(line));
        }
    }
    /// <summary>
    /// Converts a string to a Sudoku Puzzle
    /// </summary>
    /// <param name="s">string format must be 81 chars long only numbers</param>
    /// <returns>Sudoku</returns>
    public static Sudoku SudokuFromString(string s)
    {
        int[,] sudoku = new int[9, 9];
        for (int i = 0; i < 81; i++)
        {
            int x = i % 9;
            int y = i / 9;
            sudoku[x, y] = Int32.Parse(s[i].ToString());
        }
        return new Sudoku(sudoku);
    }
}
