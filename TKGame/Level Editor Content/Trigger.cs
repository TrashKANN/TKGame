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
        #region Member Variables
        private static readonly int screenWidth = 1600;
        private static readonly int screenHeight = 900;
        private static readonly int xBuffer = 100;
        private static readonly int yMax = 825;
        private static readonly int yMin = 600;
        public Rectangle rectangle { get; set; }
        public Texture2D texture { get; set; }

        public string leftStage { get; set; }
        public string rightStage { get; set; }
        public string currentStage { get; set; }
        #endregion

        #region Trigger
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
            currentStage = "defaultStage.json";
            leftStage = "leftStage.json";
            rightStage = "rightStage.json";
        }
        #endregion

        #region checkTriggers
        /// <summary>
        /// Checks if player is on the left trigger
        /// </summary>
        /// <returns></returns>
        public bool checkLeftTrigger(Player player)
        {
            bool retval = false;

            if (player.Position.X <= xBuffer && player.Position.Y <= yMax && player.Position.Y >= yMin)
                retval = true;

            return retval;
        }

        /// <summary>
        /// Checks if player is on the right trigger
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool checkRightTrigger(Player player)
        {
            bool retval = false;

            if (player.Position.X <= screenWidth && player.Position.X >= screenWidth - xBuffer && player.Position.Y <= yMax && player.Position.Y >= yMin)
                retval = true;

            return retval;
        }
        #endregion
    }
}
