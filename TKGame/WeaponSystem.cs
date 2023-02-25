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
using System.ComponentModel;

namespace TKGame
{
    internal class WeaponSystem
    {
        #region Member Variables
        public static bool visible;
        public static VerticalStackPanel VSP { get; private set; }  //To hold weapon labels

        protected static List<Weapon> weapons;  
        private static List<Widget> widgets;
        private static FontSystem wsFontSystem { get; set; }
        private static Label swordLabel;
        private static Label spearLabel;
        private static Label axeLabel;
        private static readonly int wsFontSize = 50;
        private static readonly Color wsColor = Color.Black;
        #endregion
       /// <summary>
       /// initializes Weapon System UI Elements
       /// </summary>
        public static void Initialize()
        {
            visible = true;
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

            swordLabel.Text = "SWORD";
            swordLabel.TextColor = wsColor;
            swordLabel.Visible = visible;

            spearLabel.Text = "SWORD";
            spearLabel.TextColor = wsColor;
            spearLabel.Visible = visible;

            axeLabel.Text = "SWORD";
            axeLabel.TextColor = wsColor;
            axeLabel.Visible = visible;

            foreach(Widget widget in widgets)
            {
                VSP.Widgets.Add(widget); 
            }
        }
    }
}
