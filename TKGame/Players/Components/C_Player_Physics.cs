using Microsoft.Xna.Framework;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;

namespace TKGame.Players.Components
{
    internal class C_Player_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/) // The &reference isn't working.
        {
            Player player = entity as Player;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            player.Position += player.Velocity * deltaTime;

            player.resolveVerticalCollision(player, deltaTime);

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
            entity.isDead();
        }

        // Probably move this to Entity.cs



    }
}
