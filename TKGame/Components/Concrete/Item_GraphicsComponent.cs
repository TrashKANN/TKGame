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
    class Item_GraphicsComponent : GraphicsComponent
    {
        void GraphicsComponent.Update(Entity entity/*, SpriteBatch spriteBatch*/)
        {
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(entity.entityTexture,
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
