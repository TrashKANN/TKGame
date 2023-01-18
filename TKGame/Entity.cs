using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TKGame
{
    abstract class Entity
    {
        protected Texture2D image;
        protected Color color = Color.White;

        public Vector2 Position, Velocity;
        public SpriteEffects Orientation; // Flip Horizontal/Vertical
        public bool IsExpired;

        public Vector2 Size
        {
            get 
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        public abstract void Update(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, 0, Size / 2f, 1f, Orientation, 0);
        }
    }
}
