﻿using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

namespace TKGame
{
    class Player : Entity
    {
        private static Player instance;
        private static object syncRoot = new object();
        public static Player Instance
        {
            get
            {
                // Creates the player if it doesn't already exist
                // Uses thread locking to guarentee safety.
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Player();
                    }

                return instance;
            }
        }

        /// <summary>
        /// Player components.
        /// </summary>
        private Player()
        {
            entityTexture = Art.PlayerTexture;
            // Figure out how to not hard code for now
            // Starts at (1560, 450) at the middle on the floor level
            Position = new Vector2(1600/2, 900 - 40);
        }

        /// <summary>
        /// Grabs the input data, uses that and the deltaTime to update the Player's velocity and orientation.
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            // Player Movement
            Velocity = Input.GetMovementDirection();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            const float movementSpeed = 500;

            Position.X += movementSpeed * Velocity.X * deltaTime;
            Position.Y += movementSpeed * Velocity.Y * deltaTime;

            Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);

            if (Velocity.X > 0) 
            {
                Orientation = SpriteEffects.None;
            }
            else if (Velocity.X < 0)
            {
                Orientation = SpriteEffects.FlipHorizontally;
            }
        }

        /// <summary>
        /// Draws each Player Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
