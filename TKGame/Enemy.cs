using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace TKGame
{
    class Enemy : Entity
    {
        private static Enemy instance;
        private static object syncRoot = new object();
        Vector2 startPosition = new Vector2(500, 650);
        Vector2 stopPosition = new Vector2(1200, 650);

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
            Position = startPosition;
        }

        /// <summary>
        /// Updates Enemy Sprite
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime)
        {
            // enemy moves to right
            Velocity.X = 1f;
            Position += Velocity;
        }

        /// <summary>
        /// Draws Enemy Sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Velocity.X > 0f)
                base.Draw(spriteBatch);
        }
    }
}
