using System;
using System.Diagnostics;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using TKGame.Level_Editor_Content;

namespace TKGame
{
    class Player : Entity
    {
        private static Player instance;
        private static object syncRoot = new object();
        private static readonly float GRAVITY = 1.0f;
        private static float MOVEMENT_SPEED = 500.0f;
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
            Position = new Vector2(1600/2, 900 - 70);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
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
            UpdatePlayerPosition(deltaTime);
        }

        /// <summary>
        /// Draws each Player Sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        #region Update Helper Functions
        /// <summary>
        /// Updates the Player position based on the keyboard input and gravity.
        /// </summary>
        /// <param name="deltaTime"></param>
        private void UpdatePlayerPosition(float deltaTime)
        {
            Vector2 endVelocity = Velocity;

            endVelocity.X += MOVEMENT_SPEED * Velocity.X * deltaTime;
            endVelocity.Y += MOVEMENT_SPEED * Velocity.Y * deltaTime;


            if (Velocity.X > 0)
            {
                Orientation = SpriteEffects.None;
            }
            else if (Velocity.X < 0)
            {
                Orientation = SpriteEffects.FlipHorizontally;
            }

            endVelocity.Y += GRAVITY;

            Position += endVelocity;

            Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);

            HitBox.X = (int)Position.X - (int)Size.X / 2;
            HitBox.Y = (int)Position.Y - (int)Size.Y / 2;
        }

        #endregion Update Helper Functions
    }
}
