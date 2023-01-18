using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TKGame
{
    abstract class Entity
    {
        protected Texture2D image;
        protected Color color = Color.White;

        public Vector2 Position, Velocity;
        public float Orientation;
        public bool IsExpired;

        public Vector2 Size
        {
            get 
            {
                return image == null ? Vector2.Zero : new Vector2(image.Width, image.Height);
            }
        }

        public abstract void Update();

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, Position, null, color, Orientation, Size / 2f, 1f, 0, 0);
        }
    }
}
