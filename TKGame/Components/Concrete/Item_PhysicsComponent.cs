using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;

namespace TKGame.Components.Concrete
{
    internal class Item_PhysicsComponent : PhysicsComponent
    {
        void PhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/)
        {
            Player player = Player.Instance;

            if (player != null)
            {
                Vector2 playerPosition = player.Position;
                // if player is within pickup range
                if (playerPosition.X == entity.Position.X &&
                    playerPosition.Y >= entity.Position.Y - 12 &&
                    playerPosition.Y <= entity.Position.Y + 12)
                {
                    // pickup item
                }
            }
        }
    }
}
