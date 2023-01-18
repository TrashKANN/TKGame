using System;
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
        public static Player Instance
        {
            get
            {
                // Creates the player if it doesn't already exist
                if (instance == null)
                    instance = new Player();

                return instance;
            }
        }

        private Player()
        {
            image = Art.Player;
            // Figure out how to not hard code for now
            // Starts at (1560, 450) at the middle on the floor level
            Position = new Vector2(1600/2, 900 - 40);
        }

        public override void Update(GameTime gameTime)
        {
            // Player Movement
            Vector2 direction = Input.GetMovementDirection();
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            const float movementSpeed = 1;

            Velocity.X += movementSpeed * direction.X * deltaTime;
            Velocity.Y += movementSpeed * direction.Y * deltaTime;
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);

            if (direction.X > 0) 
            {
                Orientation = SpriteEffects.None;
            }
            else if (direction.X < 0)
            {
                Orientation = SpriteEffects.FlipHorizontally;
            }

            Velocity = Vector2.Zero; // Reset velocity at the end of the frame
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
