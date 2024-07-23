using System;
using System.Collections.Generic;

[Flags]
public enum WallState
{
    // Wall state flags
    Left = 1,    // 0001
    Right = 2,   // 0010
    Up = 4,      // 0100
    Down = 8,    // 1000
    Visited = 128 // 1000 0000
}

public struct Position
{
    public int X;
    public int Y;
}

public struct Neighbour
{
    public Position NeighbourPos;
    public WallState SharedWall;
}


public static class MazeGenerator
{
    // Generates a maze of specified width and height
    public static WallState[,] Generate(int width, int height)
    {
        // Initialize the maze with walls
        WallState[,] maze = new WallState[width, height];
        WallState initialWalls = WallState.Left | WallState.Right | WallState.Up | WallState.Down;

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                maze[i, j] = initialWalls; // Set all walls for each cell
            }
        }

        // Create a random exit on the right wall of the last column
        var random = new Random();
        int exitY = random.Next(0, height);
        maze[width - 1, exitY] &= ~WallState.Right;

        // Apply the recursive backtracker algorithm to generate the maze
        return ApplyRecursiveBacktracker(maze, width, height);
    }

    // Gets unvisited neighbours of the given position
    public static List<Neighbour> GetUnvisitedNeighbours(Position pos, WallState[,] maze, int width, int height)
    {
        var neighbours = new List<Neighbour>();

        // Check left neighbour
        if (pos.X > 0 && !maze[pos.X - 1, pos.Y].HasFlag(WallState.Visited))
        {
            neighbours.Add(new Neighbour
            {
                NeighbourPos = new Position { X = pos.X - 1, Y = pos.Y },
                SharedWall = WallState.Left
            });
        }

        // Check down neighbour
        if (pos.Y > 0 && !maze[pos.X, pos.Y - 1].HasFlag(WallState.Visited))
        {
            neighbours.Add(new Neighbour
            {
                NeighbourPos = new Position { X = pos.X, Y = pos.Y - 1 },
                SharedWall = WallState.Down
            });
        }

        // Check up neighbour
        if (pos.Y < height - 1 && !maze[pos.X, pos.Y + 1].HasFlag(WallState.Visited))
        {
            neighbours.Add(new Neighbour
            {
                NeighbourPos = new Position { X = pos.X, Y = pos.Y + 1 },
                SharedWall = WallState.Up
            });
        }

        // Check right neighbour
        if (pos.X < width - 1 && !maze[pos.X + 1, pos.Y].HasFlag(WallState.Visited))
        {
            neighbours.Add(new Neighbour
            {
                NeighbourPos = new Position { X = pos.X + 1, Y = pos.Y },
                SharedWall = WallState.Right
            });
        }

        return neighbours;
    }

    // Applies the recursive backtracking algorithm to generate the maze
    private static WallState[,] ApplyRecursiveBacktracker(WallState[,] maze, int width, int height)
    {
        var rng = new Random();
        var stack = new Stack<Position>();
        var startPosition = new Position { X = rng.Next(0, width), Y = rng.Next(0, height) };

        // Mark the starting position as visited and push it onto the stack
        maze[startPosition.X, startPosition.Y] |= WallState.Visited;
        stack.Push(startPosition);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            var neighbours = GetUnvisitedNeighbours(current, maze, width, height);

            if (neighbours.Count > 0)
            {
                // Push current position back onto the stack
                stack.Push(current);

                // Choose a random neighbour to move to
                var randIndex = rng.Next(0, neighbours.Count);
                var randomNeighbour = neighbours[randIndex];

                var neighbourPosition = randomNeighbour.NeighbourPos;

                // Remove walls between current cell and chosen neighbour
                maze[current.X, current.Y] &= ~randomNeighbour.SharedWall;
                maze[neighbourPosition.X, neighbourPosition.Y] &= ~GetOppositeWall(randomNeighbour.SharedWall);

                // Mark the neighbour as visited and push it onto the stack
                maze[neighbourPosition.X, neighbourPosition.Y] |= WallState.Visited;
                stack.Push(neighbourPosition);
            }
        }

        return maze;
    }

    // Returns the opposite wall direction
    private static WallState GetOppositeWall(WallState wall)
    {
        return wall switch
        {
            WallState.Right => WallState.Left,
            WallState.Left => WallState.Right,
            WallState.Up => WallState.Down,
            WallState.Down => WallState.Up,
            _ => WallState.Left // Default case
        };
    }
}
