using AdventOfCode2024.Common;

namespace AdventOfCode2024.Day3.Parsers;

internal sealed class NumberParser
{
    public static int? TryParseNumber(string line, ref int index)
    {
        var number = new List<char>();
        for (;;)
        {
            var character = line.GetWithoutMove(index);
            if (character is >= '0' and <= '9')
            {
                number.Add(character.Value);
                index++;
            }
            else break;
        }

        if (number.IsEmpty())
            return null;

        return Convert.ToInt32(number.JoinToString(string.Empty));
    }
}