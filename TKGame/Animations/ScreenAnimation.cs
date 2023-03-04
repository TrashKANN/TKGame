using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Animations
{
    public class ScreenAnimation
    {
        private readonly int Zero = 0;
        public enum screenStatus
        {
            ready,
            fadeIn,
            waiting,
            fadeOut,
            notReady
        }
        private screenStatus currentStatus;
        public screenStatus CurrentStatus { get { return currentStatus; } }

        private Texture2D screenImage;
        private Rectangle screenBounds;
        private Color color;
        private byte fade;
        private float timer, inTime, waitTime, outTime;
        private float startToWaitTime { get { return inTime; } }
        private float startToFadeOutTime { get { return inTime + waitTime; } }
        private float startToEndTime { get { return inTime + waitTime + outTime; } }
        
        public ScreenAnimation(GraphicsDevice graphicsDevice)
        {
            float min = 0.1f;
            this.screenImage = new Texture2D(graphicsDevice, 1600, 900);
            this.inTime = Math.Max(inTime, min); 
            this.waitTime = Math.Max(waitTime, min);
            this.outTime = Math.Max(outTime, min);
            Init();
        }

        public void Init()
        {
            fade = 0;
            timer = 0;
            color = new Color(Zero, Zero, Zero);
            currentStatus = screenStatus.ready;
        }

        public void Update(GameTime gameTime)
        {
            if (timer < startToWaitTime) //checks if fading in
            {
                fade = (byte)((byte.MaxValue * timer) / startToWaitTime);
                if (currentStatus != screenStatus.fadeIn) currentStatus = screenStatus.fadeIn;
            }
            else if (timer < startToFadeOutTime)    //checks if waiting
            {
                if (color.A < byte.MaxValue) color.A = byte.MaxValue;
                if (currentStatus != screenStatus.waiting) currentStatus = screenStatus.waiting;
            }
            else if (timer < startToEndTime)    //checks if fading out
            {
                fade = (byte)(byte.MaxValue - ((byte.MaxValue * (timer - startToFadeOutTime)) / outTime));
                if (currentStatus != screenStatus.fadeOut) currentStatus = screenStatus.fadeOut;
            }
            else// Either ready or not ready
            {
                fade = byte.MinValue;
                if (currentStatus != screenStatus.notReady) currentStatus = screenStatus.notReady;
            }

           //Updates Color and time;
            color = new Color(fade, fade, fade);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(screenImage, new Vector2(), color);
        }

        

    }
}
