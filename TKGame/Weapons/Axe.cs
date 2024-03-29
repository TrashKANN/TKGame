﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;

namespace TKGame.Weapons
{
    class Axe : Weapon
    {
        //Components
        IGraphicsComponent weaponsComponent;


        /// <summary>
        /// Private Constructor for Sword
        /// </summary>
        public Axe()
        {
            weaponsComponent= new C_Weapon_Graphics();
            damageStat = 10; //Same Speed and Range Better Damage
            weaponTexture = Art.PlayerAxeTexture;
            position = new Vector2(815, 730);
            
            hitbox = new Rectangle(815, 730, 85, 100);
            damageStat = 10;
        }

        /// <summary>
        /// Activates weapon
        /// </summary>
        public override void Activate() { isActiveAxe = true; }
        /// <summary>
        /// Deactivates Weapon
        /// </summary>
        public override void Deactivate() { isActiveAxe = false; }
        /// <summary>
        /// Draws item on screen for pickup
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {

            //spriteBatch.Draw(weaponTexture, position, weaponRect, color, 0, position, 0.2f, orientation, 0);
            //spriteBatch.Draw(weaponTexture, weaponRect, color);
        }

        /// <summary>
        /// Updates Sword, will need to be changed
        /// </summary>
        /// <param name="w"></param>
        public override void Update(Entity E)
        {
        }
    }
}
