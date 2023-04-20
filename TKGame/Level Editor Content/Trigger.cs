﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Animations;

namespace TKGame.Level_Editor_Content
{
    public class Trigger
    {
        #region Member Variables
        public Rectangle HitBox { get; set; }
        public Texture2D Texture { get; set; }
        public string Action { get; set; }

        private ScreenTransition transition;

        private DateTime timeSinceTriggered = DateTime.MinValue;
        private TimeSpan timeBetweenTriggers = TimeSpan.FromMilliseconds(5000);
        #endregion

        #region Trigger
        /// <summary>
        /// Creates Trigger Objects given starting coordinates, width, height
        /// </summary>
        /// <param name="xpos"></param>
        /// <param name="ypos"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Trigger(int xpos, int ypos, int width, int height, string action)
        {
            HitBox = new Rectangle(xpos, ypos, width, height);
            Texture = new Texture2D(TKGame.Graphics.GraphicsDevice, 1, 1);
            Texture.SetData(new Color[] { Color.Yellow });
            Action = action;
            transition = new ScreenTransition();
        }
        #endregion

        public bool checkTrigger()
        {
            if (HitBox.Intersects(Player.Instance.HitBox) 
                && (DateTime.Now - timeSinceTriggered >= timeBetweenTriggers))
            {
                timeSinceTriggered= DateTime.Now;
                return true;
            }
            else
                return false;
        }
    }
}
