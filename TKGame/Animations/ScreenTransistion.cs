using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TKGame.BackEnd;

namespace TKGame.Animations
{
    public class ScreenTransition
    {
        #region Member Data
        private Rectangle[] sourceRectangles;

        private int spriteX = 512;
        private int spriteY = 288;
        private int buffer = 1;
        byte currentIndex = 0;
        private float timer = 0;
        private int threshold = 16; // ~1/60th of a second = frame rate
        private bool loop = true;
        #endregion

        /// <summary>
        /// creates ScreenTransition Object
        /// </summary>
        public ScreenTransition () // TODO: Move this transition to it's own child class of ScreenTransition
        {
            int[] row = {   buffer, 
                            spriteY * 1 + buffer * 3, 
                            spriteY * 2 + buffer * 5,
                            spriteY * 3 + buffer * 7,
                            spriteY * 4 + buffer * 9,
                            spriteY * 5 + buffer * 11 };

            int[] col = {   buffer,
                            spriteX * 1 + buffer * 3,
                            spriteX * 2 + buffer * 5 };

            //individual sprite images
            sourceRectangles     = new Rectangle[16];

            sourceRectangles[0]  = new Rectangle(col[0], row[0], spriteX, spriteY);
            sourceRectangles[1]  = new Rectangle(col[1], row[0], spriteX, spriteY);
            sourceRectangles[2]  = new Rectangle(col[2], row[0], spriteX, spriteY);

            sourceRectangles[3]  = new Rectangle(col[0], row[1], spriteX, spriteY);
            sourceRectangles[4]  = new Rectangle(col[1], row[1], spriteX, spriteY);
            sourceRectangles[5]  = new Rectangle(col[2], row[1], spriteX, spriteY);
            
            sourceRectangles[6]  = new Rectangle(col[0], row[2], spriteX, spriteY);
            sourceRectangles[7]  = new Rectangle(col[1], row[2], spriteX, spriteY);
            sourceRectangles[8]  = new Rectangle(col[2], row[2], spriteX, spriteY);
            
            sourceRectangles[9]  = new Rectangle(col[0], row[3], spriteX, spriteY);
            sourceRectangles[10] = new Rectangle(col[1], row[3], spriteX, spriteY);
            sourceRectangles[11] = new Rectangle(col[2], row[3], spriteX, spriteY);
          
            sourceRectangles[12] = new Rectangle(col[0], row[4], spriteX, spriteY);
            sourceRectangles[13] = new Rectangle(col[1], row[4], spriteX, spriteY);
            sourceRectangles[14] = new Rectangle(col[2], row[4], spriteX, spriteY);

            sourceRectangles[15] = new Rectangle(buffer, row[5], spriteX, spriteY);
        }

        /// <summary>
        /// Draws Screen Transition Rectangle the size of game window
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw a rectangle the size of background
            spriteBatch.Draw(Art.TransitionTexture, new Rectangle(0, 0, TKGame.ScreenWidth, TKGame.ScreenHeight), sourceRectangles[currentIndex], Color.White);
        }

        /// <summary>
        /// Updates transition spritesheet through different images 
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            if (timer > threshold) //waits for about half a second and then resets the loop 
            {
                loop = true;
            }

            if (loop) //activates when the spritesheet has incremented all the way through
            {
                //logic to loop through images
                if (timer > threshold) //checks if enough time has passed
                {
                    if (currentIndex < 15)
                    {
                        currentIndex++; //increments through sprite sheet
                    }
                    else
                    {
                        currentIndex = 0; // sets image back to full transparent window
                        loop = false; //stops incrementing at end of sprite sheet
                        TKGame.levelComponent.GetCurrentLevel().isTransitioning = false; //stops transition
                        TKGame.paused = false;
                    }
                    timer = 0; // reset timer
                }
                else
                {
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //increases timer  
                }
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //just increments timer and leaves
            }



            //if (timer > threshold + 800) //waits for about half a second and then resets the loop 
            //    loop = true;

            //if (!loop) //activates when the spritesheet has incremented all the way through
            //{
            //    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //just increments timer and leaves
            //    return;
            //}
            ////logic to loop through images
            //if (timer > threshold) //checks if enough time has passed
            //{
            //    if (currentIndex < 15)
            //        currentIndex++; //increments through sprite sheet
            //    else
            //    {
            //        currentIndex = 0; // sets image back to full transparent window
            //        loop = false; //stops incrementing at end of sprite sheet
            //    }
            //    timer = 0; // reset timer
            //    TKGame.levelComponent.GetCurrentLevel().isTransitioning = false; //stops transition
            //}
            //else
            //    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds; //increases timer
        }
    }
}
