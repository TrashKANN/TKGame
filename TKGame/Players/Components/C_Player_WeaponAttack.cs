using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Players.Components
{
    internal class C_Player_WeaponAttack : IGraphicsComponent
    {
        #region Variables
        private const int swordX = 40;
        private const int swordY = 10;
        private const int spearX = 100;
        private const int spearY = 10;
        private const int axeX = 40;
        private const int axeY = 10;
        #endregion
        public ComponentType Type => throw new NotImplementedException();

        public void Update(Entity player)
        {

            if (Input.MouseState.LeftButton == ButtonState.Pressed) //Checks if left mouse is pressed
            {
                if (player.weapon.isActiveSword)//Checks if weapon is a sword
                    SwordAttack(player);
                if(player.weapon.isActiveSpear)//Checks if weapon is a spear
                    SpearAttack(player);
                if(player.weapon.isActiveAxe)// cheacks if weapon is an axe
                    AxeAttack(player);
            }
        }

        /// <summary>
        /// Move's players sword into attack position
        /// </summary>
        /// <param name="player"></param>
        public void SwordAttack(Entity player)
        {
                if (player.Orientation == SpriteEffects.None)//Checks the sprites orientation
            {
                    player.weapon.hitbox.X += swordX;
                    player.weapon.hitbox.Y += swordY;

                }
                else if (player.Orientation == SpriteEffects.FlipHorizontally)
                {
                    player.weapon.hitbox.X -= swordX;
                    player.weapon.hitbox.Y -= swordY;
                }
        }

        /// <summary>
        /// Moves player's spear into attack position
        /// </summary>
        /// <param name="player"></param>
        public void SpearAttack(Entity player)
        {

            if (player.Orientation == SpriteEffects.None) //Checks the sprites orientation
            {
                player.weapon.hitbox.X += spearX;
                player.weapon.hitbox.Y += spearY;

            }
            else if (player.Orientation == SpriteEffects.FlipHorizontally)
            {
                player.weapon.hitbox.X -= spearX;
                player.weapon.hitbox.Y -= spearY;
            }
        }

        /// <summary>
        /// Moves player's axe into attack position
        /// </summary>
        /// <param name="player"></param>
        public void AxeAttack(Entity player)
        {
            if (player.Orientation == SpriteEffects.None)//Checks the sprites orientation
            {
                player.weapon.hitbox.X += axeX;
                player.weapon.hitbox.Y += axeY;

            }
            else if (player.Orientation == SpriteEffects.FlipHorizontally)
            {
                player.weapon.hitbox.X -= axeX;
                player.weapon.hitbox.Y -= axeY;
            }
        }
    }
}