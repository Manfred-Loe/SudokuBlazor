namespace SudokuLib.API;

public class SudokuFactory
{
    readonly SudokuLoader loader;

    public SudokuFactory()
    {
        loader = new SudokuLoader();
    }

    public void LoadSudokusFromFile(string fileLocation = "validatedSudokus.txt")
    {
        loader.LoadSudokusFromFile(fileLocation);
    }
    public static void SaveSudoku(Sudoku s, string sudokuFile = "possiblesudokus.txt")
    {
        SaveSudoku(s.ToString(), sudokuFile);
    }
    public static void SaveSudoku(string sudoku, string sudokuFile = "possiblesudokus.txt")
    {
        var sudokuWriter = new SudokuWriter(sudokuFile);
        sudokuWriter.WriteSudoku(sudoku);
    }
    public Sudoku GetRandomSudoku()
    {
        return loader.GetRandomPuzzle();
    }
    public void ConvertFromList(string listOfSudokus)
    {
        loader.ConvertFileList(listOfSudokus);
    }
    public static Sudoku ConvertFromString(string sudoku)
    {
        return SudokuLoader.SudokuFromString(sudoku);
    }

    //ToDo: Get puzzle based on difficulty

    //ToDo: SudokuCreator API -> Weird with WebApp vs Local

}
