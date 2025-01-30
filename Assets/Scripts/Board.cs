using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static int w = 10; // Width of the grid
    public static int h = 20; // Height of the grid
    public static GameObject[,] grid = new GameObject[w, h]; // 2D array to store the grid

    // Rounds Vector2 so it does not have decimal values
    // Used to force Integer coordinates (without decimals) when moving pieces
    public static Vector2 RoundVector2(Vector2 v)
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    // Returns true if pos (x,y) is inside the grid, false otherwise
    public static bool InsideBorder(Vector2 pos)
    {
        return (pos.x >= 0 && pos.x < w && pos.y >= 0);
    }

    // Deletes all GameObjects in the row Y and sets the row cells to null
    public static void DeleteRow(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] != null)
            {
                Destroy(grid[x, y]); // Destroy the game object
                grid[x, y] = null;   // Clear the reference in the grid
            }
        }
    }

    // Moves all GameObjects on row Y to row Y-1
    public static void DecreaseRow(int y)
    {
        if (y - 1 >= 0) // Ensure the row above is within bounds
        {
            for (int x = 0; x < w; ++x)
            {
                if (grid[x, y] != null)
                {
                    // Move the object in the grid down
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;

                    // Update the object's position in the world
                    grid[x, y - 1].transform.position += new Vector3(0, -1, 0);
                }
            }
        }
    }

    // Decreases all rows above Y
    public static void DecreaseRowsAbove(int y)
    {
        for (int i = y; i < h; ++i)
        {
            DecreaseRow(i);
        }
    }

    // Returns true if all cells in a row have a GameObject (are not null), false otherwise
    public static bool IsRowFull(int y)
    {
        for (int x = 0; x < w; ++x)
        {
            if (grid[x, y] == null)
            {
                return false; // There is an empty cell, the row is not full
            }
        }
        return true; // All cells are occupied
    }

    // Deletes full rows
    public static void DeleteFullRows()
    {
        for (int y = 0; y < h; ++y)
        {
            if (IsRowFull(y))
            {
                DeleteRow(y);
                DecreaseRowsAbove(y + 1);
                --y; // Recheck the same row index after shifting
            }
        }
    }
}