using System.Runtime.InteropServices;
using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day5;

public sealed class Day5
{
    public int SumOfMiddlePartOfCorrectUpdates(IEnumerable<string>? overrideValues = null)
    {
        var values = InitialParse(overrideValues);

        var correctUpdates = values.Updates
            .Where(u => IsCorrect(u, values.Rules))
            .ToArray();

        var sumOfMiddleparts = correctUpdates
            .Select(u => u[u.Count / 2])
            .SumOrEmpty();

        return sumOfMiddleparts;
    }
    public int SumOfMiddlePartOfCorrectedIncorrectUpdates(IEnumerable<string>? overrideValues = null)
    {
        var values = InitialParse(overrideValues);

        var correctUpdates = values.Updates
            .Where(u => !IsCorrect(u, values.Rules))
            .Select(u => Correct(u, values.Rules))
            .ToArray();

        var sumOfMiddleparts = correctUpdates
            .Select(u => u[u.Count / 2])
            .SumOrEmpty();

        return sumOfMiddleparts;
    }

    private List<int> Correct(List<int> update, Rules rules)
    {
        var list = new List<int>(update.Count);

        foreach (var page in update)
        {
            var beforeRule = rules.Before.GetValueOrDefault(page) ?? Enumerable.Empty<int>();

            var indexBefore = list.Index().Where(u => beforeRule.Contains(u.Item)).Select(u => u.Index).DefaultIfEmpty(-1).Min();
            if (indexBefore != -1)
            {
                list.Insert(indexBefore, page);
                continue;
            }

            var afterRule = rules.After.GetValueOrDefault(page) ?? Enumerable.Empty<int>();
            var indexAfter = list.Index().Where(u => afterRule.Contains(u.Item)).Select(u => u.Index + 1).DefaultIfEmpty(0).Max();
            list.Insert(indexAfter, page);
        }

        return list;
    }

    private bool IsCorrect(List<int> update, Rules rules)
    {
        foreach (var (index, page) in update.Index())
        {
            var pageIsCorrect = PageIsCorrect(page, CollectionsMarshal.AsSpan(update).Slice(index + 1), update, rules);
            if (!pageIsCorrect)
                return false;
        }

        return true;
    }

    private bool PageIsCorrect(int page, Span<int> slice, List<int> update, Rules rules)
    {
        if (slice.IsEmpty)
        {
            var rulesForPage = rules.Before.GetValueOrDefault(page);
            if (rulesForPage is not null && rulesForPage.Any(u => u != page && update.Contains(u)))
                return false;

            return true;
        }
        
        foreach (var nextPage in slice)
        {
            var rulesForPage = rules.Before.GetValueOrDefault(page);
            if (rulesForPage is not null && rulesForPage.All(u => u != nextPage))
                return false;
        }

        return true;
    }

    private Input InitialParse(IEnumerable<string>? overrideValues)
    {
        var rules = new Rules(new Dictionary<int, List<int>>(), new Dictionary<int, List<int>>());
        var updates = new List<List<int>>();
        
        var parsingMode = ParsingMode.Rules;
        foreach (var line in overrideValues ?? File.ReadLines("Day5/input.txt"))
        {
            if (line.IsNullOrWhiteSpace())
            {
                parsingMode = ParsingMode.Updates;
                continue;
            }

            if (parsingMode is ParsingMode.Rules)
                ParseRule(line, rules);
            
            if (parsingMode is ParsingMode.Updates)
                ParseUpdate(line, updates);
        }

        return new Input(rules, updates);
    }

    private void ParseRule(string line, Rules rules)
    {
        var numbers = line.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(u => Convert.ToInt32(u))
            .ToArray();

        rules.Before.TryAdd(numbers[0], []);
        rules.Before[numbers[0]].Add(numbers[1]);

        rules.After.TryAdd(numbers[1], []);
        rules.After[numbers[1]].Add(numbers[0]);
    }

    private void ParseUpdate(string line, List<List<int>> updates)
    {
        var numbers = line.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(u => Convert.ToInt32(u))
            .ToList();

        updates.Add(numbers);
    }

    private enum ParsingMode
    {
        Rules,
        Updates
    }

    private record Input(Rules Rules, List<List<int>> Updates);

    private record Rules(Dictionary<int, List<int>> After, Dictionary<int, List<int>> Before);
}