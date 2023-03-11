//=====================File Description=====================
// File Name: GameScreen.cs
// Author: Nathan Green
// Contributors: 
// Creation Date: 3/10/23
//==========================================================
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.ScreenManager
{
    /// <summary>
    /// Enum listing screen transition states
    /// </summary>
    public enum ScreenStatus
    {
        TransitionOn,
        Active,
        TransitionOff,
        Hidden,
    }

    public abstract class GameScreen
    {
        #region Properties
        /// <summary>
        /// Indicates if the screen is a smaller popup screen
        /// </summary>
        public bool Popup { get { return popup; } protected set { popup = value; } }

        /// <summary>
        /// Indicates how long it takes for transition on 
        /// </summary>
        public TimeSpan OnTime { get { return onTime; } protected set { onTime = value; } }

        public TimeSpan OffTime { get { return OffTime; } protected set { OffTime= value; } }

        public float TransitionPostion { get { return TransitionPostion; } protected set { TransitionPostion = value; } }

        public byte TransitionAlpha { get { return (byte)(255 - TransitionPostion * 255); } }

        public ScreenStatus Status { get { return screenStatus; } protected set { screenStatus = value; } }

        public bool IsExiting { get { return exiting; } protected internal set { bool fireEvent = !exiting && value; exiting = value; if (fireEvent && (Exiting != null)) { Exiting(this, EventArgs.Empty); } } }

        public bool IsActive { get { return !otherScreenFocus && (screenStatus == ScreenStatus.TransitionOn || screenStatus == ScreenStatus.Active); } }

        #endregion

        #region variables
        bool popup = false;
        TimeSpan onTime = TimeSpan.Zero;
        TimeSpan offTime = TimeSpan.Zero;
        float transitionPosition = 1;
        ScreenStatus screenStatus = ScreenStatus.TransitionOn;
        bool exiting = false;
        bool otherScreenFocus;
        public event EventHandler Exiting;

        #endregion

        #region Init

        public virtual void LoadContent() { }

        public virtual void UnloadContent() { }

        #endregion

        #region Update and Draw

         public virtual void Update(GameTime gameTime, bool otherScreenHasFocus, bool covered)
        {
            this.otherScreenFocus = otherScreenHasFocus;

            if(IsExiting)
            {
                screenStatus = ScreenStatus.TransitionOff;

                if(!UpdateTransition(gameTime, transistionOffTime, 1))
                {
                    screenManager.RemoveScreen(this);
                }
            }
    }


}
