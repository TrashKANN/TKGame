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
        private IPhysicsComponent potionItemPhysics = new C_Item_Physics();
        private IGraphicsComponent potionItemGraphics = new C_Item_Graphics();
        static Random rand = new Random();
        int x = rand.Next(900, 1200);
        public PotionItem()
        {
            entityTexture = Art.PotionItemTexture;
            Position = new Vector2(x, 839); // random x-coor spawn
            HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
        }

        public override void Update(GameTime gameTime)
        {
            potionItemPhysics.Update(this, gameTime/*, world*/);
        }

        public void Draw()
        {
            potionItemGraphics.Update(this);
        }
    }
}

