using Microsoft.Xna.Framework;
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
        private const int GRAVITY = 10;
        private float jumpTimer = 0;
        private float jumpDuration = 2;
        void PhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            entity.Velocity.X += entity.MOVEMENT_SPEED * entity.Velocity.X * deltaTime;
            entity.Position.X += entity.Velocity.X * deltaTime;
            entity.Position.Y += entity.Velocity.Y * deltaTime;
            entity.Velocity.Y += GRAVITY * deltaTime;

            jumpTimer += deltaTime;
            if(jumpTimer >= jumpDuration) 
            { 
                entity.Velocity.Y = 0; 
                jumpTimer = 0;
            }

            //Vector2 endVel = entity.Velocity;
            //endVel += entity.MOVEMENT_SPEED * entity.Velocity * deltaTime;

            //endVel.Y += GRAVITY;

            //entity.Position += endVel;

            // update hitbox
            entity.HitBox = new Rectangle(((int)entity.Position.X - ((int)entity.Size.X / 2)),
                                            ((int)entity.Position.Y - (int)entity.Size.Y / 2),
                                            (int)entity.Size.X,
                                            (int)entity.Size.Y);

            // clamp position to screen bounds
            //world.resolveCollisions(entity.Position, entity.HitBox);
            entity.Position = Vector2.Clamp(entity.Position, entity.Size / 2, TKGame.ScreenSize - entity.Size / 2);

            // The world should own all of the Stage stuff & entities
            // so we would call it here within the PhysicsComponent instead of
            // in the EntitiyManager.

            // GameTime should be owned by World as well.
        }

        private void CheckVerticalWallDistances()
        {
            float playerYPos = Player.Instance.Position.Y;
            Wall closestWall = null;

            foreach(Wall wall in TKGame.levelComponent.GetCurrentStage().StageWalls)
            {
                if(closestWall is null) closestWall = wall;

                if(playerYPos - wall.HitBox.Y < playerYPos - closestWall.HitBox.Y)
                {
                    closestWall = wall;
                }
            }
        }
    }
}
