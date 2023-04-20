using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;

namespace TKGame
{
    class PotionItem : Item
    {
        private PhysicsComponent potionItemPhysics = new Item_PhysicsComponent();
        private GraphicsComponent potionItemGraphics = new Item_GraphicsComponent();
        public PotionItem()
        {
            entityTexture = Art.PotionItemTexture;
            Position = new Vector2(1200, 840); // hard coded spawn position at the moment
        }

        public override void Update(GameTime gameTime)
        {
            potionItemPhysics.Update(this, gameTime/*, world*/);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            potionItemGraphics.Update(this, spriteBatch);
        }
    }
}

