using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.BackEnd
{
    public class Background
    {
        // Variables to hold Background Image
        /// <summary>
        /// Rectangle to hold the Background Texture
        /// </summary>
        public Rectangle BackgroundRect { get; set; }

        /// <summary>
        /// Texture to hold the Background Image
        /// </summary>
        public Texture2D BackgroundTexture { get; set; }
        public string BackgroundName { get; set; }

        /// <summary>
        /// Creates a new Rectangle and Texture2D the size of the provided width and height to store the image.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Background(int width, int height, string backgroundName)
        {
            BackgroundTexture = new Texture2D(TKGame.Graphics.GraphicsDevice, width, height);
            BackgroundRect = new Rectangle(0, 0, width, height);
            this.BackgroundName = backgroundName;
        }

    }
}
