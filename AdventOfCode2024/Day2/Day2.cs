namespace AdventOfCode2024.Day2;

public sealed class Day2
{
    private const int _minDifference = 1;
    private const int _maxDifference = 3;

    public int SafeReportsCountV1()
    {
        var reports = InitialPopulateReports();

        var safeReportsCount = reports.Count(IsSafe);
        return safeReportsCount;
    }

    public int SafeReportsCountV1WithMethodFromV2()
    {
        var reports = InitialPopulateReports();

        var safeReportsCount = reports.Count(u => IsSafe(u, badLevelsToleranceCount: 0));
        return safeReportsCount;
    }

    public int SafeReportsCountV2(IEnumerable<string>? overrideValues = null)
    {
        var reports = InitialPopulateReports(overrideValues);

        var safeReportsCount = reports.Count(u => IsSafe(u, badLevelsToleranceCount: 1));
        return safeReportsCount;
    }

    public List<Report> DumpUnsafeReportsCountV2(IEnumerable<string>? overrideValues = null)
    {
        var reports = InitialPopulateReports(overrideValues);

        var unsafeReports = reports.Where(u => !IsSafe(u, badLevelsToleranceCount: 1)).ToList();
        return unsafeReports;
    }

    private bool IsSafe(Report report)
    {
        var difference = report.Levels.Zip(report.Levels.Skip(1))
            .Select(u => u.Second - u.First);
        var isSafe = difference.All(u => u < 0 && Math.Abs(u) is >= _minDifference and <= _maxDifference)
            || difference.All(u => u > 0 && Math.Abs(u) is >= _minDifference and <= _maxDifference);
        return isSafe;
    }

    private bool IsSafe(Report report, int badLevelsToleranceCount)
    {
        return IsSafeAscendingWithBadLevelTolerance(report, badLevelsToleranceCount)
            || IsSafeDescendingWithBadLevelTolerance(report, badLevelsToleranceCount)
            // handle for first is incorrect. Try to remove it
            || IsSafeAscendingWithBadLevelTolerance(new Report(Levels: report.Levels.Skip(1).ToList()), badLevelsToleranceCount - 1)
            || IsSafeDescendingWithBadLevelTolerance(new Report(Levels: report.Levels.Skip(1).ToList()), badLevelsToleranceCount - 1);
    }

    private static bool IsSafeAscendingWithBadLevelTolerance(Report report, int badLevelsToleranceCount)
        => IsSafeWithBadLevelTolerance(report, badLevelsToleranceCount, u => u > 0);

    private static bool IsSafeDescendingWithBadLevelTolerance(Report report, int badLevelsToleranceCount)
        => IsSafeWithBadLevelTolerance(report, badLevelsToleranceCount, u => u < 0);

    private static bool IsSafeWithBadLevelTolerance(Report report, int badLevelsToleranceCount, Func<int, bool> orderFunc)
    {
        var previous = report.Levels.First();

        foreach (var current in report.Levels.Skip(1))
        {
            var difference = current - previous;
            if (orderFunc(difference) && Math.Abs(difference) is >= _minDifference and <= _maxDifference)
            {
                previous = current;
                continue;
            }

            if (badLevelsToleranceCount <= 0) 
                return false;

            badLevelsToleranceCount--;
        }

        return true;
    }

    private static List<Report> InitialPopulateReports(IEnumerable<string>? overrideValues = null)
    {
        var lines = overrideValues ?? File.ReadLines("Day2/input.txt");

        var reports = new List<Report>(1000);
        foreach (var line in lines)
        {
            var splitted = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var levels = splitted.Select(u => Convert.ToInt32(u)).ToList();
            reports.Add(new Report(levels));
        }

        return reports;
    }
}