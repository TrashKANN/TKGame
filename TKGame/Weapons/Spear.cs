using Microsoft.Xna.Framework;
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
    class Spear : Weapon
    {


        //Components
        IGraphicsComponent weaponGraphics;


        /// <summary>
        /// Private Constructor for Sword
        /// </summary>
        public Spear()
        {
            weaponGraphics = new C_Weapon_Graphics();
            damageStat = 5; //Same Damage Better Range
            weaponTexture = Art.PlayerSpearTexture;
            position = new Vector2(815, 730);
            hitbox = new Rectangle(815, 730, 150, 85);
            damageStat = 5;

        }

        /// <summary>
        /// Activates weapon
        /// </summary>
        public override void Activate() { isActiveSpear = true; }
        /// <summary>
        /// Deactivates Weapon
        /// </summary>
        public override void Deactivate() { isActiveSpear = false; }
        /// <summary>
        /// Draws item on screen for pickup
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {

        }

        /// <summary>
        /// Updates Sword, will need to be changed
        /// </summary>
        /// <param name="w"></param>
        public override void Update(Entity E)
        {
            weaponGraphics.Update(E);
        }
    }
}
