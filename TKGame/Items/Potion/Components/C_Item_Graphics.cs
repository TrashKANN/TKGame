using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.World;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;

namespace TKGame.Items.Potion.Components
{
    class C_Item_Graphics : IGraphicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Graphics;
        void IGraphicsComponent.Update(Entity entity/*, SpriteBatch spriteBatch*/)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            TKGame.SpriteBatch.Draw(entity.entityTexture,
                                entity.Position,
                                null,
                                entity.color,
                                0,
                                entity.Size / 2f,
                                1f,
                                entity.Orientation,
                                0);
            //spriteBatch.End();
        }
    }
}
