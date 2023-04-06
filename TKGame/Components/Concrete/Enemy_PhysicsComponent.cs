using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;

namespace TKGame.Components.Concrete
{
    internal class Enemy_PhysicsComponent : PhysicsComponent
    {
        void PhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/)
        {
            Player player = Player.Instance;

            if (player != null)
            {
                Vector2 playerPosition = player.Position;

                if (entity.Position.X > playerPosition.X)
                {
                    entity.Position.X -= entity.Velocity.X;
                    entity.Orientation = SpriteEffects.None;
                }
                if (entity.Position.X < playerPosition.X)
                {
                    entity.Position.X += entity.Velocity.X;
                    entity.Orientation = SpriteEffects.FlipHorizontally;
                }
                if (entity.Position.Y > playerPosition.Y)
                {
                    entity.Position.Y -= entity.Velocity.Y;
                }
                if (entity.Position.Y < playerPosition.Y)
                {
                    entity.Position.Y += entity.Velocity.Y;
                }

                entity.hitBox.X = (int)entity.Position.X - (int)entity.Size.X / 2;
                entity.hitBox.Y = (int)entity.Position.Y - (int)entity.Size.Y / 2;
                entity.Position = Vector2.Clamp(entity.Position, entity.Size / 2, TKGame.ScreenSize - entity.Size / 2);
            }
        }
    }
}
