using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Players;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.PowerUps.RelatedEntities
{
    public class C_FireBall_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        void IGraphicsComponent.Update(Entity entity)
        {
            if (Player.Instance.isLookingLeft)
            {
                entity.Orientation = SpriteEffects.FlipHorizontally;
            }
            else
            {
                entity.Orientation = SpriteEffects.None;
            }
        }
    }
}
