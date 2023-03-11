//=====================File Description=====================
// File Name: GameScreen.cs
// Author: Nathan Green
// Contributors: 
// Creation Date: 3/10/23
//==========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.ScreenManager
{
    public class ScreenManager : DrawableGameComponent
    {
        #region Properties
        /// <summary>
        /// Spritebatch for all screens
        /// </summary>
        public SpriteBatch SpriteBatch { get { return spriteBatch; } }
        
        public bool TraceEnabled { get { return traceEnabled; } set { traceEnabled = value; } }

        #endregion

        #region Variables
        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> updateScreens = new List<GameScreen>();

        SpriteBatch spriteBatch;
        bool initialized;
        bool traceEnabled;

        #endregion

        #region Init

        /// <summary>
        /// Constructs a new screen manageer component;
        /// </summary>
        /// <param name="game"></param>
        public ScreenManager(Game game) : base(game) 
        {

        }

        public override void Initialize()
        {
            base.Initialize();

            initialized = true;
        }

        protected override void UnloadContent()
        {
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }

        #endregion

        #region Update/Draw

        public override void Update(GameTime gameTime)
        {
            updateScreens.Clear();

            foreach(GameScreen screen in screens)
                updateScreens.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool covered = false;

            while (updateScreens.Count > 0)
            {
                // removes topmost screen from list
                GameScreen screen = updateScreens[updateScreens.Count - 1];

                updateScreens.RemoveAt(updateScreens.Count - 1);

                //Updates the Screen
                screen.Update(gameTime, otherScreenHasFocus, covered);

                if(screen.ScreenStatus == ScreenStatus.TransitionOn || screen.ScreenStatus == ScreenStatus.Active)
                {
                    if(!otherScreenHasFocus)
                    {
                        screen.HandleInput();

                        otherScreenHasFocus= true;
                    }

                    if(!screen.Popup)
                        covered= true;
                }
            }

            if (traceEnabled)
                TraceScreen();
        }

        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

#if WINDOWS
            Trace.WriteLine(string.Join(", ", screenNames.ToArray()));
#endif
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameScreen screen in screens)
            {
                if(screen.ScreenStatus == ScreenStatus.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }
        #endregion

        #region Public Methods

        public void AddScreen(GameScreen screen)
        {
            screen.ScreenManager = this;
            screen.IsExiting = false;

            if (initialized)
            {
                screen.LoadContent();
            }

            screens.Add(screen);

        }

        public void RemoveScreen(GameScreen screen)
        {
            if (initialized)
                screen.UnloadContent();

            screens.Remove(screen);
            updateScreens.Remove(screen);
        }

        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }

        #endregion
    }
}
