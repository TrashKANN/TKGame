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
        public Rectangle rectangle { get; private set; }

        public static Enemy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot) 
                    {
                        if (instance == null)
                        {
                            instance = new Enemy();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// Enemy constructor
        /// </summary>
        private Enemy()
        {
            entityTexture = Art.EnemyTexture;

            Position = new Vector2(700, 100);
        }

        /// <summary>
        /// Updates Enemy Sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime)
        {
            //throw new NotImplementedException();
        }

        /// <summary>
        /// Draws Enemy Sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
