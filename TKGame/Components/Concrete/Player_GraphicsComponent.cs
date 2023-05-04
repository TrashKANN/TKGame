using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    internal class Player_GraphicsComponent : GraphicsComponent
    {
        GraphicsComponent weaponGraphics = new Weapon_GraphicsComponent();
        void GraphicsComponent.Update(Entity entity)
        {
            //Player player = entity as Player;
            Player player = Player.Instance;

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
                player.entityTexture = Art.PlayerRightCrouch;
                player.Position.Y += 3;
            }
            else
                player.entityTexture = Art.PlayerTexture;

            // Moved this entirely out of Entity and into this component.
            //TKGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            //TKGame.SpriteBatch.Draw(entity.entityTexture,
            //                    entity.Position,
            //                    null,
            //                    entity.color,
            //                    0,
            //                    entity.Size / 2f,
            //                    1f,
            //                    entity.Orientation,
            //                    0);
            //TKGame.SpriteBatch.End();
        }
    }
}
