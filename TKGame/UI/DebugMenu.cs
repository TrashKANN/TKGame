using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using TKGame.BackEnd;
using TKGame.Players;

namespace TKGame.UI
{
    public class DebugMenu : IMenu
    {
        private static VerticalStackPanel VSP { get; set; }

        private static Dictionary<Keys, Label> keyboardLabelDict = new Dictionary<Keys, Label>()
        {
            { Keys.W,     new Label() { Text = "W"     } },
            { Keys.A,     new Label() { Text = "A"     } },
            { Keys.S,     new Label() { Text = "S"     } },
            { Keys.D,     new Label() { Text = "D"     } },
            { Keys.Space, new Label() { Text = "Space" } }
        };

        //Spacers to separate VSP
        private static Label spacer1 { get; set; }
        private static Label spacer2 { get; set; }


        // FontSystems allow us to use fonts with Myra
        private static FontSystem DebugFontSystem { get; set; }
        private static Label FPSText { get; set; }
        private static Label PlayerPosText { get; set; }
        private static Label PlayerVelText { get; set; }

        private static Grid KeyboardGrid { get; set; }
        private static SolidBrush ActiveBackgroundBrush { get; set; }
        private static SolidBrush InactiveBackgroundBrush { get; set; }
        private static List<Widget> UIWidgets { get; set; }

        public IMultipleItemsContainer Container { get { return VSP; } }

        // Readonly is used since static variables can't be const
        private static readonly int DEBUG_FONT_SIZE = 48;
        private static readonly Color DEBUG_TEXT_COLOR = Color.Gainsboro;
        private static readonly Color KEYBOARD_OVERLAY_ACTIVE_TEXT_COLOR = Color.Lime;
        private static readonly Color KEYBOARD_OVERLAY_ACTIVE_BACKGROUND_COLOR = Color.Gray;
        private static readonly Color KEYBOARD_OVERLAY_INACTIVE_TEXT_COLOR = Color.White;
        private static readonly Color KEYBOARD_OVERLAY_INACTIVE_BACKGROUND_COLOR = Color.DarkGray;
        private static readonly Color KEYBOARD_OVERLAY_BORDER_COLOR = Color.DimGray;

        public DebugMenu()
        {
            Initialize();
            LoadContent();
        }

        /// <summary>
        /// Initialize debug elements (labels, fonts, etc.)
        /// </summary>
        private static void Initialize()
        {
            VSP = new VerticalStackPanel();
            KeyboardGrid = new Grid();
            spacer1 = new Label();
            spacer2 = new Label();
            FPSText = new Label();
            PlayerPosText = new Label();
            PlayerVelText = new Label();
            ActiveBackgroundBrush = new SolidBrush(KEYBOARD_OVERLAY_ACTIVE_BACKGROUND_COLOR);
            InactiveBackgroundBrush = new SolidBrush(KEYBOARD_OVERLAY_INACTIVE_BACKGROUND_COLOR);

            UIWidgets = new List<Widget>()
            {
                spacer1, FPSText, PlayerPosText, PlayerVelText, KeyboardGrid
            };

            // FontSystem is kinda like a font-handler. We can use this to retrieve the
            // font data to use in UI components
            byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
            DebugFontSystem = new FontSystem();
            DebugFontSystem.AddFont(ttfData);
        }

