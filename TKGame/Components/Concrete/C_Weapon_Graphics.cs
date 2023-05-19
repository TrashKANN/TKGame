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

            if (Input.KeyboardState.CapsLock)
            {
                if (entity.weapon.isActiveSword)
                    entity.weapon.weaponTexture = Art.SwordTexture;
                else if (entity.weapon.isActiveSpear)
                    entity.weapon.weaponTexture = Art.SpearTexture;
                else if (entity.weapon.isActiveAxe)
                    entity.weapon.weaponTexture = Art.PlayerAxeTexture;
                else
                    entity.weapon.weaponTexture = Art.SwordTexture;
            }
            else
            {
                entity.entityTexture = Art.PlayerTexture;
            }
            
            if (entity.Velocity.X > 0)
            {
                entity.weapon.orientation = SpriteEffects.None;
            }
            else if (entity.Velocity.X < 0)
            {
                entity.weapon.orientation = SpriteEffects.FlipHorizontally;
            }

            if (entity.Orientation == SpriteEffects.None)
            {
                entity.weapon.position.X = entity.Position.X + entity.weapon.xoff;
                entity.weapon.position.Y = entity.Position.Y - entity.weapon.yoff;

            }
            else if (entity.Orientation == SpriteEffects.FlipHorizontally)
            {
                entity.weapon.position.X = entity.Position.X - entity.weapon.xoff;
                entity.weapon.position.Y = entity.Position.Y - entity.weapon.yoff;
            }

        }
    }
}
