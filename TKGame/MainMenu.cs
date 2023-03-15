using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Level_Editor_Content;

namespace TKGame
{
    public class MainMenu
    {
        public static bool IsEnabled { get; private set; }
        private static TextButton playButton;
        private static TextButton exitButton;
        private static Desktop desktop;
        private static Grid mainMenuGrid;
        private static Label background;
        private static SolidBrush red;
        private static FontSystem mainMenuFontSystem;

        public static void Initialize(Desktop desktop, TKGame game)
        {
            byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
            mainMenuFontSystem = new FontSystem();
            mainMenuFontSystem.AddFont(ttfData);

            IsEnabled = true;
            red = new SolidBrush(Color.Red);
            mainMenuGrid = new Grid() { ShowGridLines= true };
            background= new Label();

            MainMenu.desktop = desktop;

            playButton = ConstructMenuButton("Play", 0, 0, 400);
            exitButton = ConstructMenuButton("Exit", 1, 0, 400);
            playButton.TouchDown += (sender, eventArgs) =>
            {
                TKGame.paused = false;
                DisableMainMenu();
            };

            exitButton.TouchDown += (sender, eventArgs) => 
            {
                LevelEditor.SaveStageDataToJSON(game.currentStage, "auto_saved_stage_data");
                game.Exit();
            }; 

            
        }

        public static void LoadContent()
        {
            mainMenuGrid.ColumnsProportions.Add(new Proportion() { Type = ProportionType.Part });
            
            for (int i = 0; i < 2; i++)
            {
                mainMenuGrid.RowsProportions.Add(new Proportion() { Type = ProportionType.Part });
            }

            mainMenuGrid.Widgets.Add(playButton);
            mainMenuGrid.Widgets.Add(exitButton);

            desktop.Root = mainMenuGrid;
        }

        public static void Update()
        {

        }

        public static void DisableMainMenu()
        {
            foreach (var widget in mainMenuGrid.Widgets)
            {
                widget.Enabled = false;
                widget.Visible = false;
            }
        }

        private static TextButton ConstructMenuButton(string text, int gridRow, int gridCol, int width)
        {
            TextButton newButton = new TextButton();
            newButton.Text = text;
            newButton.GridColumn = gridCol;
            newButton.GridRow = gridRow;
            newButton.Width = width;
            newButton.Height = 100;
            newButton.Background = red;
            newButton.HorizontalAlignment = HorizontalAlignment.Center;
            newButton.VerticalAlignment = VerticalAlignment.Center;
            newButton.Font = mainMenuFontSystem.GetFont(72);
            return newButton;
        }
    }
}