        /// <summary>
        /// Load/Stylize all debug UI elements
        /// </summary>
        private static void LoadContent()
        {
            VSP.Margin = new Myra.Graphics2D.Thickness(100, 100, 0, 0);

            // Configure labels 
            foreach (var widget in UIWidgets)
            {
                if (widget is Label)
                {
                    ((Label)widget).Text = string.Empty;
                    ((Label)widget).TextColor = DEBUG_TEXT_COLOR;
                    ((Label)widget).Font = DebugFontSystem.GetFont(DEBUG_FONT_SIZE);
                    ((Label)widget).Visible = GameDebug.DebugMode;
                }

                VSP.Widgets.Add(widget);
            }

            // Configure the KeyboardGrid
            KeyboardGrid.ShowGridLines = false;
            KeyboardGrid.ColumnSpacing = 0;
            KeyboardGrid.RowSpacing = 0;
            KeyboardGrid.AcceptsKeyboardFocus = false;

            // Make a 3x3 grid where each column is 52px wide, 
            // and each row is 56px tall
            for (int i = 0; i < 3; i++)
            {
                KeyboardGrid.ColumnsProportions.Add(
                    new Proportion()
                    {
                        Type = ProportionType.Pixels,
                        Value = 52
                    }
                );

                KeyboardGrid.RowsProportions.Add(
                    new Proportion()
                    {
                        Type = ProportionType.Pixels,
                        Value = 56
                    }
                );
            }

            // Place labels into grid
            keyboardLabelDict[Keys.W].GridRow = 0;
            keyboardLabelDict[Keys.W].GridColumn = 1;

            keyboardLabelDict[Keys.A].GridRow = 1;
            keyboardLabelDict[Keys.A].GridColumn = 0;

            keyboardLabelDict[Keys.S].GridRow = 1;
            keyboardLabelDict[Keys.S].GridColumn = 1;

            keyboardLabelDict[Keys.D].GridRow = 1;
            keyboardLabelDict[Keys.D].GridColumn = 2;

            keyboardLabelDict[Keys.Space].GridRow = 2;
            keyboardLabelDict[Keys.Space].GridColumn = 0;
            keyboardLabelDict[Keys.Space].GridColumnSpan = 3;

            // Configure all labels for the keyboard overlay
            foreach (var keyLabelPair in keyboardLabelDict)
            {
                Label keyLabel = keyLabelPair.Value;
                keyLabel.Font = DebugFontSystem.GetFont(DEBUG_FONT_SIZE);
                keyLabel.TextColor = KEYBOARD_OVERLAY_INACTIVE_TEXT_COLOR;
                keyLabel.Visible = GameDebug.DebugMode;

                // Stretch the Label to fit within the entirety of the grid cell
                keyLabel.HorizontalAlignment = HorizontalAlignment.Stretch;
                keyLabel.VerticalAlignment = VerticalAlignment.Stretch;

                // Set the text to be centered within the label
                keyLabel.TextAlign = FontStashSharp.RichText.TextHorizontalAlignment.Center;

                keyLabel.Background = ActiveBackgroundBrush;
                keyLabel.Border = new SolidBrush(KEYBOARD_OVERLAY_BORDER_COLOR);
                keyLabel.BorderThickness = new Myra.Graphics2D.Thickness(1);

                KeyboardGrid.Widgets.Add(keyLabel);
            }

            VSP.Widgets.Add(KeyboardGrid);
        }

        /// <summary>
        /// Toggle the visibility of the debug UI
        /// </summary>
        public static void ToggleVisibility()
        {
            GameDebug.DebugMode = !GameDebug.DebugMode;
            SetComponentVisibility();
        }

        /// <summary>
        /// Sets the visibility of all elements of the debug UI
        /// </summary>
        private static void SetComponentVisibility()
        {
            // TODO: Have some sort of list of all UI elements so we can 
            // use foreach
            FPSText.Visible = GameDebug.DebugMode;
            PlayerPosText.Visible = GameDebug.DebugMode;
            PlayerVelText.Visible = GameDebug.DebugMode;

            foreach (var entry in keyboardLabelDict)
            {
                entry.Value.Visible = GameDebug.DebugMode;
            }
        }

        /// <summary>
        /// Update debug UI elements
        /// </summary>
        public static void Update()
        {
            // Round the FPS to 2 decimal places
            FPSText.Text = $"FPS: {Math.Round(GameDebug.FPS, 2)}";
            PlayerPosText.Text = $"x-pos: {Math.Round(Player.Instance.Position.X, 2)}"
                + $"\ny-pos: {Math.Round(Player.Instance.Position.Y, 2)}";
            PlayerVelText.Text = $"x-vel: {Math.Round(Player.Instance.Velocity.X, 2)}"
                + $"\ny-vel: {Math.Round(Player.Instance.Velocity.Y, 2)}";

            UpdateKeyboardOverlay();
        }

        /// <summary>
        /// Update the colors of the keyboard overlay if a valid key is pressed
        /// </summary>
        private static void UpdateKeyboardOverlay()
        {
            // For each key that is in the dictionary, check to see if it's
            // pressed. If it is, make it the active color, if it isn't, make it
            // the inactive color.
            KeyboardState keyboardState = Keyboard.GetState();

            foreach (var keyLabelPair in keyboardLabelDict)
            {
                Label keyLabel = keyLabelPair.Value;

                keyLabel.TextColor = keyboardState.IsKeyDown(keyLabelPair.Key)
                    ? KEYBOARD_OVERLAY_ACTIVE_TEXT_COLOR
                    : KEYBOARD_OVERLAY_INACTIVE_TEXT_COLOR;

                keyLabel.Background = keyboardState.IsKeyDown(keyLabelPair.Key)
                    ? ActiveBackgroundBrush
                    : InactiveBackgroundBrush;
            }
        }
    }
}
