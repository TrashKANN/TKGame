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
        private static readonly int DEBUG_FONT_SIZE = 48;
        private static readonly Color DEBUG_COLOR = Color.Gainsboro;

        public static void Initialize()
        {
            DebugMode = false;
            if (FPSText is null)
            {
                FPSText = new Label();
            }
            if(FS is null)
            {
                byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
                FS = new FontSystem();
                FS.AddFont(ttfData);
            }
        }

        public static void LoadContent()
        {

            VSP = new VerticalStackPanel();

            FPSText.Text = string.Empty;
            FPSText.TextColor = DEBUG_COLOR;
            FPSText.Font = FS.GetFont(DEBUG_FONT_SIZE);
            //FPSText.Width = 500;
            FPSText.Margin = new Myra.Graphics2D.Thickness(100);
            FPSText.Visible = DebugMode;

            VSP.Widgets.Add(FPSText);
        }

        public static void UpdateFPS(GameTime gameTime)
        {
            FPS = 1d / gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void ToggleVisibility()
        {
            DebugMode = !DebugMode;
            SetComponentVisibility();
        }

        private static void SetComponentVisibility()
        {
            FPSText.Visible = DebugMode;
        }

        public static void Update()
        {
            FPSText.Text = $"FPS: {Math.Round(FPS)}";
        }
    }
}
