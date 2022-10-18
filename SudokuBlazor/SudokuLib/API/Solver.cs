namespace SudokuLib.API;

public class Solver
{
    private readonly Backtrace _backtrace;
    private readonly WaveCollapse _waveCollapse;
    private readonly ImprovedBacktrace _improvedBacktrace;

    public Solver()
    {
        _backtrace = new Backtrace();
        _waveCollapse = new WaveCollapse();
        _improvedBacktrace = new ImprovedBacktrace();
    }

    public Sudoku Backtrace(Sudoku sudoku)
    {
        return _backtrace.Solve(sudoku);
    }

    public IEnumerable<Sudoku> YieldBacktrace(Sudoku sudoku)
    {
        foreach (var puzzle in _backtrace.SolveYield(sudoku))
        {
            yield return puzzle;
        }
    }
    public Sudoku ImprovedBacktrace(Sudoku sudoku)
    {
        return _improvedBacktrace.Solve(sudoku);
    }
    public IEnumerable<Sudoku> YieldImprovedBacktrace(Sudoku sudoku)
    {
        foreach (var puzzle in _improvedBacktrace.SolveYield(sudoku))
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
