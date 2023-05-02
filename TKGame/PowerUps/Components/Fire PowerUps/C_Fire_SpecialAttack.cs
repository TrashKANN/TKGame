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
using TKGame.Status_Effects;
using TKGame.Players;
using TKGame.PowerUps.RelatedEntities;

namespace TKGame.PowerUps.Components.FirePowerUps
{
    class C_Fire_SpecialAttack : ISpecialAttackComponent
    {
        #region Constants
        private readonly float BURNING_DURATION = 8f;
        private readonly float BURNING_TICK_INTERVAL = 0.5f;
        private readonly float BURNING_DAMAGE_PER_TICK = 1.5f;
        #endregion Constants
        ComponentType IComponent.Type => ComponentType.AttackSpecial;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; private set; }
        public bool isAttacking { get; private set; }

        public C_Fire_SpecialAttack()
        {
            NameID = "FireSpecialAttack";
            AttackType = AttackType.Special;
            //HitBox = new Rectangle(new Point(Player.Instance.HitBox.X - 150, Player.Instance.HitBox.Y), new Point(150, 150));
        }
        public void Update(Entity entity)
        {
            // if the player is attacking, the hitbox is updated to follow the player
            // Move this into InputComponent
            if (Input.WasKeyPressed(Keys.E))
            {
                isAttacking = true;

                Entity fireball = new Fireball(0.5f, BURNING_DURATION, BURNING_TICK_INTERVAL, BURNING_DAMAGE_PER_TICK, new C_FireBall_Physics(), new C_FireBall_Graphics()); // Replace with your fire entity instantiation logic
                EntityManager.Add(fireball);
            }
            else
            {
                isAttacking = false;
            }
        }
        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Burning_Status(BURNING_DURATION, BURNING_TICK_INTERVAL, BURNING_DAMAGE_PER_TICK, source));
        }
    }
}
