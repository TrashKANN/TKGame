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
using TKGame.Items.Potion.Components;
using TKGame.PowerUps;
using TKGame.Status_Effects;

namespace TKGame.Items.Poison
{
    public class PoisonItem : Item
    {
        public PoisonItem() { }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
