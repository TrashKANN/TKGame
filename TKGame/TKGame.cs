﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Myra is a library that allows us to add GUI components.
// https://github.com/rds1983/Myra
using Myra;
using Myra.Graphics2D.UI;
using TKGame.BackEnd;
using TKGame.Players;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;
using TKGame.UI;
using TKGame.Weapons;
using TKGame.World.Components;
using TKGame.Enemies;
using TKGame.Items;
using TKGame.Content.Weapons;

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

        // TODO: Move this to another class eventually
        private static readonly Color WALL_COLOR = new Color(0x9a, 0x9b, 0x9c, 0xFF);

        private static GraphicsDeviceManager graphics;
        private static SpriteBatch spriteBatch;

        public static GraphicsDeviceManager Graphics { get { return graphics; } private set { graphics = value; } }
        public static SpriteBatch SpriteBatch { get { return spriteBatch; } private set { spriteBatch = value; } }

        public Desktop desktop;

        // TODO: Refactor out of the main TKGame class
        public static bool paused;
        private static bool hasLoaded = false;
        private static bool hasInitialized = false;
        public static readonly float GRAVITY = 1100.0f;


        #endregion

        #region Components
        private ILevelEditorComponent levelEditorComponent;
        public static ILevelComponent levelComponent;
        #endregion Components

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
            levelEditorComponent = new C_World_LevelEditor();
            levelComponent = new C_World_Level(new List<Level>());
            paused = true;

        }
        protected override void Initialize()
        {
            // Let Myra know what our Game object is so we can use it
            MyraEnvironment.Game = this;

            base.Initialize();
            hasInitialized = true;
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Art.LoadContent(Content);
            Music.LoadContent(Content, 0.069f);

            // Add and Load Default Level
            levelComponent.AddLevel(new Level(new Dictionary<string, Stage>
            {
                { "stage0", new Stage("stage0.json") },
                { "stage1", new Stage(StageGenerator.GenerateStage("stage1.json")) },
                { "stage2", new Stage(StageGenerator.GenerateStage("stage2.json")) },
                { "stage3", new Stage(StageGenerator.GenerateStage("stage3.json")) },
                { "stage4", new Stage(StageGenerator.GenerateStage("stage4.json")) },
                { "stage5", new Stage(StageGenerator.GenerateStage("stage5.json")) },
            }
            ));

            // load the first level
            levelComponent.GetCurrentStage().Initialize();
            
            hasLoaded = true;
        }
        protected override void Update(GameTime gameTime)
        {
            GameTime = gameTime;
            Input.Update();

            // Add pause stuff here
            //Do if not paused
            if (!paused)
            {
                levelComponent.Update();
            }

            // Switch menus or exit game depending on current menu when Escape is pressed
            if (Input.WasKeyPressed(Keys.Escape))
            {
                switch (MenuHandler.CurrentMenuState)
                {
                    case MenuHandler.MenuState.GAME_MENU:
                        MenuHandler.SwitchToMenu(MenuHandler.MenuState.PAUSE_MENU);
                        paused = true;
                        break;
                    case MenuHandler.MenuState.PAUSE_MENU:
                        MenuHandler.SwitchToMenu(MenuHandler.MenuState.GAME_MENU);
                        paused = false;
                        break;
                    case MenuHandler.MenuState.MAIN_MENU:
                        ExitGame();
                        break;
                    default:
                        break;
                }
            }
#if DEBUG
            // Toggle the debug UI's visibility once per key press
            // TODO: Probably move this somewhere else
            if (Input.WasKeyPressed(Keys.G))
            {
                DebugMenu.ToggleVisibility();
            }
            
            // Toggle Editing mode for levels, pauses the game to hault entity movement.
            if (Input.WasKeyPressed(Keys.L))
            {
                LevelEditor.ToggleEditor();
                paused = !paused;
            }
#endif

            // Update Transition Screen
            if (levelComponent.GetCurrentLevel().isTransitioning)
            {
                paused = true;
                levelComponent.GetCurrentLevel().transition.Update(GameTime);
            }


            // Update all menus
            MenuHandler.UpdateMenus();

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


            //Draw Stage Backgrounds
            levelComponent.GetCurrentStage().Draw(spriteBatch);


            // Draw each wall to the screen
            // Update level editor

            if (hasLoaded && hasInitialized)
            {

                foreach (IBlock block in levelComponent.GetCurrentStage().StageBlocks)
                {
                    spriteBatch.Draw(block.Texture, block.HitBox, WALL_COLOR);
                    if (GameDebug.DebugMode)
                    {
                        GameDebug.DrawBoundingBox(block.HitBox, Color.Lime, 5);
                    }
                }

                EntityManager.Draw(spriteBatch);

                if (GameDebug.DebugMode)
                {
                    foreach (Entity entity in EntityManager.GetEntities())
                    {
                        GameDebug.DrawBoundingBox(entity, Color.Blue, 5);
                    }

                    foreach (IAttackComponent atk in Player.Instance.GetAttackComponents())
                    {
                        if (atk != null)
                        {
                            if (atk.isAttacking)
                            {
                                GameDebug.DrawBoundingBox(atk.HitBox, Color.Red, 3);
                            }
                        }
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
            }

            spriteBatch.End();

            // Render UI elements from Myra
            MenuHandler.Desktop.Render();

            base.Draw(gameTime);

            // Once everything has finished drawing, figure out the framerate
            GameDebug.UpdateFPS(gameTime);
        }

        public void ExitGame()
        {
            Exit();
        }
    }
}