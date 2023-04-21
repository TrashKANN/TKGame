using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;

namespace TKGame.Weapons
{
    class Spear : Weapon
    {
        private static readonly int WALK_ACCELERATION = 1;
        private static readonly int JUMP_HEIGHT = -1;
        private float MOVEMENT_SPEED = 500f;

        //Components
        GraphicsComponent weaponGraphics;


        /// <summary>
        /// Private Constructor for Sword
        /// </summary>
        public Spear()
        {
            //input = input_;
            //physics = physics_;
            //graphics = graphics_;
            weaponTexture = Art.SpearTexture;
            position = new Vector2(815, 730);
            weaponRect = new Rectangle(815, 730, 85, 85);
            damageStat = 1;
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
        public override void Update(Entity E, SpriteBatch spriteBatch)
        {
            weaponGraphics.Update(E, spriteBatch);
        }
    }
}
