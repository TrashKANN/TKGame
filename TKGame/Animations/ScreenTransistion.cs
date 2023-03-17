using Microsoft.VisualBasic.FileIO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using StbImageSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TKGame.BackEnd;

namespace TKGame.Animations
{
    public class ScreenTransition
    {
        #region Member Data
        private Rectangle[] sourceRectangles;

        private static readonly int screenWidth = 1600;
        private static readonly int screenHeight = 900;
        private int spriteX = 509;
        private int spriteY = 285;
        private int buffer = 5;
        byte currentIndex = 0;
        private float timer = 0;
        private int threshold = 50;
        private bool loop = true;



        #endregion
        /// <summary>
        /// creates ScreenTransition Object
        /// </summary>
        /// <param name="graphicsDevice"></param>
        public ScreenTransition (GraphicsDevice graphicsDevice)
        {
            //individual sprite images
            sourceRectangles = new Rectangle[16];
            sourceRectangles[1] = new Rectangle(buffer, buffer, spriteX, spriteY);
            sourceRectangles[2] = new Rectangle(spriteX +(buffer*2), buffer, spriteX, spriteY);
            sourceRectangles[3] = new Rectangle(buffer, spriteY + (buffer * 2), spriteX, spriteY);
            sourceRectangles[4] = new Rectangle(spriteX + (buffer*2), spriteY + (buffer * 2), spriteX, spriteY);
            sourceRectangles[5] = new Rectangle((spriteX  * 2) + (buffer * 2), spriteY + (buffer * 2), spriteX, spriteY);
            sourceRectangles[6] = new Rectangle(buffer, (spriteY *2) + (buffer), spriteX, spriteY);
            sourceRectangles[7] = new Rectangle((spriteX + buffer) + 2, (spriteY + (buffer * 2)) * 2, spriteX, spriteY);
            sourceRectangles[8] = new Rectangle((spriteX * 2) + (buffer * 2), (spriteY + (buffer * 2)) * 2, spriteX, spriteY);
            sourceRectangles[9] = new Rectangle(buffer, (spriteY * 3) + (2 * buffer) , spriteX, spriteY);
            sourceRectangles[10] = new Rectangle((spriteX + buffer) + 2, (spriteY * 3) + (2 * buffer), spriteX, spriteY);
            sourceRectangles[11] = new Rectangle((spriteX * 2) + (buffer * 2), (spriteY * 3) + (6 * buffer), spriteX, spriteY);
            sourceRectangles[12] = new Rectangle(buffer, (spriteY + buffer) * 4, spriteX, spriteY);
            sourceRectangles[13] = new Rectangle((spriteX + buffer) + 2, (spriteY + buffer) * 4, spriteX, spriteY);
            sourceRectangles[14] = new Rectangle((spriteX * 2) + (buffer * 2), (spriteY + buffer) * 4, spriteX, spriteY);
            sourceRectangles[15] = new Rectangle(buffer, (spriteY + buffer) * 5, spriteX, spriteY);
            sourceRectangles[0] = new Rectangle((spriteX + buffer) + 2, (spriteY +buffer) * 5 + 16, spriteX, spriteY);

        }
        /// <summary>
        /// Draws Screen Transition Rectangle the size of game window
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw a rectangle the size of background
            spriteBatch.Draw(Art.LoadTexture, new Rectangle(0, 0, screenWidth, screenHeight), sourceRectangles[currentIndex], Color.White);
        }

        /// <summary>
        /// Updates transition spritesheet through different images 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (timer > threshold + 500) //waits for about half a second and then resets the loop 
                loop = true;

                if (!loop) //activates when the spritesheet has incremented all the way through
                {
                    timer += (float) gameTime.ElapsedGameTime.TotalMilliseconds; //just increments timer and leaves
                    return;
                }
                //logic to loop through images
                if (timer > threshold) //checks if enough time has passed
                {
                    if (currentIndex < 15)
                        currentIndex++; //increments through sprite sheet
                    else
                    {
                        currentIndex = 0; // sets image back to full transparent window
                        loop = false; //stops incrementing at end of sprite sheet
                    }
                    timer = 0; // reset timer
                }
                else
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //increases timer  
        }


    }
}
