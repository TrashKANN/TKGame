using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Level_Editor_Content
{
    internal class Trigger
    {
        public Rectangle rectangle { get; set; }
        public Texture2D texture { get; set; }

        public Stage leftStage { get; set; }
        public Stage rightStage { get; set; }

        /// <summary>
        /// Creates Trigger Objects given starting coordinates, width, height, and a GraphicsDevice object
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="graphicsDevice"></param>
        public Trigger(int x, int y, int width, int height, GraphicsDevice graphicsDevice)
        {
            rectangle = new Rectangle(x, y, width, height);
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.Yellow });
        }
        
    }
}
