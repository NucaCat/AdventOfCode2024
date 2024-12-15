namespace AdventOfCode2024.Day6;

public sealed class Day6
{
    public int CountOfDistinctPositionsOfPatrolPath(IEnumerable<string>? overrideValues = null)
    {
        var grid = InitialParse(overrideValues);

        var path = PatrolPath(grid);
        var countOfDistinctPositions = path.Distinct().Count();

        return countOfDistinctPositions;
    }

    private IEnumerable<(int Row, int Column)> PatrolPath(char[][] grid)
    {
        var currentStep = StartPosition(grid);
        var path = new List<(int, int)>([(currentStep.Row, currentStep.Column)]);

        for (;;)
        {
            currentStep = MakeMove(currentStep, grid);
            if (OutOfBounds((currentStep.Row, currentStep.Column), grid))
                break;
            
            path.Add((currentStep.Row, currentStep.Column));
        }

        return path;
    }

    private (int Row, int Column, Direction) MakeMove((int Row, int Column, Direction Direction) currentStep, char[][] grid)
    {
        var nextStep = NextStep(currentStep);
        if (OutOfBounds((nextStep.Row, nextStep.Column), grid))
            return nextStep;
        
        if (grid[nextStep.Row][nextStep.Column] == '#')
            currentStep.Direction = TurnRight(currentStep.Direction);

        nextStep = NextStep(currentStep);
        return nextStep;
    }

    private (int Row, int Column, Direction Direction) NextStep((int Row, int Column, Direction Direction) currentStep)
        => currentStep.Direction switch
        {
            Direction.Up => (currentStep.Row - 1, currentStep.Column, currentStep.Direction),
            Direction.Down => (currentStep.Row + 1, currentStep.Column, currentStep.Direction),
            Direction.Left => (currentStep.Row, currentStep.Column - 1, currentStep.Direction),
            Direction.Right => (currentStep.Row, currentStep.Column + 1, currentStep.Direction)
        };

    private Direction TurnRight(Direction direction)
        => direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            Direction.Right => Direction.Down
        };

    private bool OutOfBounds((int Row, int Column) point, char[][] grid)
        => point.Row == -1 || point.Column == -1 || point.Row == grid.Length || point.Column == grid[0].Length; 

    private static (int Row, int Column, Direction ) StartPosition(char[][] grid)
    {
        return (from row in grid.Index()
            from character in row.Item.Index()
            where character.Item == '^'
            select (row.Index, character.Index, Direction.Up)).First();
    }

    private char[][] InitialParse(IEnumerable<string>? overrideValues)
        => (overrideValues ?? File.ReadLines("Day6/input.txt")).Select(u => u.ToCharArray()).ToArray();
    
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}