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

namespace TKGame.PowerUps
{
    class C_Fire_SpecialAttack : ISpecialAttackComponent
    {
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }

        private Point leftHitBoxPoint = new Point(Player.Instance.GetHitBox().X - 150, Player.Instance.GetHitBox().Y);
        private Point rightHitBoxPoint = new Point(Player.Instance.GetHitBox().X + Player.Instance.GetHitBox().Width + 150, Player.Instance.GetHitBox().Y);

        public C_Fire_SpecialAttack()
        {
            NameID = "FireSpecialAttack";
            AttackType = AttackType.Special;
            HitBox = new Rectangle(leftHitBoxPoint, new Point(150, 150));
        }
        public void Update(Entity entity)
        {
            if (Input.WasKeyPressed(Keys.E))
            {
                DrawHitBox();
            }
        }
        public void OnHit(Entity target)
        {
            throw new NotImplementedException();
        }

        private void DrawHitBox()
        {
            ConfigureHitBox();
            TKGame.SpriteBatch.Begin();
            GameDebug.DrawBoundingBox(HitBox, Color.Red, 3);
            TKGame.SpriteBatch.End();
        }

        private void ConfigureHitBox()
        {
            if (Player.Instance.isLookingLeft)
            {
                HitBox = new Rectangle(leftHitBoxPoint, HitBox.Size);
            }
            else
            {
                HitBox = new Rectangle(rightHitBoxPoint, HitBox.Size);
            }
        }
    }
}
