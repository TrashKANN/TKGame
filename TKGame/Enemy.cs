using System;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TKGame.Level_Editor_Content;

namespace TKGame
{
    class Enemy : Entity
    {
        static Enemy instance;
        private static object syncRoot = new object();
        Vector2 spawn = new Vector2(150, 700);           // to set initial Position
        Vector2 speed = new Vector2((float)1.5, 1);      // to set initial Velocity

        /// <summary>
        /// Create instance of Enemy object with locking for gaurantee
        /// </summary>
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
            Position = spawn;
            Velocity.X = speed.X;
            entityName = "enemy"; // name for current enemy class
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }

        /// <summary>
        /// Enemy Update method
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Move();
        }

        public void Move()
        {
            Player player = Player.Instance;

            // check if player is null
            if (player != null)
            {
                Vector2 playerPosition = player.Position;
                
                if (Position.X > playerPosition.X)
                {
                    Position.X -= speed.X;
                    Orientation = SpriteEffects.FlipHorizontally;
                }
                if (Position.X < playerPosition.X)
                {
                    Position.X += speed.X;
                    Orientation = SpriteEffects.None;
                }
                if (Position.Y > playerPosition.Y)
                {
                    Position.Y -= speed.Y;
                }
                if (Position.Y < playerPosition.Y)
                {
                    Position.Y += speed.Y;
                }

                hitBox.X = (int)Position.X - (int)Size.X / 2;
                hitBox.Y = (int)Position.Y - (int)Size.Y / 2;
                Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);
            }
        }

        /// <summary>
        /// Draws Enemy sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
