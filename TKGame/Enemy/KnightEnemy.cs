using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;

namespace TKGame
{
    public class KnightEnemy : Enemy
    {
        public KnightEnemy() 
        {
            entityTexture = Art.KnightEnemyTexture; 
            Position = new Vector2(300, 800); // hard coded spawn position at the moment
            velocity = new Vector2((float)1.5, 1);
            HitBox = new Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
            Player player = Player.Instance;

            // check if player is null
            if (player != null)
            {
                Vector2 playerPosition = player.Position;

                if (Position.X > playerPosition.X)
                {
                    Position.X -= velocity.X;
                    Orientation = SpriteEffects.None;
                }
                if (Position.X < playerPosition.X)
                {
                    Position.X += velocity.X;
                    Orientation = SpriteEffects.FlipHorizontally;
                }
                if (Position.Y > playerPosition.Y)
                {
                    Position.Y -= velocity.Y;
                }
                if (Position.Y < playerPosition.Y)
                {
                    Position.Y += velocity.Y;
                }

                hitBox.X = (int)Position.X - (int)Size.X / 2;
                hitBox.Y = (int)Position.Y - (int)Size.Y / 2;
                Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
