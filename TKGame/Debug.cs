using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

// Myra is a library that allows us to add GUI components.
// https://github.com/rds1983/Myra
using Myra;
using Myra.Graphics2D.UI;

namespace TKGame
{
    public class GameDebug
    {
        public static bool DebugMode { private get; set; }
        public static double FPS { get; private set; }
        public static Label FPSText { get; set; }
        public static VerticalStackPanel VSP { get; private set; }
        private static FontSystem FS { get; set; }

        // Readonly is used since static variables can't be const
        private static readonly int DEBUG_FONT_SIZE = 48;
        private static readonly Color DEBUG_COLOR = Color.Gainsboro;

        /// <summary>
        /// Initialize debug elements (text, fonts, etc.)
        /// </summary>
        public static void Initialize()
        {
            DebugMode = false;
            if (FPSText is null)
            {
                FPSText = new Label();
            }
            if(FS is null)
            {
                // FontSystem is kinda like a font-handler. We can use this to retrieve the
                // font data to use in UI components
                byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
                FS = new FontSystem();
                FS.AddFont(ttfData);
            }
        }

        public static void LoadContent()
        {
            // Create a VerticalStackPanel to put all the debug elements in so they look nice
            // without needing to manually position every element
            VSP = new VerticalStackPanel();

            // Configure the FPSText panel
            FPSText.Text = string.Empty;
            FPSText.TextColor = DEBUG_COLOR;
            FPSText.Font = FS.GetFont(DEBUG_FONT_SIZE);
            FPSText.Margin = new Myra.Graphics2D.Thickness(100);
            FPSText.Visible = DebugMode;

            // Add FPSText as a child of the VerticalStackPanel
            VSP.Widgets.Add(FPSText);
        }

        /// <summary>
        /// Updates the debug UI's FPS text using a given GameTime
        /// </summary>
        /// <param name="gameTime"></param>
        public static void UpdateFPS(GameTime gameTime)
        {
            FPS = 1d / gameTime.ElapsedGameTime.TotalSeconds;
        }

        /// <summary>
        /// Toggle the visibility of the debug UI
        /// </summary>
        public static void ToggleVisibility()
        {
            DebugMode = !DebugMode;
            SetComponentVisibility();
        }

        /// <summary>
        /// Sets the visibility of all elements of the debug UI
        /// </summary>
        private static void SetComponentVisibility()
        {
            // TODO: Have some sort of list of all UI elements so we can 
            // use foreach
            FPSText.Visible = DebugMode;
        }

        /// <summary>
        /// Update debug UI elements
        /// </summary>
        public static void Update()
        {
            // Round the FPS to 2 decimal places
            FPSText.Text = $"FPS: {Math.Round(FPS, 2)}";
        }
    }
}
