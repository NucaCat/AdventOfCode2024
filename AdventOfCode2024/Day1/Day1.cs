using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day1;

public sealed class Day1
{
    public static int Distance()
    {
        var (leftList, rightList) = InitialPopulateLists();

        var distance = leftList.OrderBy()
            .Zip(rightList.OrderBy())
            .Aggregate(0, (accumulated, tuple) => accumulated + Math.Abs(tuple.First - tuple.Second));

        return distance;
    }
    public static int SimilarityScore()
    {
        var (leftList, rightList) = InitialPopulateLists();

        var rightDictionary = rightList
            .GroupBy(u => u)
            .ToDictionary(u => u.Key, u => u.Count());

        var similarity = leftList
            .Select(u => rightDictionary.GetValueOrDefault(u) * u)
            .DefaultIfEmpty()
            .Sum();

        return similarity;
    }

    private static (List<int> leftList, List<int> rightList) InitialPopulateLists()
    {
        var lines = File.ReadLines("Day1/input.txt");

        var leftList = new List<int>(1000);
        var rightList = new List<int>(1000);
        foreach (var line in lines)
        {
            var splitted = line.Split("   ");
            leftList.Add(Convert.ToInt32(splitted[0]));
            rightList.Add(Convert.ToInt32(splitted[1]));
        }

        return (leftList, rightList);
    }
}