using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Assignment12
{
    public class Grid
    {
        // Keep track of X's and O's, fill this matrix with gridVal
        private GridVal[,] grid;
        // Each and any spot my be selected
        private bool[,] selection;
        // Current "cursor" in 2D
        private int[] current;
        // List of models
        private MyModel[] models;
        // Track who won (Dot means no winner)
        private GridVal WhoWon;

        public Grid(MyModel[] models)
        {
            // Setup variables
            grid = new GridVal[3, 3];
            selection = new bool[3, 3];
            current = new int[2] { 0, 0 };
            WhoWon = GridVal.dot;
            this.models = models;

            // Setup the grid
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            // Setup a 3 by 3 grid
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    grid[i, j] = GridVal.dot;
                    selection[i, j] = false;
                }
            }
        }

        // Let the "cursor" move by changing the current spot depending on direction
        public void MoveCursor(int dx, int dy)
        {
            current[0] = (current[0] + dx + 3) % 3;
            current[1] = (current[1] + dy + 3) % 3;
        }

        // Change selection and check who wins
        public void ToggleSelection()
        {
            selection[current[0], current[1]] = !selection[current[0], current[1]];
            int eVal = (int)grid[current[0], current[1]];
            int max_eVal = Enum.GetNames(typeof(GridVal)).Length;
            grid[current[0], current[1]] = (GridVal)((eVal + 1) % max_eVal);
            WhoWon = GetWhoWon();
        }

        public GridVal GetWhoWon()
        {
            // Turn is enum of X, O
            foreach (var player in new GridVal[] { GridVal.X, GridVal.O })
            {
                // Check horizontal and vertical
                for (int i = 0; i < grid.GetLength(0); i++)
                {
                    if ((grid[i, 0] == player && grid[i, 1] == player && grid[i, 2] == player) ||
                        (grid[0, i] == player && grid[1, i] == player && grid[2, i] == player))
                    {
                        return player;
                    }
                }
                // Diagonals
                if ((grid[0, 0] == player && grid[1, 1] == player && grid[2, 2] == player) ||
                    (grid[2, 0] == player && grid[1, 1] == player && grid[0, 2] == player))
                {
                    return player;
                }
            }
            // No winner
            return GridVal.dot;
        }

        // Draw the grid with the cameraPosition, aspectRatio, etc. (same as how it was in the main game class)
        public void DrawGrid(Vector3 cameraPosition, float aspectRatio, Vector3 cameraTarget, Vector3 cameraUpDirection)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    bool won = (grid[i, j] != GridVal.dot && WhoWon == grid[i, j]);
                    Vector3 location = new Vector3(i * 100 - 50.0f, j * 100 - 50.0f, 1.0f);

                    MyModel here = models[(int)grid[i, j]];
                    here.pos = location;
                    here.color = (grid[i, j] == GridVal.X) ? Color.BlueViolet :
                                 (grid[i, j] == GridVal.O) ? Color.DarkOrange :
                                 Color.PaleGreen;

                    if (i == current[0] && j == current[1])
                        here.color = Color.Red;

                    here.draw(cameraPosition, aspectRatio, cameraTarget, cameraUpDirection, won);
                }
            }
        }
    }
}