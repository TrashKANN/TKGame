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
            weaponTexture = Art.PlayerAxeTexture;
            position = new Vector2(815, 730);
            weaponRect = new Rectangle(815, 730, 85, 85);
            damageStat = 1;
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
        /// Damage enemy function, will need to be implemented later
        /// </summary>
        /// <param name="weapon"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override int DamageEnemy(Weapon weapon)
        {
            throw new NotImplementedException();
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
