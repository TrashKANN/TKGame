using System;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Security.AccessControl;
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
        private static Enemy instance;
        private static object syncRoot = new object();
        Vector2 spawn = new Vector2(150, 700);           // to set initial Position
        Vector2 speed = new Vector2((float)1.5, 1);      // to set initial Velocity
        int rightBoundary = 1500;                        // right boundary variable for Enemy's patrol
        int leftBoundary = 150;                          // left boundary variable for Enemy's patrol

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
        /// sets texture, initial spawn point, and x-coordinate velocity for Enemy
        /// </summary>
        private Enemy()
        {
            entityTexture = Art.EnemyTexture;
            Position = spawn;
            Velocity.X = speed.X;
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

        }

        /// <summary>
        /// Enemy Update method
        /// calls UpdateLocation() to handle the work for patrolling enemy animation
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime)
        {
            Patrol();
        }

        /// <summary>
        /// Updates Enemy location for patrol animation 
        /// uses formula to make Enemy sprite move bakc and forth within boundaries
        /// </summary>
        public void Patrol()
        {
            Position.X += Velocity.X;                       // Enemy initially starts moving to right

            if (Position.X == rightBoundary)
            {
                Velocity.X = -speed.X;                      // Enemy moves left
                Orientation = SpriteEffects.FlipHorizontally;
            }
            else if (Position.X == leftBoundary)
            {
                Velocity.X = speed.X;                       // Enemy moves right
                Orientation = SpriteEffects.None;
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
