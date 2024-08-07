using System.Diagnostics;

namespace StringEnums.Tests;

public class Perf(Action<string> write)
{
    private readonly Action<string> Write = write;

    private static double MeasureTicks(Action action)
    {
        long counter = 1L;
        Stopwatch sw = new();
        action(); // prime
        sw.Start();
        do
        {
            action();
            counter++;
        } while (sw.ElapsedMilliseconds < 100);
        sw.Stop();
        return sw.ElapsedTicks / (double)counter;
    }

    public void MeasureRate(Action action, string label)
    {
        double frequency = Stopwatch.Frequency / MeasureTicks(action);
        Write($"{frequency,11:####,###} {label}");
    }

    public void MeasureDuration(Action action, long iterations, string label)
    {
        long ticks = (long)(MeasureTicks(action) * iterations);
        TimeSpan ts = TimeSpan.FromTicks(ticks);
        Write($"{ts} {label}");
    }
}
