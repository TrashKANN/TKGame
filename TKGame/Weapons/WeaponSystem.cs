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


namespace TKGame.Weapons
{
    public class WeaponSystem
    {
        #region Member Variables


        // Vertical stack panel that will hold all the UI elements for Weapon information
        public static VerticalStackPanel VSP { get; private set; }

        // FontSystems allow us to use fonts with Myra
        private static FontSystem weaponFontSystem { get; set; }
        private static Label swordLabel { get; set; }
        private static Label spearLabel { get; set; }
        private static Label axeLabel { get; set; }
        private static Texture2D weaponTexture { get; set; }
        private static List<Widget> UIWidgets { get; set; }

        // Readonly is used since static variables can't be const
        private static readonly int WEAPON_FONT_SIZE = 48;
        private static readonly Color WEAPON_TEXT_COLOR = Color.Gainsboro;


        // Readonly weapon labels to remove hardcoded strings
        private static readonly string swordText = "Sword";
        private static readonly string spearText = "Spear";
        private static readonly string axeText = "Axe";

        List<string> weaponNames = new List<string>() { "Sword", "Spear", "Bow" };
        List<bool> weaponBools = new List<bool>() { false, false, false };



        #endregion


        /// <summary>
        /// Initialize Weapon System UI Elements
        /// </summary>
        public static void Initialize()
        {

            swordLabel = new Label();
            spearLabel = new Label();
            axeLabel = new Label();

            // Create a VerticalStackPanel to put all the debug elements in so they look nice
            // without needing to manually position every element
            VSP = new VerticalStackPanel();

            UIWidgets = new List<Widget>()
            {
                swordLabel, spearLabel, axeLabel
            };

            // FontSystem is kinda like a font-handler. We can use this to retrieve the
            // font data to use in UI components
            byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
            weaponFontSystem = new FontSystem();
            weaponFontSystem.AddFont(ttfData);
        }

        /// <summary>
        /// Load/Stylize all Weapon System UI elements
        /// </summary>
        public static void LoadContent(VerticalStackPanel VSP)
        {
            //VSP.Margin = new Myra.Graphics2D.Thickness(100, 100, 0, 0);

            // Configure labels
            // Sword Label
            config(VSP, true, swordLabel, swordText);

            // Spear Label
            config(VSP, true, spearLabel, spearText);

            // Axe Label
            config(VSP, true, axeLabel, axeText);

        }
       

        private static void config(VerticalStackPanel VSP, bool isActive, Label label, string text)
        {
            label.Text = text;
            if(isActive)
                label.TextColor = Color.Red;
            else
                label.TextColor= WEAPON_TEXT_COLOR;
            label.Font = weaponFontSystem.GetFont(WEAPON_FONT_SIZE);
            label.Visible = true;
            VSP.Widgets.Add(label);
        }
        /// <summary>
        /// Update debug UI elements
        /// </summary>
        public static void Update()
        {
            // TODO: Setup color change with active weapon
            swordLabel.Text = swordText;
            spearLabel.Text = spearText;
            axeLabel.Text = axeText;
        }

        public static void LoadContent()
        {

        }
    }
}
