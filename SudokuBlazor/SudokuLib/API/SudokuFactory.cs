namespace SudokuLib.API;

public class SudokuFactory
{
	SudokuLoader loader;


	public SudokuFactory()
	{
		loader = new SudokuLoader();
	}

	public void LoadSudokusFromFile(string fileLocation = "validatedSudokues.txt")
	{
		loader.LoadSudokusFromFile(fileLocation);
	}
	public void SaveSudoku(Sudoku s,string sudokuFile = "possiblesudokus.txt")
	{
		SaveSudoku(s.ToString(),sudokuFile);
	}
	public void SaveSudoku(string sudoku, string sudokuFile = "possiblesudokus.txt")
	{
		SudokuWriter sudokuWriter = new SudokuWriter(sudokuFile);
		sudokuWriter.WriteSudoku(sudoku);
	}
	public  Sudoku GetRandomSudoku()
	{
		return loader.GetRandomPuzzle();
	}
	public void ConvertFromList(string listOfSudokus)
	{
		loader.ConvertFileList(listOfSudokus);
	}
	public Sudoku ConvertFromString(string sudoku)
	{
		return loader.SudokuFromString(sudoku);
	}

	//ToDo: Get puzzle based on difficulty

	//ToDo: SudokuCreator API -> Weird with WebApp vs Local

}
