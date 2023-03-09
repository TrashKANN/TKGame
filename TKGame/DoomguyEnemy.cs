using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TKGame.Enemy
{
    class DoomguyEnemy : Enemy
    {
        static DoomguyEnemy instance;
        private static object syncRoot = new object();

        public static DoomguyEnemy Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                        {
                            instance = new DoomguyEnemy();
                        }
                    }
                }
                return instance;
            }
        }

        private DoomguyEnemy()
        {
            entityTexture = Art.EnemyTexture;
            position = new Vector2(150, 700);
            velocity = new Vector2((float)1.5, 1);
            entityName = "DoomguyEnemy";
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
                    Orientation = SpriteEffects.FlipHorizontally;
                }
                if (Position.X < playerPosition.X)
                {
                    Position.X += velocity.X;
                    Orientation = SpriteEffects.None;
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