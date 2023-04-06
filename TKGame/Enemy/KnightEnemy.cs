﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.BackEnd;

namespace TKGame
{
    public class KnightEnemy : Enemy
    {
        private PhysicsComponent knightEnemyPhysics;

        /// <summary>
        /// knight enemy components
        /// </summary>
        public KnightEnemy(PhysicsComponent physics_) 
        {
            entityTexture = Art.KnightEnemyTexture; 
            Position = new Vector2(300, 800); // hard coded spawn position at the moment
            velocity = new Vector2((float)1.5, 1);
            HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);

            knightEnemyPhysics = physics_;
        }

        /// <summary>
        /// Update knight enemy components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime, SpriteBatch spriteBatch)
        {
            knightEnemyPhysics.Update(, gameTime/*, world*/);
            //Player player = Player.Instance;

            //if (player != null)
            //{
            //    Vector2 playerPosition = player.Position;

            //    if (Position.X > playerPosition.X)
            //    {
            //        Position.X -= velocity.X;
            //        Orientation = SpriteEffects.None;
            //    }
            //    if (Position.X < playerPosition.X)
            //    {
            //        Position.X += velocity.X;
            //        Orientation = SpriteEffects.FlipHorizontally;
            //    }
            //    if (Position.Y > playerPosition.Y)
            //    {
            //        Position.Y -= velocity.Y;
            //    }
            //    if (Position.Y < playerPosition.Y)
            //    {
            //        Position.Y += velocity.Y;
            //    }

            //    hitBox.X = (int)Position.X - (int)Size.X / 2;
            //    hitBox.Y = (int)Position.Y - (int)Size.Y / 2;
            //    Position = Vector2.Clamp(Position, Size / 2, TKGame.ScreenSize - Size / 2);
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
