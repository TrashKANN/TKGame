using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.Level_Editor_Content
{
    static class LevelEditor
    {
        //private static GraphicsDevice graphics;
        //private static SpriteBatch spriteBatch;
        private static MouseState previousMouseState;
        private static Vector2 startPosition;
        private static bool EditMode = false;

        public static void ToggleEditor()
        {
            EditMode = !EditMode;
        }
        public static void BuildWall(Stage stage, GraphicsDevice graphics)
        {
            MouseState currentMouseState = Mouse.GetState();
            Vector2 topLeftPosition = new Vector2();
            Vector2 size = new Vector2();

            // If the left mouse button WAS NOT pressed last update AND IS pressed this update, store coordinates
            if (previousMouseState.LeftButton == ButtonState.Released &&
                currentMouseState.LeftButton == ButtonState.Pressed)
            {
                startPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
            }
            // If the left mouse button IS ALREADY pressed and WAS RELEASED this update, store coordinates for end position
            else if (previousMouseState.LeftButton == ButtonState.Pressed &&
                     currentMouseState.LeftButton == ButtonState.Released)
            {
                Vector2 endPosition = new Vector2(currentMouseState.X, currentMouseState.Y);
                topLeftPosition.X = Math.Min(startPosition.X, endPosition.X);
                topLeftPosition.Y = Math.Min(startPosition.Y, endPosition.Y);
                size = new Vector2(Math.Abs(startPosition.X - endPosition.X), Math.Abs(startPosition.Y - endPosition.Y));
            }

            previousMouseState = currentMouseState;

            Wall newWall = new Wall(((int)topLeftPosition.X), ((int)topLeftPosition.Y), ((int)size.X), ((int)size.Y), graphics);

            stage.walls.Add(newWall);
        }
    }
}
