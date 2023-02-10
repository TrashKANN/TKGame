using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using System.IO;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;

namespace TKGame
{
    internal class WeaponSystem
    {
        #region
        public static VerticalStackPanel VSP { get; private set; }  //To hold weapon labels

        protected static List<Weapon> weapons;  
        private static List<Widget> widgets;
        private static FontSystem wsFontSystem { get; set; }
        private static Label swordLabel;
        private static Label spearLabel;
        private static Label axeLabel;
        #endregion
       /// <summary>
       /// initializes Weapon System UI Elements
       /// </summary>
        public static void Initialize()
        {
            swordLabel = new Label();
            spearLabel = new Label();
            axeLabel = new Label();

            VSP = new VerticalStackPanel();

            widgets = new List<Widget>()
            {
                swordLabel, spearLabel, axeLabel
            };
        }

        public static void LoadContent()
        {
            VSP.Margin = new Myra.Graphics2D.Thickness(100, 100, 0, 0);

            

        }
    }
}
