using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace TKGame
{
    public abstract class Entity
    {
        protected Texture2D entityTexture;

        // Move to a Transform class later instead of having it only in the Entity class
        public Vector2 Position, Velocity;
        public SpriteEffects Orientation; // Flip Horizontal/Vertical
        // used for Drawing Sprites
        public Color color = Color.White;
        public bool IsExpired;
        public string entityName; // to identify each entity by name

        public Vector2 Size
        {
            get 
            {
                return entityTexture == null ? Vector2.Zero : new Vector2(entityTexture.Width, entityTexture.Height);
            }
        }

        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the entity sprites with the default values. More parameters can be added to Draw() later to augment these later.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityTexture, Position, null, color, 0, Size / 2f, 1f, Orientation, 0);
        }
    }
}
