using System;
using System.Collections.Generic;
using FontStashSharp;
using Microsoft.Xna.Framework;
using System.IO;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame
{
    public class GameDebug
    {
        // Boolean to describe whether debug mode is on or off
        public static bool DebugMode { get; set; }
        
        // Vertical stack panel that will hold all the UI elements for debug information
        public static VerticalStackPanel VSP { get; private set; }
        
        private static Dictionary<Keys, Label> keyboardLabelDict = new Dictionary<Keys, Label>()
        {
            { Keys.W,     new Label() { Text = "W"     } },
            { Keys.A,     new Label() { Text = "A"     } },
            { Keys.S,     new Label() { Text = "S"     } },
            { Keys.D,     new Label() { Text = "D"     } },
            { Keys.Space, new Label() { Text = "Space" } }
        };

        private static double FPS { get; set; }

        // FontSystems allow us to use fonts with Myra
        private static FontSystem DebugFontSystem { get; set; }
        private static Label FPSText { get; set; }
        private static Label PlayerPosText { get; set; }
        private static Label PlayerVelText { get; set; }

        // Horizontal stack panel so that the text for the keyboard overlay lines up nicely
        private static HorizontalStackPanel KeyboardOverlay { get; set; }
        private static Texture2D hitboxTexture;

        // Readonly is used since static variables can't be const
        private static readonly int DEBUG_FONT_SIZE = 48;
        private static readonly Color DEBUG_COLOR = Color.Gainsboro;
        private static readonly Color DEBUG_KEYBOARD_OVERLAY_ACTIVE_COLOR = Color.Lime;
        private static readonly Color DEBUG_KEYBOARD_OVERLAY_INACTIVE_COLOR = Color.Red;

        /// <summary>
        /// Initialize debug elements (text, fonts, etc.)
        /// </summary>
        public static void Initialize()
        {
            DebugMode = false;
            FPSText = new Label();
            PlayerPosText = new Label();
            PlayerVelText = new Label();
            KeyboardOverlay = new HorizontalStackPanel();

            // FontSystem is kinda like a font-handler. We can use this to retrieve the
            // font data to use in UI components
            byte[] ttfData = File.ReadAllBytes(@"Content/Fonts/Retro Gaming.ttf");
            DebugFontSystem = new FontSystem();
            DebugFontSystem.AddFont(ttfData);
        }

        public static void LoadContent()
        {
            // Create a VerticalStackPanel to put all the debug elements in so they look nice
            // without needing to manually position every element
            VSP = new VerticalStackPanel();

            // Configure the FPSText panel
            FPSText.Text = string.Empty;
            FPSText.TextColor = DEBUG_COLOR;
            FPSText.Font = DebugFontSystem.GetFont(DEBUG_FONT_SIZE);
            FPSText.Margin = new Myra.Graphics2D.Thickness(100, 100, 0, 0);
            FPSText.Visible = DebugMode;

            // Add FPSText as a child of the VerticalStackPanel
            VSP.Widgets.Add(FPSText);

            // Configure the PlayerPosText panel
            PlayerPosText.Text = string.Empty;
            PlayerPosText.TextColor = DEBUG_COLOR;
            PlayerPosText.Font = DebugFontSystem.GetFont(DEBUG_FONT_SIZE);
            PlayerPosText.Margin = new Myra.Graphics2D.Thickness(100, 0, 0, 0);
            PlayerPosText.Visible = DebugMode;
            VSP.Widgets.Add(PlayerPosText);

            // Configure the PlayerVelText panel
            PlayerVelText.Text = string.Empty;
            PlayerVelText.TextColor = DEBUG_COLOR;
            PlayerVelText.Font = DebugFontSystem.GetFont(DEBUG_FONT_SIZE);
            PlayerVelText.Margin = new Myra.Graphics2D.Thickness(100, 0, 0, 0);
            PlayerVelText.Visible = DebugMode;
            VSP.Widgets.Add(PlayerVelText);

            // Configure all labels for the keyboard overlay
            foreach (var keyLabelPair in keyboardLabelDict)
            {
                Label keyText = keyLabelPair.Value;

                keyText.Font = DebugFontSystem.GetFont(DEBUG_FONT_SIZE);
                keyText.TextColor = DEBUG_KEYBOARD_OVERLAY_INACTIVE_COLOR;
                keyText.Visible = DebugMode;
                keyText.Margin = new Myra.Graphics2D.Thickness(0, 0, 20, 0);

                KeyboardOverlay.Widgets.Add(keyText);

            }
            KeyboardOverlay.Margin = new Myra.Graphics2D.Thickness(100, 0, 100, 0);
            VSP.Widgets.Add(KeyboardOverlay);

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
            foreach (var keyLabelPair in keyboardLabelDict)
            {
                keyLabelPair.Value.TextColor = (Keyboard.GetState().IsKeyDown(keyLabelPair.Key))
                    ? DEBUG_KEYBOARD_OVERLAY_ACTIVE_COLOR
                    : DEBUG_KEYBOARD_OVERLAY_INACTIVE_COLOR;
            }
        }

        /// <summary>
        /// Draw bounding rectangle around a given rectangle.
        /// </summary>
        public static void DrawBoundingRectangle(SpriteBatch spriteBatch, Rectangle rectangle, Color color, int lineWidth)
        {
            if (hitboxTexture is null)
            {
                hitboxTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                hitboxTexture.SetData(new Color[] { Color.White });
            }

            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.X, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.X, rectangle.Y, rectangle.Width + lineWidth, lineWidth), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.X + rectangle.Width, rectangle.Y, lineWidth, rectangle.Height + lineWidth), color);
            spriteBatch.Draw(hitboxTexture, new Rectangle(rectangle.X, rectangle.Y + rectangle.Height, rectangle.Width + lineWidth, lineWidth), color);
        }

        /// <summary>
        /// Draw a bounding rectangle around a given entity.
        /// </summary>
        public static void DrawBoundingRectangle(SpriteBatch spriteBatch, Entity entity, Color color, int lineWidth)
        {
            Rectangle entityRect = new Rectangle
            {
                X = (int)(entity.Position.X - entity.Size.X / 2f),
                Y = (int)(entity.Position.Y - entity.Size.Y / 2f),
                Width = (int)entity.Size.X,
                Height = (int)entity.Size.Y
            };
            DrawBoundingRectangle(spriteBatch, entityRect, color, lineWidth);
        }
    }
}
