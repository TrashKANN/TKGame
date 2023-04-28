using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    internal class C_Player_Physics : PhysicsComponent
    {
        private static readonly float GRAVITY = 1.0f;
        void PhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            Vector2 endVel = entity.Velocity;
            endVel += entity.MOVEMENT_SPEED * entity.Velocity * deltaTime;

            endVel.Y += GRAVITY;

            entity.Position += endVel;

            entity.HitBox = new Rectangle(((int)entity.Position.X - ((int)entity.Size.X / 2)),
                                            ((int)entity.Position.Y - (int)entity.Size.Y / 2),
                                            (int)entity.Size.X,
                                            (int)entity.Size.Y);

            //world.resolveCollisions(entity.Position, entity.HitBox);
            entity.Position = Vector2.Clamp(entity.Position, entity.Size / 2, TKGame.ScreenSize - entity.Size / 2);

            // The world should own all of the Stage stuff & entities
            // so we would call it here within the PhysicsComponent instead of
            // in the EntitiyManager.

            // GameTime should be owned by World as well.
        }
    }
}
