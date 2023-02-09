using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    internal class Trigger
    {
        public Rectangle rectangle { get; set; }
        public Texture2D texture { get; set; }

        public Trigger(int x, int y, int width, int height, GraphicsDevice graphicsDevice)
        {
            rectangle = new Rectangle(x, y, width, height);
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Yellow });
        }
    }
}
