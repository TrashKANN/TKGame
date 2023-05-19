using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;//Allows the use of MediaPlayer


namespace TKGame.BackEnd
{
    public class Music
    {
        #region Member Variables
        private static Song backgroundSong;
        #endregion

        /// <summary>
        /// Loads up audio and plays it. Takes a Content Manager and a float volume value.
        /// </summary>
        /// <param name="Content"></param>
        /// <param name="vol"></param>
        public static void LoadContent(ContentManager Content, float vol)
        {
            //Song is just temporary until Keegan makes us a basedAF soundtrack
            backgroundSong = Content.Load<Song>(@"C:/Users/Greywater/GIT/Backgrounds/TKGame/TKGame/Content/bin/DesktopGL/Audio/Break-Out-Loop");
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = vol; //1.0f is full volume 0.0f is silent
            MediaPlayer.Play(backgroundSong);

            //TODO: Add Functionality for changing stages or screens
        }



    }
}
