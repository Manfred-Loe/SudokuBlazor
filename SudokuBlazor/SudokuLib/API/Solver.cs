namespace SudokuLib.API;

public class Solver
{
	private Backtrace _backtrace;
	private WaveCollapse _waveCollapse;


	public Solver() 
	{
		_backtrace = new Backtrace();
		_waveCollapse = new WaveCollapse();
	}

	public Sudoku Backtrace(Sudoku sudoku)
	{
		return _backtrace.Solve(sudoku);
	}

	public IEnumerable<Sudoku> YieldBacktrace(Sudoku sudoku)
	{
		foreach(var puzzle in _backtrace.SolveYield(sudoku))
		{
			yield return puzzle;
		}
	}

	public Sudoku WaveCollapse(Sudoku sudoku)
	{
		return _waveCollapse.Solve(sudoku);
	}

	public IEnumerable<Sudoku> YieldWaveCollapse(Sudoku sudoku)
	{
		foreach (var puzzle in _waveCollapse.SolveYield(sudoku))
		{
			yield return puzzle;
		}
	}


}
