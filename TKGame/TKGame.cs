﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Myra is a library that allows us to add GUI components.
// https://github.com/rds1983/Myra
using Myra;
using Myra.Graphics2D.UI;
using TKGame.Animations;
using TKGame.BackEnd;
using TKGame.Content.Weapons;
using TKGame.Level_Editor_Content;
using TKGame.Weapons;

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
        public Desktop desktop;
        private KeyboardState previousState, currentState;

        //Declare Background Object
        private Background BackgroundImage;

        //Declare ScreenTransition Object
        private ScreenTransition transition;

        //Declare Triggers
        List<Trigger> triggers;

        //Declaring Weapon 
        public static Weapon currentWeapon { get; private set; }


        
        // TODO: Refactor out of the main TKGame class
        private static string currentStageName = "defaultStage" + ".json";
        internal Stage currentStage;
        //Stage leftStage;
        //Stage rightStage;
        int screenWidth, screenHeight;
        public static bool paused = true;
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
            desktop = new Desktop();

            //Create New Background Object w/variables for setting Rectangle and Texture
            BackgroundImage = new Background(screenWidth, screenHeight, graphics.GraphicsDevice);

            //Create New ScreenTransition Object
            transition = new ScreenTransition(graphics.GraphicsDevice);

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
            // Initialize main menu
            MainMenu.Initialize(desktop, this);

            //Initializing WeaponSystem
            WeaponSystem.Initialize();
            //Initializing Weapons
            currentWeapon = new Sword();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LoadContent(Content);
            Music.LoadContent(Content, 0.15f);

            // Manually add player instance
            EntityManager.Add(Player.Instance);

            // Spawn a knight enemy
            EnemyFactory knightFactory = new KnightEnemyFactory();
            Enemy knight = knightFactory.CreateEnemy();
            EntityManager.Add(knight);

            // Spawn a potion item
            ItemFactory potionFactory = new PotionItemFactory();
            Item potion = potionFactory.CreateItem();
            EntityManager.Add(potion);

            //Loads Image into the Texture


            // Load Weapon System Content
            WeaponSystem.LoadContent(VSP);


            // Load debug content
            GameDebug.LoadContent(VSP);

            // Load main menu
            MainMenu.LoadContent();

            // Continue setting up Myra
            //desktop.Root = VSP;
        }
        protected override async void Update(GameTime gameTime)
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
                currentWeapon.Update(gameTime);
            }
            

            if (triggers[0].checkLeftTrigger(Player.Instance))
            {
                transition.Update(gameTime);
                paused = true;

                List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(triggers[0].leftStage, GraphicsDevice)).walls;
                currentStage = new Stage(stageWalls, graphics.GraphicsDevice);
                paused = false;
            }

            if (triggers[1].checkRightTrigger(Player.Instance))
            {
                paused = true;
                transition.Update(gameTime);
                List<Wall> stageWalls = (LevelEditor.LoadStageDataFromJSON(triggers[1].rightStage, GraphicsDevice)).walls;
                currentStage = new Stage(stageWalls, graphics.GraphicsDevice);
                paused = false;
            }
            // Exit the game if Escape is pressed
            if (Input.WasKeyPressed(Keys.Escape))
            {
                ExitGame();
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
            //Draws currentWeapon
            currentWeapon.Draw(spriteBatch);

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
                // I really hate this, needs to be refactored. Asking Thomas might be easiest
                if(Input.KeyboardState.IsKeyDown(Keys.W))
                {
                    LevelEditor.BuildWall(currentStage, graphics.GraphicsDevice, spriteBatch);
                }
                // D (Hold) + LClick = Mark; + RClick = UnMark; + Enter = Delete Mar
                else if(Input.KeyboardState.IsKeyDown(Keys.D))
                {
                    LevelEditor.DeleteWall(currentStage.walls);
                }
                // Ctrl + Z = Undo last wall deleted
                else if(Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.Z))
                {
                    LevelEditor.UndoDeletedWall(currentStage.walls);
                }
                // Ctrl + Y = Redo last wall deleted
                else if (Input.KeyboardState.IsKeyDown(Keys.LeftControl) && Input.WasKeyPressed(Keys.Y))
                {
                    LevelEditor.RedoDeletedWall(currentStage.walls);
                }
                LevelEditor.DrawGridLines(spriteBatch, screenWidth, screenHeight, Color.Black);
            }

            //Draws Loading Screen Offscreen until needed
            //spriteBatch.Draw(Art.LoadTexture, transition.rect, Color.White);
            transition.Draw(spriteBatch);

            spriteBatch.End();

            // Render UI elements from Myra
            desktop.Render();


            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }

        public void ExitGame()
        {
            LevelEditor.SaveStageDataToJSON(currentStage, "auto_saved_stage_data");
            Exit();
        }

        public static void SwitchToGameplayMenu() 
        {
            Instance.desktop.Root = Instance.VSP;
        }
    }
}