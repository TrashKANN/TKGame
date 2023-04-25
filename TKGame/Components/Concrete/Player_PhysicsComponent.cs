using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    internal class Player_PhysicsComponent : PhysicsComponent
    {
        private const int GRAVITY = 1000;

        void PhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            Player player = entity as Player;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Velocity.X += player.MOVEMENT_SPEED * player.Velocity.X * deltaTime;
            player.Position.X += player.Velocity.X * deltaTime;
            player.Position.Y += player.Velocity.Y * deltaTime;

            if(player.CollidedVertically)
            {
                player.Velocity.Y = 0;
                player.CollidedVertically = false;
            } 
            else
            {
                player.Velocity.Y += GRAVITY * deltaTime;
            }

            // update hitbox
            entity.HitBox = new Rectangle(((int)player.Position.X - ((int)player.Size.X / 2)),
                                            ((int)player.Position.Y - (int)player.Size.Y / 2),
                                            (int)player.Size.X,
                                            (int)player.Size.Y);

            // clamp position to screen bounds
            //world.resolveCollisions(entity.Position, entity.HitBox);
            player.Position = Vector2.Clamp(player.Position, player.Size / 2, TKGame.ScreenSize - entity.Size / 2);

            // The world should own all of the Stage stuff & entities
            // so we would call it here within the PhysicsComponent instead of
            // in the EntitiyManager.

            // GameTime should be owned by World as well.
        }
    }
}
