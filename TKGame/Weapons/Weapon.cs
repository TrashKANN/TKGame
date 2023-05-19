using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;

namespace TKGame.Content.Weapons
{
    public abstract class Weapon
    { 

        public Texture2D weaponTexture;
        public Vector2 position, velocity;
        public Rectangle hitbox;
        public SpriteEffects orientation;
        public bool isActiveSword = true;
        public bool isActiveSpear = false;
        public bool isActiveAxe = false;
        public Color color = Color.White;
        public float damageStat;
        public Entity currentEntity;
        public Vector2 size;
        public int xoff = 50;
        public int yoff = 60;
        public bool isReversed = false;

        /// <summary>
        /// Function to draw Weapon Sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Input.KeyboardState.CapsLock) 
                spriteBatch.Draw(weaponTexture, hitbox, color);
        }


        /// <summary>
        /// Activates Weapon
        /// </summary>
        public abstract void Activate();
        public abstract void Deactivate();
        /// <summary>
        /// Function to Update Weapon Position
        /// </summary>
        /// <param name="w"></param>
        public abstract void Update(Entity E);

        //private Texture2D UpdateTexture()
        //{
        //    if (isActiveSpear)
        //        weaponTexture = Art.SpearTexture;
        //    else if (isActiveAxe)
        //        weaponTexture = Art.AxeTexture;
        //    else 
        //        weaponTexture = Art.SwordTexture;
        //    return weaponTexture;
        //}
    }
}
