using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Players;

namespace TKGame.PowerUps.RelatedEntities
{
    public class C_Shocked_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        void IGraphicsComponent.Update(Entity entity)
        {
            // if player is looking right then set attack image to right of player
            if (!Player.Instance.isLookingLeft)
            {
                entity.Position = new Vector2(Player.Instance.HitBox.X + 200, Player.Instance.HitBox.Y + 50);
            }
        }
    }
}
