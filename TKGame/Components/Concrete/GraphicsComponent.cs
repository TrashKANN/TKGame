using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Concrete
{
    public class GraphicsComponent
    {
        internal void Update(Entity entity, SpriteBatch spriteBatch)
        {
            if (entity.Velocity.X > 0)
            {
                entity.Orientation = SpriteEffects.None;
            }
            else if (entity.Velocity.X < 0)
            {
                entity.Orientation = SpriteEffects.FlipHorizontally;
            }

            // Moved this entirely out of Entity and into this component.
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            spriteBatch.Draw(entity.entityTexture, 
                                entity.Position, 
                                null, 
                                entity.color, 
                                0, 
                                entity.Size / 2f,
                                1f,
                                entity.Orientation,
                                0);
            spriteBatch.End();
        }
    }
}
