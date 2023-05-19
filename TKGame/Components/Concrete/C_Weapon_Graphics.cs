using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.Weapons;

namespace TKGame.Components.Concrete
{
    internal class C_Weapon_Graphics : IGraphicsComponent
    {
        public ComponentType Type { get; }
        public Texture2D weaponTexture = Art.PlayerTexture;
        private VerticalSplitPane VSP { get; set; }


        void IGraphicsComponent.Update(Entity entity)
        {
            //Adds Weapon To Player Sprite
            if (Input.KeyboardState.IsKeyDown(Keys.D1))
            {
                entity.weapon.isActiveSword = true;
                entity.weapon.isActiveSpear = false;
                entity.weapon.isActiveAxe = false;
            }
            else if(Input.KeyboardState.IsKeyDown(Keys.D2))
            {
                entity.weapon.isActiveSword = false;
                entity.weapon.isActiveSpear = true;
                entity.weapon.isActiveAxe = false;
            }
            else if (Input.KeyboardState.IsKeyDown(Keys.D3))
            {
                entity.weapon.isActiveSword = false;
                entity.weapon.isActiveSpear = false;
                entity.weapon.isActiveAxe = true;
            }

            if (Input.KeyboardState.CapsLock && !entity.weapon.isReversed)
            {
                if (entity.weapon.isActiveSword)
                    entity.weapon.weaponTexture = Art.SwordTexture;
                else if (entity.weapon.isActiveSpear)
                    entity.weapon.weaponTexture = Art.SpearTexture;
                else if (entity.weapon.isActiveAxe)
                    entity.weapon.weaponTexture = Art.AxeTexture;
            }
            else if (Input.KeyboardState.CapsLock && entity.weapon.isReversed)
            {
                if (entity.weapon.isActiveSword)
                    entity.weapon.weaponTexture = Art.RevSwordTexture;
                else if (entity.weapon.isActiveSpear)
                    entity.weapon.weaponTexture = Art.RevSpearTexture;
                else if (entity.weapon.isActiveAxe)
                    entity.weapon.weaponTexture = Art.RevAxeTexture;
            }

            if (entity.Orientation == SpriteEffects.None)
            {
                entity.weapon.hitbox.X = (int)entity.Position.X + entity.weapon.xoff - 50;
                entity.weapon.hitbox.Y = (int)entity.Position.Y - entity.weapon.yoff;
                entity.weapon.isReversed = false;

            }
            else if (entity.Orientation == SpriteEffects.FlipHorizontally)
            {
                entity.weapon.hitbox.X = (int)entity.Position.X - entity.weapon.xoff - 50;
                entity.weapon.hitbox.Y = (int)entity.Position.Y - entity.weapon.yoff;
                entity.weapon.isReversed= true;
            }



        }
    }
}
