namespace SudokuLib.Utils;

// currently public class - needs API,
// possibly make a web page that shows the generation process?
public static class SudokuCreator
{
    public static Sudoku CreateRandFilledBoard()
    {
        Sudoku p = new();
        SudokuHelper.RecalculateCandidates(p);
        Random r = new();
        int failCount = 0;
        while (!SudokuHelper.CheckIfFull(p))
        {
            //puzzle should be valid going into this loop

            int cell = r.Next(81);
            int cellX = cell % 9;
            int cellY = cell / 9;

            //randomize the sudoku
            //p = SudokuGenerator.RandomizeSudoku(p);
            SudokuHelper.RecalculateCandidates(p);

            //check if that cell has a value already.. if it does continue
            if (p.Cells[cellX, cellY].Number != 0) { continue; }
            //if cell is 0, clone puzzle and plug in new random number
            Sudoku clone = SudokuManipulator.Clone(p);
            if (p.Cells[cellX, cellY].Candidates.Count == 1)
            {
                clone.Cells[cellX, cellY].Number = p.Cells[cellX, cellY].Candidates[0];
            }
            else
            {
                clone.Cells[cellX, cellY].Number = p.Cells[cellX, cellY].Candidates[r.Next(1, p.Cells[cellX, cellY].Candidates.Count)];
            }

            //check if new puzzle has at least one viable solution (validate)
            if (Validate(clone))
            {
                p = clone;
            }
            else
            {
                failCount++;
            }
            if (failCount >= 50)
            {
                WaveCollapse wc = new();
                return wc.Solve(p);
            }
        }
        return p;
    }
    internal static bool Validate(Sudoku p)
    {
        Backtrace backtrace = new();
        Sudoku temp = SudokuManipulator.Clone(p);
        temp = backtrace.Solve(temp);
        return SudokuHelper.CheckIfFull(temp) && SudokuHelper.ValidatePuzzle(temp);
    }

    public static Sudoku CreateSudoku(Sudoku p, int targetClueCount, int logID)
    {
        var f = new StreamWriter($"task{logID}.log", true);
        Random r = new();
        MultiChecker mc = new();
        int counter = 0;
        while (counter < 50)
        {
            //puzzle should be valid going into this loop
            int cell = r.Next(81);
            int cellX = cell % 9;
            int cellY = cell / 9;
            //check if that cell has already 0. if it is, continue;
            if (p.Cells[cellX, cellY].Number == 0) { continue; }
            //if cell is 0, clone puzzle and plug in new random number
            Sudoku clone = SudokuManipulator.Clone(p);
            clone.Cells[cellX, cellY] = new(0);
            if (mc.CheckSolutions(clone, 2) <= 1)
            {
                f.WriteLine($"Found a number. New Sudoku: {clone.ToString}");
                p = clone;
                if (SudokuHelper.CountClues(p, false) <= targetClueCount) { return p; }
                counter = 0;
                continue;
            }
            counter++;
        }

        return p;
    }
}
