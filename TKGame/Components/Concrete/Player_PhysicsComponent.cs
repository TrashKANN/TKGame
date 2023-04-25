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
        private const int GRAVITY = 500;
        void PhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Player player = (Player)entity;

            player.Velocity.X += player.MOVEMENT_SPEED * player.Velocity.X * deltaTime;
            player.Position.X += player.Velocity.X * deltaTime;
            player.Position.Y += player.Velocity.Y * deltaTime;

            if(player.IsOnGround && player.FramesSinceJump > 30)
            {
                player.Velocity.Y = 0;
                player.FramesSinceJump = 0;
            } 
            else
            {
                player.Velocity.Y += GRAVITY * deltaTime;
            }

            //Vector2 endVel = entity.Velocity;
            //endVel += entity.MOVEMENT_SPEED * entity.Velocity * deltaTime;

            //endVel.Y += GRAVITY;

            //entity.Position += endVel;

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
