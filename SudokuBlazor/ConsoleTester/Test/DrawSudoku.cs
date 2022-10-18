using SudokuLib.Puzzle;

namespace ConsoleTester.Test
{
    internal static class DrawSudoku
    {
        public static void Print(Sudoku s)
        {
            Console.WriteLine("+-----+-----+-----+");
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    int number = s.Cells[x, y].Number;
                    if (number == 0)
                    {
                        Console.Write("|.");
                    }
                    else
                    {
                        Console.Write($"|{number}");
                    }
                }
                Console.WriteLine("|");
                if (y % 3 == 2)
                {
                    Console.WriteLine("+-----+-----+-----+");
                }
            }
        }
    }
}