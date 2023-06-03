using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;

namespace TKGame.UI
{
    public class PauseMenu : IMenu
    {
        private static TextButton mainMenuButton;
        private static TextButton exitButton;
        private static FontSystem pauseMenuFontSystem;
        private static Label pauseLabel;

        private static readonly int PAUSE_MENU_FONT_SIZE = 72;

        private static readonly Color PAUSE_MENU_BACKGROUND_COLOR = new Color(0x29, 0x29, 0x29, 0xC0); // Dark gray with alpha of 0xC0
        private static readonly Vector4 PAUSE_LABEL_TEXT_COLOR_VECTOR = new Vector4(140f / 255, 20f / 255, 20f / 255, 1f);
        private static readonly Color PAUSE_MENU_TEXT_COLOR = Color.Gold;
        private static readonly Color PAUSE_MENU_BUTTON_BACKGROUND_COLOR = Color.DimGray;
        private static readonly Color PAUSE_MENU_MAIN_MENU_HOVER_TEXT_COLOR = Color.Gold;
        private static readonly Color PAUSE_MENU_EXIT_HOVER_TEXT_COLOR = Color.Red;

        private static readonly string MAIN_MENU_BUTTON_STYLE_NAME = "mainmenu";
        private static readonly string EXIT_BUTTON_STYLE_NAME = "exit";

        private static readonly Dictionary<string, Color> textColorDict = new Dictionary<string, Color>()
        {
            { MAIN_MENU_BUTTON_STYLE_NAME, PAUSE_MENU_MAIN_MENU_HOVER_TEXT_COLOR },
            { EXIT_BUTTON_STYLE_NAME,      PAUSE_MENU_EXIT_HOVER_TEXT_COLOR      }
        };

        private static HorizontalStackPanel hsp;
        private static VerticalStackPanel vsp;
        private static Grid pauseMenuGrid { get; set; }
        public IMultipleItemsContainer Container { get { return pauseMenuGrid; } }


        public PauseMenu() 
        {
            Initalize();
            LoadContent();
        }

        private static void Initalize()
        {
            byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
            pauseMenuFontSystem = new FontSystem();
            pauseMenuFontSystem.AddFont(ttfData);

            pauseMenuGrid = new Grid()
            {
                Background = new SolidBrush(PAUSE_MENU_BACKGROUND_COLOR)
            };

            vsp = new VerticalStackPanel()
            {
                GridRow = 0,
                GridColumn = 0,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            hsp = new HorizontalStackPanel();

            mainMenuButton = ConstructMenuButton("Main Menu", MAIN_MENU_BUTTON_STYLE_NAME, 1, 1, 400);
            exitButton = ConstructMenuButton("Exit", EXIT_BUTTON_STYLE_NAME, 1, 1, 400);

            pauseLabel = new Label()
            {
                Text = "PAUSED",
                TextColor = new Color(PAUSE_LABEL_TEXT_COLOR_VECTOR),
                TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center,
                Font = pauseMenuFontSystem.GetFont(PAUSE_MENU_FONT_SIZE),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment= HorizontalAlignment.Center
            };
        }

        private static void LoadContent()
        {
            mainMenuButton.Margin = new Myra.Graphics2D.Thickness(0, 0, 20, 0);
            mainMenuButton.MouseEntered += OnMouseEnterButton;
            mainMenuButton.MouseLeft += OnMouseLeaveButton;

            exitButton.Margin = new Myra.Graphics2D.Thickness(20, 0, 0, 0);
            exitButton.MouseEntered += OnMouseEnterButton;
            exitButton.MouseLeft -= OnMouseLeaveButton;

            mainMenuButton.TouchDown += (sender, eventArgs) =>
            {
                TKGame.paused = true;
                MenuHandler.SwitchToMenu(MenuHandler.MenuState.MAIN_MENU);
            };

            exitButton.TouchDown += (sender, eventArgs) =>
            {
                TKGame.Instance.ExitGame();
            };

            hsp.Widgets.Add(mainMenuButton);
            hsp.Widgets.Add(exitButton);
            vsp.Widgets.Add(pauseLabel);
            vsp.Widgets.Add(hsp);
            pauseMenuGrid.Widgets.Add(vsp);
        }

        public static void Update()
        {
            pauseLabel.TextColor = new Color(
                PAUSE_LABEL_TEXT_COLOR_VECTOR.X,
                PAUSE_LABEL_TEXT_COLOR_VECTOR.Y,
                PAUSE_LABEL_TEXT_COLOR_VECTOR.Z,
                (float)Math.Abs(Math.Sin(TKGame.GameTime.TotalGameTime.TotalSeconds))
            );
            //Debug.WriteLine($"{pauseLabel.TextColor.R} {pauseLabel.TextColor.G} {pauseLabel.TextColor.B} {pauseLabel.TextColor.A}");
        }

        private static TextButton ConstructMenuButton(string text, string styleName, int gridRow, int gridCol, int width)
        {
            TextButton newButton = new TextButton();
            newButton.Id = styleName;
            newButton.Text = text;
            newButton.Width = width;
            newButton.Height = 100;
            newButton.Font = pauseMenuFontSystem.GetFont(PAUSE_MENU_FONT_SIZE);
            newButton.Background = new SolidBrush(PAUSE_MENU_BUTTON_BACKGROUND_COLOR);
            newButton.TextColor = PAUSE_MENU_TEXT_COLOR;
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

            ((TextButton)obj).TextColor = PAUSE_MENU_TEXT_COLOR;
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

            TextButton button = (TextButton)obj;
            Color color;

            if (textColorDict.TryGetValue(button.Id, out color))
                button.TextColor = color;
        }
    }
}
