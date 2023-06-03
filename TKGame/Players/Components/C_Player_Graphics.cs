using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;

namespace TKGame.Players.Components
{
    internal class C_Player_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        IGraphicsComponent weaponGraphics = new C_Weapon_Graphics();
        void IGraphicsComponent.Update(Entity entity)
        {
            Player player = entity as Player;

            weaponGraphics.Update(entity);
            if (player.Velocity.X > 0)
            {
                player.Orientation = SpriteEffects.None;
            }
            else if (player.Velocity.X < 0)
            {
                player.Orientation = SpriteEffects.FlipHorizontally;
            }

            weaponGraphics.Update(player);
            if (player.Velocity.X > 0)
            {
                player.Orientation = SpriteEffects.None;
            }
            else if (player.Velocity.X < 0)
            {
                player.Orientation = SpriteEffects.FlipHorizontally;
            }

            if (player.isCrouched)
            {
                entity.entityTexture = Art.PlayerRightCrouch;
                entity.Position.Y += 3;
            }
            else
                entity.entityTexture = Art.PlayerTexture;
        }
    }
}
