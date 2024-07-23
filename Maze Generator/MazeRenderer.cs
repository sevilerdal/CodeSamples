using System.Collections.Generic;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField] private int width = 10;                // Width of the maze
    [SerializeField] private int height = 10;               // Height of the maze
    [SerializeField] private float size = 1f;               // Size of each cell in the maze
    [SerializeField] private Transform wallPrefab = null;   // Prefab for the maze walls
    [SerializeField] private Transform floorPrefab = null;  // Prefab for the maze floor

    private WallState[,] maze;                             // 2D array representing the maze
    private List<Transform> walls = new List<Transform>(); // List to keep track of wall objects

    void Start()
    {
        // Generate and render the maze when the game starts
        maze = MazeGenerator.Generate(width, height);
        Render(maze);
    }

    // Regenerates the maze and re-renders it
    public void Regenerate()
    {
        DestroyMaze();    // Destroy the existing maze
        maze = MazeGenerator.Generate(width, height); // Generate a new maze
        Render(maze);    // Render the new maze
    }

    // Renders the maze based on the maze array
    void Render(WallState[,] maze)
    {
        // Instantiate and scale the floor object
        var floor = Instantiate(floorPrefab, transform);
        floor.localScale = new Vector3(width, 1, height);

        // Iterate through each cell in the maze
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                var cell = maze[i, j]; // Get the wall state for the current cell
                var position = new Vector3(-width / 2 + i, 0, -height / 2 + j); // Calculate the cell position

                // Render the upper wall if necessary
                if (cell.HasFlag(WallState.Up))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, 0, size / 2);
                    topWall.eulerAngles = new Vector3(0, 90, 0);
                    walls.Add(topWall);
                }

                // Render the left wall if necessary
                if (cell.HasFlag(WallState.Left))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size / 2, 0, 0);
                    walls.Add(leftWall);
                }

                // Render the right wall at the end of each row if necessary
                if (i == width - 1 && cell.HasFlag(WallState.Right))
                {
                    var rightWall = Instantiate(wallPrefab, transform) as Transform;
                    rightWall.position = position + new Vector3(size / 2, 0, 0);
                    walls.Add(rightWall);
                }

                // Render the bottom wall for the first row if necessary
                if (j == 0 && cell.HasFlag(WallState.Down))
                {
                    var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                    bottomWall.position = position + new Vector3(0, 0, -size / 2);
                    bottomWall.eulerAngles = new Vector3(0, 90, 0);
                    walls.Add(bottomWall);
                }
            }
        }
    }

    // Destroys all wall objects and clears the maze data
    void DestroyMaze()
    {
        foreach (var wall in walls)
        {
            Destroy(wall.gameObject); // Destroy each wall object
        }
        maze = null; // Clear the maze data
        walls.Clear(); // Clear the list of wall objects
    }
}
