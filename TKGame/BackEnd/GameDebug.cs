using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TKGame.World;

namespace TKGame.BackEnd
{
    public static class GameDebug
    {
        #region Member Variables

        // Boolean to describe whether debug mode is on or off
        public static bool DebugMode { get; set; }
        public static double FPS { get; private set; }
        private static Texture2D HitboxTexture { get; set; }

        #endregion

        static GameDebug()
        {
#if DEBUG
            DebugMode = true;
#else
            DebugMode = false;
#endif
            FPS = 0d;
        }

        /// <summary>
        /// Updates the debug UI's FPS text using a given GameTime. This should be
        /// called at the *very end* of the game's Draw() function
        /// </summary>
        /// <param name="gameTime"></param>
        public static void UpdateFPS(GameTime gameTime)
        {
            FPS = 1d / gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Draw bounding rectangle around a given rectangle.
        /// </summary>
        public static void DrawBoundingBox(Rectangle rectangle, Color color, int lineWidth, SpriteBatch spriteBatch)
        {
            if (HitboxTexture is null)
            {
                HitboxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                HitboxTexture.SetData(new Color[] { Color.White });
            }

            // These will draw thin rectangles (essentially lines) on each edge of a rectangle
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), color);
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), color);
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X + rectangle.Width - lineWidth, rectangle.Y, lineWidth, rectangle.Height), color);
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - lineWidth, rectangle.Width, lineWidth), color);
        }

        /// <summary>
        /// Draw a bounding rectangle around a given entity.
        /// </summary>
        public static void DrawBoundingBox(Entity entity, Color color, int lineWidth)
        {
            // Construct a Rectangle out of the entity's position and size so we can
            // just use the other DrawBoundingRectangle() function to actually draw
            Rectangle entityRect = new Rectangle
            {
                X = entity.HitBox.X,
                Y = entity.HitBox.Y,
                Width = (int)entity.HitBox.Width,
                Height = (int)entity.HitBox.Height
            };
            DrawBoundingBox(entityRect, color, lineWidth, TKGame.SpriteBatch);
        }
    }
}