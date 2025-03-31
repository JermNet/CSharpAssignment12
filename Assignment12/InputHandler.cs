using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment12
{
    public class InputHandler
    {
        // Check for last input
        private bool lastTimeWasNotSpace = false;
        private bool lastTimeWasNotUp = false;
        private bool lastTimeWasNotRight = false;
        private bool lastTimeWasNotLeft = false;
        private bool lastTimeWasNotDown = false;

        public void HandleInput(Grid gameGrid, out bool gameOver, out string message)
        {
            gameOver = false;
            message = "";

            KeyboardState k = Keyboard.GetState();
            if (k.IsKeyDown(Keys.Up) && lastTimeWasNotUp) gameGrid.MoveCursor(0, 1);
            if (k.IsKeyDown(Keys.Down) && lastTimeWasNotDown) gameGrid.MoveCursor(0, -1);
            if (k.IsKeyDown(Keys.Right) && lastTimeWasNotRight) gameGrid.MoveCursor(1, 0);
            if (k.IsKeyDown(Keys.Left) && lastTimeWasNotLeft) gameGrid.MoveCursor(-1, 0);
            if (k.IsKeyDown(Keys.Space) && lastTimeWasNotSpace) gameGrid.ToggleSelection();

            // Check for winner, setting gameover to true as well as a winning message if the winner is not dot
            string winner = gameGrid.GetWhoWon().ToString();
            if (!winner.Equals("dot"))
            {
                message = $"{winner} is the winner! Press Escape to Quit!";
                gameOver = true;
            }

            lastTimeWasNotUp = !k.IsKeyDown(Keys.Up);
            lastTimeWasNotDown = !k.IsKeyDown(Keys.Down);
            lastTimeWasNotRight = !k.IsKeyDown(Keys.Right);
            lastTimeWasNotLeft = !k.IsKeyDown(Keys.Left);
            lastTimeWasNotSpace = !k.IsKeyDown(Keys.Space);
        }
    }
}
