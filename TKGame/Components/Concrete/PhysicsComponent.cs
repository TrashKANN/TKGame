using Microsoft.Xna.Framework;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Concrete
{
    public class PhysicsComponent
    {
        private static readonly float GRAVITY = 1.0f;
        internal void Update(Entity /*&*/entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 endVel = entity.Velocity;
            endVel += entity.MOVEMENT_SPEED * entity.Velocity * deltaTime;

            endVel.Y = GRAVITY;
            entity.Position += endVel;

            entity.hitBox.X = (int)entity.Position.X - (int)entity.Size.X / 2;
            entity.hitBox.Y = (int)entity.Position.Y - (int)entity.Size.Y / 2;

            //world.resolveCollisions(entity.Position, entity.HitBox);
            entity.Position = Vector2.Clamp(entity.Position, entity.Size / 2, TKGame.ScreenSize - entity.Size / 2);

            // The world should own all of the Stage stuff & entities
            // so we would call it here within the PhysicsComponent instead of
            // in the EntitiyManager.

            // GameTime should be owned by World as well.
        }
    }
}
