using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;

// Myra is a library that allows us to add GUI components.
// https://github.com/rds1983/Myra
using Myra;
using Myra.Graphics2D.UI;

namespace TKGame
{
    public class TKGame : Game
    {
        public static TKGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Desktop desktop;
        private KeyboardState previousState, currentState;


        //Declare Background Object
        private Background BackgroundImage;
        
        // TODO: Refactor out of the main TKGame class
        List<Wall> walls;
        int screenWidth, screenHeight;
        bool paused = false;

        public TKGame()
        {
            Instance = this;
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            this.Content.RootDirectory = "Content";

            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            // Let Myra know what our Game object is so we can use it
            MyraEnvironment.Game = this;

            //Create New Background Object w/variables for setting Rectangle and Texture
            BackgroundImage = new Background(screenWidth, screenHeight, graphics.GraphicsDevice);
 
            // TODO: Remove magic numbers
            // We'll eventually probably want to generate walls based off level data (from a file).
            walls = new List<Wall>()
            {
                new Wall(0, screenHeight - 40, screenWidth, 50, graphics.GraphicsDevice),       // Floor 
                new Wall(0, 0, screenWidth, 50, graphics.GraphicsDevice),                       // Ceiling
                new Wall(0, 0, 50, screenHeight - 250, graphics.GraphicsDevice),                // Left wall
                new Wall(screenWidth - 50, 0, 50, screenHeight - 250, graphics.GraphicsDevice), // Right wall
                new Wall(screenWidth / 2, screenHeight / 2, 250, 250, graphics.GraphicsDevice)  // Extra wall to test collision on
            };

            // Initialize keyboard states (used for one-shot keyboard inputs)
            previousState = currentState = new KeyboardState();

            // Initialize debug information
            GameDebug.Initialize();
#if DEBUG
            // For now, just enable DebugMode when building a Debug version
            GameDebug.DebugMode = true;
#endif
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LoadContent(Content);

            EntityManager.Add(Player.Instance);

            //Loads Image into the Texture
            BackgroundImage.BackgroundTexture = this.Content.Load<Texture2D>(@"Cobble");


            // TODO: use this.Content to load your game content here

            // Load debug content
            GameDebug.LoadContent();

            // Continue setting up Myra
            desktop = new Desktop();
            desktop.Root = GameDebug.VSP;
        }

        protected override void Update(GameTime gameTime)
        {
            // Get the current keyboard state
            currentState = Keyboard.GetState();
            GameTime = gameTime;
            Input.Update();

            // Add pause stuff here

            // Exit the game if Escape is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

#if DEBUG
            // Toggle the debug UI's visibility once per key press
            // TODO: Probably move this somewhere else
            if (currentState.IsKeyDown(Keys.G) && !previousState.IsKeyDown(Keys.G))
            {
                GameDebug.ToggleVisibility();
            }
#endif

            // Set the previous state now that we've checked for our desired inputs
            previousState = currentState;

            // Update debug information
            GameDebug.Update();

            //Do if not paused
            if (!paused)
            {
                EntityManager.Update(gameTime);
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here

            // SpriteBatch sends your sprites in batches to the GPU. We can
            // Begin and End a couple hundred batches per frame. Sprites that
            // share the same shader are placed in the same batch.
            spriteBatch.Begin();

            //Draws the image into the Background
            spriteBatch.Draw(BackgroundImage.BackgroundTexture, BackgroundImage.BackgroundRect, Color.White);

            // Draw each wall to the screen
            foreach (Wall wall in walls)
            {
                spriteBatch.Draw(wall.Texture, wall.Rect, Color.Beige);
            }

            // SpriteSortMode is set for sprite Textures, BlendState is apparently better for PNGs
            spriteBatch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied);
            EntityManager.Draw(spriteBatch);
            spriteBatch.End();

            // Render UI elements from Myra
            desktop.Render();

            // Add spriteBatch for everything else i.e. Text etc.

            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }
    }
}