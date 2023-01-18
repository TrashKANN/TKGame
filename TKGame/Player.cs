using System;
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
            Position = new Vector2(1600 - 40, 900/2);
        }

        public override void Update()
        {
            // Player Movement
            const float movementSpeed = 10;
            Velocity += movementSpeed * Input.GetMovementDirection();
            Position += Velocity;
            Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);

            if (Velocity.LengthSquared() > 0)
            {
                Orientation = Velocity.Length(); // Flip Orientation based on direction
            }

            Velocity = Vector2.Zero; // Reset velocity at the end of the frame
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
