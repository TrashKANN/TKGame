using Microsoft.Xna.Framework;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;

namespace TKGame.Players.Components
{
    internal class C_Player_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        private static readonly float GRAVITY = 1100.0f;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            Player player = entity as Player;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Position += player.Velocity * deltaTime;

            resolveVerticalCollision(player, deltaTime);

            if (player.CollidedHorizontally)
            {
                // TODO
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

        // Probably move this to Entity.cs
        private void resolveVerticalCollision(Entity entity, float deltaTime)
        {
            bool wasGrounded = entity.IsOnGround; // Store the previous grounded state

            if (entity.CollidedVertically)
            {
                // Ground collision
                if (entity.Velocity.Y > 0)
                {
                    entity.IsOnGround = true;
                }
                else
                {
                    entity.IsOnGround = false;
                }
                entity.Velocity.Y = 0;
                entity.CollidedVertically = false;
            }

            // Apply gravity only if the entity is not on the ground and was not previously grounded
            if (!isOnGround(entity) && !wasGrounded)
            {
                entity.Velocity.Y += GRAVITY * deltaTime;
            }
        }



        private bool isOnGround(Entity player)
        {
            Rectangle extendedHitBox = new Rectangle(player.HitBox.Left + 1, player.HitBox.Bottom, player.HitBox.Width - 2, 10);

            foreach (Wall wall in TKGame.levelComponent.GetCurrentLevel().currentStage.StageWalls)
            {
                if (extendedHitBox.Intersects(wall.HitBox))
                {
                    // Only consider the player grounded if the collision is from below
                    if (player.Velocity.Y >= 0)
                    {
                        player.IsOnGround = true;
                        return true;
                    }
                    else if (player.Velocity.Y < 0 && player is Player)
                    {
                        // Check if the top of the wall is within reach to allow climbing
                        if (player.HitBox.Bottom - wall.HitBox.Top <= player.Velocity.Y)
                        {
                            player.IsOnGround = true;
                            player.Velocity.Y = 0;
                            return true;
                        }
                    }
                }
            }

            player.IsOnGround = false; // Reset grounded state if no collision is found
            return false;
        }


    }
}
