using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    private float lastFall = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Default position not valid? Then it's game over
        if (!IsValidBoard())
        {
            Debug.Log("GAME OVER");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame.
    // Implements all piece movements: right, left, rotate and down.
    void Update()
    {
        // Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Modify position
            transform.position += new Vector3(-1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
            {
                // It's valid. Update grid.
                UpdateBoard();
            }
            else
            {
                // It's not valid. Revert.
                transform.position += new Vector3(1, 0, 0);
            }
        }

        // Move Right
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Modify position
            transform.position += new Vector3(1, 0, 0);

            // See if it's valid
            if (IsValidBoard())
            {
                // It's valid. Update grid.
                UpdateBoard();
            }
            else
            {
                // It's not valid. Revert.
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        // Rotate
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.Rotate(0, 0, -90);

            // See if it's valid
            if (IsValidBoard())
            {
                // It's valid. Update grid.
                UpdateBoard();
            }
            else
            {
                // It's not valid. Revert.
                transform.Rotate(0, 0, 90);
            }
        }

        // Move Downwards and Fall (each second)
        if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= 1)
        {
            // Modify position
            transform.position += new Vector3(0, -1, 0);

            // See if it's valid
            if (IsValidBoard())
            {
                // It's valid. Update grid.
                UpdateBoard();
            }
            else
            {
                // It's not valid. Revert.
                transform.position += new Vector3(0, 1, 0);

                // Clear filled horizontal lines
                Board.DeleteFullRows();

                // Spawn next Group
                FindFirstObjectByType<Spawner>().SpawnNext();

                // Disable script
                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    // Updates the board with the current position of the piece.
    void UpdateBoard()
    {
        // Clear current positions of the piece in the grid
        for (int y = 0; y < Board.grid.GetLength(1); y++)
        {
            for (int x = 0; x < Board.grid.GetLength(0); x++)
            {
                if (Board.grid[x, y] != null &&
                    Board.grid[x, y].transform.parent == transform)
                {
                    Board.grid[x, y] = null;
                }
            }
        }

        // Update the grid with the new positions of the piece's blocks
        foreach (Transform child in transform)
        {
            Vector2 v = Board.RoundVector2(child.position);
            Board.grid[(int)v.x, (int)v.y] = child.gameObject;
        }
    }

    // Returns if the current position of the piece makes the board valid or not
    bool IsValidBoard()
    {
        foreach (Transform child in transform)
        {
            Vector2 v = Board.RoundVector2(child.position);

            // Not inside Border?
            if (!Board.InsideBorder(v))
                return false;

            // Block in grid cell (and not part of same group)?
            if (Board.grid[(int)v.x, (int)v.y] != null &&
                Board.grid[(int)v.x, (int)v.y].transform.parent != transform)
                return false;
        }
        return true;
    }
}