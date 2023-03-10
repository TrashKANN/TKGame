using System;
using System.Collections.Generic;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        #region Member Variables
        public static TKGame Instance { get; private set; }
        public static Viewport Viewport { get { return Instance.GraphicsDevice.Viewport; } }
        public static Vector2 ScreenSize { get { return new Vector2(Viewport.Width, Viewport.Height); } }
        public static GameTime GameTime { get; private set; }

        //Declares public Vertical Stack Panel
        public VerticalStackPanel VSP { get; protected set; }

        // TODO: Move this to another class eventually
        private static readonly Color WALL_COLOR = new Color(0x9a, 0x9b, 0x9c, 0xFF);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Desktop desktop;
        private KeyboardState previousState, currentState;

        //Declare Background Object
        private Background BackgroundImage;


        // Declare Enemy Object
        Enemy.DoomguyEnemy enemy;

        //Declare Triggers
        List<Trigger> triggers;

        
        // TODO: Refactor out of the main TKGame class
        private static string currentStageName = "defaultStage" + ".json";
        Stage currentStage;
        //Stage leftStage;
        //Stage rightStage;
        int screenWidth, screenHeight;
        bool paused = false;
        #endregion
        public TKGame()
        {
            Instance = this;
            Content.RootDirectory = "Content";
            IsFixedTimeStep = true; // Time between frames is constant
            TargetElapsedTime = TimeSpan.FromSeconds(1d / 240d); // Set target fps (240 for now)
            graphics = new GraphicsDeviceManager(this);
            VSP = new VerticalStackPanel();
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

            // Create Triggers
            // TODO: Create Functionality for Procedural Generation with Level Designer
            triggers = new List<Trigger>()
            {
                new Trigger(0,screenHeight - 240, 55, 195, GraphicsDevice),
                new Trigger(screenWidth - 50, screenHeight - 240, 50, 195, GraphicsDevice),
            };

            // TODO: Remove magic numbers
            // Initialize a default stage.
            currentStageName = File.Exists(currentStageName) 
                ? currentStageName 
                : "defaultStage.json";
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
            //Initializing WeaponSystem
            WeaponSystem.Initialize();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LoadContent(Content);
            Music.LoadContent(Content, 0.15f);

            // Manually adding entities at the moment...
            EntityManager.Add(Player.Instance);
            EntityManager.Add(Enemy.DoomguyEnemy.Instance);
            EntityManager.Add(Item.DiamondSwordItem.Instance);

            //Loads Image into the Texture

            //BackgroundImage.BackgroundTexture = Content.Load<Texture2D>(@"Art/Cobble");

            // Load Weapon System Content
            WeaponSystem.LoadContent(VSP);
           // BackgroundImage.BackgroundTexture = Content.Load<Texture2D>(@"C:/Users/");


            
            // Load debug content
            GameDebug.LoadContent(VSP);



            // Continue setting up Myra
            desktop = new Desktop();
            desktop.Root = VSP;
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
                EntityManager.Update(gameTime, spriteBatch, currentStage);
            }

            if (triggers[0].checkLeftTrigger(Player.Instance))
            {
                List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(triggers[0].leftStage, GraphicsDevice)).walls;
                currentStage = new Stage(stageWalls, graphics.GraphicsDevice);
            }

            if (triggers[1].checkRightTrigger(Player.Instance))
            {
                List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(triggers[1].rightStage, GraphicsDevice)).walls;
                currentStage = new Stage(stageWalls, graphics.GraphicsDevice);
            }

            // Exit the game if Escape is pressed
            if (Input.WasKeyPressed(Keys.Escape))
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

            // Toggle Editing mode for levels, pauses the game to hault entity movement.
            if (Input.WasKeyPressed(Keys.L))
            {
                LevelEditor.ToggleEditor();
                paused = !paused;
            }
#endif
            // Will Prompt the User for a string that it will use to save the stage
            //if (Input.WasKeyPressed(Keys.U))
            //{
            //    Console.WriteLine("Enter in the name you'd like to save the stage under:");
            //    string newStageName = Console.ReadLine();
            //    Console.WriteLine("You Entere: " + newStageName);
            //    LevelEditor.SaveStageDataToJSON(currentStage, newStageName);
            //}

            // Set the previous state now that we've checked for our desired inputs
            previousState = currentState;

            // Updates Weapon System
            WeaponSystem.Update();

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
            spriteBatch.Draw(Art.BackgroundTexture, BackgroundImage.BackgroundRect, Color.White);

            // Draw each wall to the screen
            // Update level editor

            foreach (Wall wall in currentStage.walls)
            {
                spriteBatch.Draw(wall.Texture, wall.HitBox, WALL_COLOR);
                if (GameDebug.DebugMode) 
                { 
                    GameDebug.DrawBoundingBox(spriteBatch, wall.HitBox, Color.Lime, 5); 
                }
            }


            //Draw Triggers in gaps in the walls
            //TODO: Add Functionality for Level Designer
            foreach (Trigger trigger in triggers)
            {
                spriteBatch.Draw(trigger.texture, trigger.rectangle, Color.White);
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
                LevelEditor.DrawGridLines(spriteBatch, screenWidth, screenHeight, Color.Black);
            }

            Entity.DrawCollisionIntersections(spriteBatch, EntityManager.GetEntities()[0].collisions);

            spriteBatch.End();

            // Render UI elements from Myra
            desktop.Render();

            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }
    }
}