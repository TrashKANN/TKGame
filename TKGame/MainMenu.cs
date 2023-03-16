using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
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
        private static FontSystem mainMenuFontSystem;

        private static readonly Color MAIN_MENU_TEXT_COLOR = Color.Gold;
        private static readonly Color MAIN_MENU_BACKGROUND_COLOR = Color.DimGray;
        private static readonly Color MAIN_MENU_PLAY_HOVER_TEXT_COLOR = Color.Lime;
        private static readonly Color MAIN_MENU_EXIT_HOVER_TEXT_COLOR = Color.Red;

        private static readonly string playButtonStyleName = "play";
        private static readonly string exitButtonStyleName = "exit";

        private static readonly Dictionary<string, Color> textColorDict = new Dictionary<string, Color>
        {
            { playButtonStyleName, MAIN_MENU_PLAY_HOVER_TEXT_COLOR },
            { exitButtonStyleName, MAIN_MENU_EXIT_HOVER_TEXT_COLOR }
        };

        public static void Initialize(Desktop desktop, TKGame game)
        {
            byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
            mainMenuFontSystem = new FontSystem();
            mainMenuFontSystem.AddFont(ttfData);

            IsEnabled = true;
            mainMenuGrid = new Grid();

            MainMenu.desktop = desktop;

            playButton = ConstructMenuButton("Play", playButtonStyleName, 0, 0, 400);
            playButton.MouseEntered += OnMouseEnterButton;
            playButton.MouseLeft += OnMouseLeaveButton;


            exitButton = ConstructMenuButton("Exit", exitButtonStyleName, 1, 0, 400);
            exitButton.MouseEntered += OnMouseEnterButton;
            exitButton.MouseLeft += OnMouseLeaveButton;


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

        private static TextButton ConstructMenuButton(string text, string styleName, int gridRow, int gridCol, int width)
        {
            TextButton newButton = new TextButton();
            newButton.Id = styleName;
            newButton.Text = text;
            newButton.GridColumn = gridCol;
            newButton.GridRow = gridRow;
            newButton.Width = width;
            newButton.Height = 100;
            newButton.HorizontalAlignment = HorizontalAlignment.Center;
            newButton.VerticalAlignment = VerticalAlignment.Center;
            newButton.Font = mainMenuFontSystem.GetFont(72);
            newButton.Background = new SolidBrush(MAIN_MENU_BACKGROUND_COLOR);
            newButton.TextColor = MAIN_MENU_TEXT_COLOR;
            return newButton;
        }

        /// <summary>
        /// Changes button text color to the original color when the mouse exits the button's area.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="e"></param>
        private static void OnMouseLeaveButton(object obj, EventArgs e)
        {
            if (obj is not TextButton) throw new Exception("obj is not of type TextButton");

            ((TextButton) obj).TextColor = MAIN_MENU_TEXT_COLOR;
        }

        /// <summary>
        /// Changes button text color when mouse hovers over button. Color is determined from
        /// textColorDict.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        /// <exception cref="Exception"></exception>
        private static void OnMouseEnterButton(object obj, EventArgs e) 
        {
            if (obj is not TextButton) throw new Exception("obj is not of type TextButton");
            
            TextButton b = (TextButton)obj;
            Color c;

            if(textColorDict.TryGetValue(b.Id, out c))
                b.TextColor = c;
        }
    }
}
