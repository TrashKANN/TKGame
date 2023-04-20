using System;
using System.Collections.Generic;
using FontStashSharp;
using Microsoft.Xna.Framework;
using System.IO;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;
using IMenu = TKGame.UI.IMenu;

namespace TKGame.BackEnd
{
    public class GameDebug : IMenu
    {
        #region Member Variables

        private static VerticalStackPanel VSP { get; set; }

        // Boolean to describe whether debug mode is on or off
        public static bool DebugMode { get; set; }

        // Vertical stack panel that will hold all the UI elements for debug information
        //public static VerticalStackPanel VSP { get; private set; }

        private static Dictionary<Keys, Label> keyboardLabelDict = new Dictionary<Keys, Label>()
        {
            { Keys.W,     new Label() { Text = "W"     } },
            { Keys.A,     new Label() { Text = "A"     } },
            { Keys.S,     new Label() { Text = "S"     } },
            { Keys.D,     new Label() { Text = "D"     } },
            { Keys.Space, new Label() { Text = "Space" } }
        };

        private static double FPS { get; set; }

        //Spacers to separate VSP
        private static Label spacer1 { get; set; }
        private static Label spacer2 { get; set; }


        // FontSystems allow us to use fonts with Myra
        private static FontSystem DebugFontSystem { get; set; }
        private static Label FPSText { get; set; }
        private static Label PlayerPosText { get; set; }
        private static Label PlayerVelText { get; set; }

        private static Grid KeyboardGrid { get; set; }
        private static Texture2D HitboxTexture { get; set; }
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

        #endregion

        public GameDebug()
        {
            Initialize();
            LoadContent();
        }

        /// <summary>
        /// Initialize debug elements (labels, fonts, etc.)
        /// </summary>
        public static void Initialize()
        {
            VSP = new VerticalStackPanel();
            DebugMode = false;
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
        public static void LoadContent()
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
                    ((Label)widget).Visible = DebugMode;
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
                keyLabel.Visible = DebugMode;

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
        /// Updates the debug UI's FPS text using a given GameTime. This should be
        /// called at the *very end* of the game's Draw() function
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
            PlayerPosText.Visible = DebugMode;
            PlayerVelText.Visible = DebugMode;

            foreach (var entry in keyboardLabelDict)
            {
                entry.Value.Visible = DebugMode;
            }
        }

        /// <summary>
        /// Update debug UI elements
        /// </summary>
        public static void Update()
        {
            // Round the FPS to 2 decimal places
            FPSText.Text = $"FPS: {Math.Round(FPS, 2)}";
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

        /// <summary>
        /// Draw bounding rectangle around a given rectangle.
        /// </summary>
        public static void DrawBoundingBox(Rectangle rectangle, Color color, int lineWidth)
        {
            SpriteBatch spriteBatch = TKGame.SpriteBatch;

            if (HitboxTexture is null)
            {
                HitboxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                HitboxTexture.SetData(new Color[] { Color.White });
            }

            // These will draw thin rectangles (essentially lines) on each edge of a rectangle
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height), color);
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width, lineWidth), color);
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X + rectangle.Width - lineWidth, rectangle.Y, lineWidth, rectangle.Height), color);
            spriteBatch.Draw(HitboxTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height - lineWidth, rectangle.Width, lineWidth), color);
        }

        /// <summary>
        /// Draw a bounding rectangle around a given entity.
        /// </summary>
        public static void DrawBoundingBox(Entity entity, Color color, int lineWidth)
        {
            // Construct a Rectangle out of the entity's position and size so we can
            // just use the other DrawBoundingRectangle() function to actually draw
            Rectangle entityRect = new Rectangle
            {
                X = (int)(entity.Position.X - entity.Size.X / 2f),
                Y = (int)(entity.Position.Y - entity.Size.Y / 2f),
                Width = (int)entity.Size.X,
                Height = (int)entity.Size.Y
            };
            DrawBoundingBox(entityRect, color, lineWidth);
        }
    }
}