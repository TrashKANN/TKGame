using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TKGame
{
    class Enemy : Entity
    {
        private static Enemy instance;
        private static object syncRoot = new object(); 
        Texture2D texture;
        Rectangle rectangle;
        Vector2 position;
        Vector2 origin;
        Vector2 velocity;

        /// <summary>
        /// Enemy constructor
        /// </summary>
        private Enemy()
        {
            entityTexture = Art.EnemyTexture;

            // TODO: Start position
            //Position = new Vector2(?, ?);
        }

        /// <summary>
        /// Updates the Enemy Sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws each Enemy Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch); 
        }
    }
}
