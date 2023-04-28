using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using Microsoft.Xna.Framework.Input;
using TKGame.Components.Interface;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame.PowerUps
{
    class C_Fire_SpecialAttack : ISpecialAttackComponent
    {
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }

        private SpriteBatch atkSpriteBatch;

        public C_Fire_SpecialAttack()
        {
            NameID = "FireSpecialAttack";
            AttackType = AttackType.Special;
            HitBox = new Rectangle(new Point(Player.Instance.HitBox.X - 150, Player.Instance.HitBox.Y), new Point(150, 150));
            atkSpriteBatch = new SpriteBatch(TKGame.Graphics.GraphicsDevice);
        }
        public void Update(Entity entity)
        {
            if (Input.MouseState.RightButton == ButtonState.Pressed)
            {
                isAttacking = true;
                ConfigureHitBox();
            }
            else
            {
                isAttacking = false;
            }
        }
        public void OnHit(Entity target)
        {
            throw new NotImplementedException();
        }

        private void ConfigureHitBox()
        {
            int offset = (Player.Instance.HitBox.Center.X - Player.Instance.HitBox.X);
            if (Player.Instance.isLookingLeft)
            {
                offset = offset * -1 - HitBox.Width;
            }
            HitBox = new Rectangle(Player.Instance.HitBox.Center.X + offset, Player.Instance.HitBox.Y, HitBox.Size.X, HitBox.Size.Y);
        }
    }
}
