using System;
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
<<<<<<< HEAD
=======
using TKGame.Content.Weapons;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
>>>>>>> 95acb0dfc3575b29374f3fbff7e8f8493a6446c9
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
        public static int ScreenWidth { get { return (int)ScreenSize.X; } }
        public static int ScreenHeight { get { return (int)ScreenSize.Y; } }

        public static GameTime GameTime { get; private set; }

        //Declares public Vertical Stack Panel
        public VerticalStackPanel VSP { get; protected set; }

        // TODO: Move this to another class eventually
        private static readonly Color WALL_COLOR = new Color(0x9a, 0x9b, 0x9c, 0xFF);

        private static GraphicsDeviceManager graphics;
        private static SpriteBatch spriteBatch;

        public static GraphicsDeviceManager Graphics { get { return graphics; } private set { graphics = value; } }
        public static SpriteBatch SpriteBatch { get { return spriteBatch; } private set { spriteBatch = value; } }

        public Desktop desktop;

<<<<<<< HEAD

        
=======
        //Declaring Weapon 
        public Weapon sword;

>>>>>>> 95acb0dfc3575b29374f3fbff7e8f8493a6446c9
        // TODO: Refactor out of the main TKGame class
        public static bool paused;
        #endregion

        #region Components
        private LevelEditorComponent levelEditorComponent;
        public static LevelComponent levelComponent;
        #endregion Components

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
            levelEditorComponent = new World_LevelEditorComponent();
            levelComponent = new World_LevelComponent(new List<Level>());
            paused = true;
        }
        protected override void Initialize()
        {
            // Let Myra know what our Game object is so we can use it
            MyraEnvironment.Game = this;
            desktop = new Desktop();

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

            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            Art.LoadContent(Content);
            Music.LoadContent(Content, 0.069f);

            // Load Weapon System Content
            WeaponSystem.LoadContent(VSP);

            // Load debug content
            GameDebug.LoadContent(VSP);

            // Load main menu
            MainMenu.LoadContent();

            // Add and Load Default Level
            levelComponent.AddLevel(new Level(new Dictionary<string, Stage>
            {
                { "room0", new Stage("room0") }, 
                { "room1", new Stage("room1") }, 
                { "room2", new Stage("room2") }
            }
            ));

            // Spawn a knight enemy
            EnemyFactory knightFactory = new KnightEnemyFactory();
            Enemy knight = knightFactory.CreateEnemy();
            EntityManager.Add(knight);
            // Spawn a goblin enemy
            EnemyFactory goblinFactory = new GoblinEnemyFactory();
            Enemy goblin = goblinFactory.CreateEnemy();
            EntityManager.Add(goblin);

            // Spawn a potion item
            ItemFactory potionFactory = new PotionItemFactory();
            Item potion = potionFactory.CreateItem();
            EntityManager.Add(potion);
        }
        protected override async void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();

            // Add pause stuff here
            //Do if not paused
            if (!paused)
            {
<<<<<<< HEAD
                EntityManager.Update(gameTime, spriteBatch, currentStage);  
=======
                EntityManager.Update(gameTime, spriteBatch, currentStage);
                sword.Update(sword);
                TKGame.levelComponent.Update();
>>>>>>> 95acb0dfc3575b29374f3fbff7e8f8493a6446c9
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
                paused = true;
            }
#endif

            // Update Transition Screen
            if (levelComponent.GetCurrentLevel().isTransitioning)
            {
                paused = true;
                levelComponent.GetCurrentLevel().transition.Update(GameTime);
            }

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
            spriteBatch.Draw(Art.BackgroundTexture, levelComponent.GetCurrentStage().Background.BackgroundRect, Color.White);


            // Draw each wall to the screen
            // Update level editor

            foreach (Wall wall in levelComponent.GetCurrentStage().StageWalls)
            {
                spriteBatch.Draw(wall.Texture, wall.HitBox, WALL_COLOR);
                if (GameDebug.DebugMode) 
                { 
                    GameDebug.DrawBoundingBox(wall.HitBox, Color.Lime, 5); 
                }
            }

            foreach (Trigger trigger in levelComponent.GetCurrentStage().StageTriggers)
            {
                spriteBatch.Draw(trigger.Texture, trigger.HitBox, Color.White);
            }

            EntityManager.Draw(spriteBatch);

            if (GameDebug.DebugMode)
            {
                foreach (Entity entity in EntityManager.GetEntities())
                {
                    GameDebug.DrawBoundingBox(entity, Color.Blue, 5);
                }
            }

            // Draw the New Wall last so that the outline appears above all other images
            if (LevelEditor.EditMode)
            {
                levelEditorComponent.Update();
            }

            // Draw the transition screen if we're transitioning
            if (levelComponent.GetCurrentLevel().isTransitioning)
            {
                levelComponent.GetCurrentLevel().transition.Draw(spriteBatch);
            }

            spriteBatch.End();

            // Render UI elements from Myra
            desktop.Render();

            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }

        public void ExitGame()
        {
            LevelEditor.SaveStageDataToJSON(levelComponent.GetCurrentStage(), "auto_saved_stage_data");
            Exit();
        }

        public static void SwitchToGameplayMenu() 
        {
            Instance.desktop.Root = Instance.VSP;
        }
    }
}