using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Myra is a library that allows us to add GUI components.
// https://github.com/rds1983/Myra
using Myra;
using Myra.Graphics2D.UI;
using TKGame.Level_Editor_Content;

namespace TKGame
{
    public class TKGame : Game
    {
        public static TKGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        // TODO: Move this to another class eventually
        private static readonly Color WALL_COLOR = new Color(0x9a, 0x9b, 0x9c, 0xFF);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Desktop desktop;
        private KeyboardState previousState, currentState;


        //Declare Background Object
        private Background BackgroundImage;

        // TODO: Refactor out of the main TKGame class
        private static readonly string currentStageName = "defaultStage" + ".json";
        Stage currentStage;
        int screenWidth, screenHeight;
        bool paused = false;

        public TKGame()
        {
            Instance = this;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true; // Time between frames is constant
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 240d); // Set target fps (240 for now)
            graphics = new GraphicsDeviceManager(this);
            graphics.SynchronizeWithVerticalRetrace = false; // Disable v-sync
            graphics.PreferredBackBufferWidth = 1600;
            graphics.PreferredBackBufferHeight = 900;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            screenWidth = graphics.PreferredBackBufferWidth;
            screenHeight = graphics.PreferredBackBufferHeight;

            // Let Myra know what our Game object is so we can use it
            MyraEnvironment.Game = this;

            //Create New Background Object w/variables for setting Rectangle and Texture
            BackgroundImage = new Background(screenWidth, screenHeight, graphics.GraphicsDevice);

            // TODO: Remove magic numbers
            // Initialize a default stage.
            List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(currentStageName, graphics.GraphicsDevice)).walls;
            currentStage = new Stage(stageWalls ,graphics.GraphicsDevice);


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
            BackgroundImage.BackgroundTexture = Content.Load<Texture2D>(@"Art/Cobble");

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
            //Do if not paused
            if (!paused)
            {
                EntityManager.Update(gameTime);
            }

            // Exit the game if Escape is pressed
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                LevelEditor.SaveStageDataToJSON(currentStage, "auto_saved_stage_data");
                Exit();
            }

#if DEBUG
            // Toggle the debug UI's visibility once per key press
            // TODO: Probably move this somewhere else
            if (Input.WasKeyPressed(Keys.G))
            {
                GameDebug.ToggleVisibility();
            }

            // Toggle Editing mode for levels
            if (currentState.IsKeyDown(Keys.L) && !previousState.IsKeyDown(Keys.L))
            {
                LevelEditor.ToggleEditor();
            }
#endif

            //if (currentState.IsKeyDown(Keys.U) && !previousState.IsKeyDown(Keys.U))
            //{
            //    Console.WriteLine("Enter in the name you'd like to save the stage under:");
            //    string newStageName = Console.ReadLine();
            //    Console.WriteLine("You Entere: " + newStageName);
            //    LevelEditor.SaveStageDataToJSON(currentStage, newStageName);
            //}

            // Set the previous state now that we've checked for our desired inputs
            previousState = currentState;

            // Update debug information
            GameDebug.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SlateGray);

            // TODO: Add your drawing code here

            // SpriteBatch sends your sprites in batches to the GPU. We can
            // Begin and End a couple hundred batches per frame. Sprites that
            // share the same shader are placed in the same batch.

            // Deferred rendering means things are drawn in the ordered they're called
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            //Draws the image into the Background
            spriteBatch.Draw(BackgroundImage.BackgroundTexture, BackgroundImage.BackgroundRect, Color.White);

            // Draw each wall to the screen
            // Update level editor

            foreach (Wall wall in currentStage.walls)
            {
                spriteBatch.Draw(wall.Texture, wall.Rect, WALL_COLOR);
                if (GameDebug.DebugMode) 
                { 
                    GameDebug.DrawBoundingBox(spriteBatch, wall.Rect, Color.Lime, 5); 
                }
            }

            EntityManager.Draw(spriteBatch);

            foreach (Entity entity in EntityManager.GetEntities())
            {
                if (GameDebug.DebugMode)
                {
                    GameDebug.DrawBoundingBox(spriteBatch, entity, Color.Blue, 5);
                }
            }

            // Draw the New Wall last so that the outline appears above all other images
            if (LevelEditor.EditMode == true)
            {
                LevelEditor.BuildWall(currentStage, graphics.GraphicsDevice, spriteBatch);
            }

            spriteBatch.End();

            // Render UI elements from Myra
            desktop.Render();

            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }
    }
}