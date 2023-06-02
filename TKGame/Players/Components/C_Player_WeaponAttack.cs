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
        private int swordX = 40;
        private int swordY = 10;
        private int spearX = 100;
        private int spearY = 10;
        private int axeX = 40;
        private int axeY = 10;
        #endregion
        public ComponentType Type => throw new NotImplementedException();

        public void Update(Entity player)
        {

            if (Input.MouseState.LeftButton == ButtonState.Pressed)
            {
                if (player.weapon.isActiveSword)
                    SwordAttack(player);
                if(player.weapon.isActiveSpear)
                    SpearAttack(player);
                if(player.weapon.isActiveAxe)
                    AxeAttack(player);
            }
        }

        public void SwordAttack(Entity player)
        {
                if (player.Orientation == SpriteEffects.None)
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

        public void SpearAttack(Entity player)
        {

            if (player.Orientation == SpriteEffects.None)
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

        public void AxeAttack(Entity player)
        {
            if (player.Orientation == SpriteEffects.None)
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