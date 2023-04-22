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


namespace TKGame.Components.Concrete
{
    internal class Weapon_GraphicsComponent : GraphicsComponent
    {
        public Texture2D weaponTexture = Art.PlayerTexture;
        private VerticalSplitPane VSP { get; set; }
        void GraphicsComponent.Update(Entity entity)
        {

            //Adds Weapon To Player Sprite
            if (Input.KeyboardState.IsKeyDown(Keys.D1))
            {
                entity.weapon.isActiveSword = true;
                entity.weapon.isActiveSpear = false;
                entity.weapon.isActiveAxe = false;
            }
            //else if (Input.KeyboardState.IsKeyDown(Keys.D2))      // Will Crash if you use the number keys, some problem with moving spritebatch draw into player graphics component;
            //{
            //    entity.weapon.isActiveSword = false;
            //    entity.weapon.isActiveSpear = true;
            //    entity.weapon.isActiveAxe = false;
            //}
            //else if (Input.KeyboardState.IsKeyDown(Keys.D3))
            //{
            //    entity.weapon.isActiveSword = false;
            //    entity.weapon.isActiveSpear = false;
            //    entity.weapon.isActiveAxe = true;
            //}

            if (Input.KeyboardState.CapsLock)
            {
                if (entity.weapon.isActiveSword)
                    entity.entityTexture = Art.PlayerSwordTexture;
                else if (entity.weapon.isActiveSpear)
                    entity.entityTexture = Art.PlayerSpearTexture;
                else if (entity.weapon.isActiveAxe)
                    entity.entityTexture = Art.PlayerAxeTexture;
                else
                    entity.entityTexture = Art.PlayerTexture;
            }
            else
            {
                entity.entityTexture = Art.PlayerTexture;
            }
            
            if (entity.Velocity.X > 0)
            {
                entity.Orientation = SpriteEffects.None;
            }
            else if (entity.Velocity.X < 0)
            {
                entity.Orientation = SpriteEffects.FlipHorizontally;
            }

        }
    }
}
