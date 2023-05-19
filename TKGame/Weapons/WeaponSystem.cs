using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FontStashSharp;
using Microsoft.Xna.Framework;
using System.IO;
using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D.Brushes;
using System.ComponentModel;
using TKGame.BackEnd;

namespace TKGame.Weapons
{
    public class WeaponSystem
    {
        #region member variables
        public Texture2D weaponTexture { get; set; }
        public Vector2 weaponPosition;
        public Rectangle weaponRectangle;
        #endregion

        public WeaponSystem()
        {
            weaponRectangle = new Rectangle(1450, 50, 100, 100);
            weaponTexture = Art.SwordTexture;
        }

        public virtual void Draw(SpriteBatch spriteBatch, Texture2D weaponTexture)
        {
            if(Input.KeyboardState.CapsLock)
                spriteBatch.Draw(weaponTexture, weaponRectangle, Color.White);        
        }
    }
}
