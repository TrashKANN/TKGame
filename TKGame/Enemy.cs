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

namespace TKGame
{
    class Enemy : Entity
    {
        static Enemy instance;
        private static object syncRoot = new object();
        Vector2 spawn = new Vector2(150, 785);           // to set initial Position
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
        /// </summary>
        private Enemy()
        {
            entityTexture = Art.EnemyTexture;
            Position = spawn;
            Velocity.X = speed.X;
            entityName = "enemy"; // name for current enemy class
        }

        /// <summary>
        /// Enemy Update method
        /// </summary>
        /// <param name="gameTime"></param>
        /// <exception cref="NotImplementedException"></exception>
        public override void Update(GameTime gameTime)
        {
            Move();
        }

        public void Move()
        {
            // getter for player class
            Player player = EntityManager.GetEntities().FirstOrDefault(x => x.entityName == "player" && x is Player) as Player;

            // check if player is null
            if (player != null)
            {
                Vector2 playerPosition = player.Position;
                
                if (Position.X > playerPosition.X) 
                    Position.X -= speed.X;
                if (Position.X < playerPosition.X)
                    Position.X += speed.X;
                if (Position.Y > playerPosition.Y)
                    Position.Y -= speed.Y;
                if (Position.Y < playerPosition.Y)
                    Position.Y += speed.Y;
            }

            // Enemy initially starts moving to right
            //Position.X += Velocity.X;

            // Enemy moves left
            //if (Position.X == rightBoundary)
            //{
            //    Velocity.X = -speed.X;                      
            //    Orientation = SpriteEffects.FlipHorizontally;
            //}
            // Enemy moves right
            //else if (Position.X == leftBoundary)
            //{
            //    Velocity.X = speed.X;                       
            //    Orientation = SpriteEffects.None;
            //}
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
