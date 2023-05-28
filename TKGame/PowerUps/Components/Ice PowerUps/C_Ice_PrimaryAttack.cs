using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Players;
using TKGame.PowerUps.RelatedEntities;
using TKGame.Status_Effects;

namespace TKGame.PowerUps.Components.IcePowerUps
{
    class C_Ice_PrimaryAttack : IPrimaryAttackComponent
    {
        #region Constants
        private readonly float CHILLED_DURATION = 8f;
        private readonly float CHILLED_TICK_INTERVAL = 0.5f;
        private readonly float CHILLED_DAMAGE_PER_TICK = 0f;
        #endregion Constants
        ComponentType IComponent.Type => ComponentType.AttackPrimary;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }

        public C_Ice_PrimaryAttack()
        {
            NameID = "IcePrimaryAttack";
            AttackType = AttackType.Primary;
            // does player hitbox always move when attacking? If so, this isn't necessary
            //HitBox = new Rectangle(new Point(Player.Instance.HitBox.X - 150, Player.Instance.HitBox.Y), new Point(150, 150));
        }
        public void Update(Entity entity)
        {
            // TODO: put in input component

            if (Input.WasKeyPressed(Keys.I))
            {
                isAttacking = true;
                Entity fireball = new Chilled(0.5f, CHILLED_DURATION, CHILLED_TICK_INTERVAL, CHILLED_DAMAGE_PER_TICK, new C_Chilled_Physics(), new C_Chilled_Graphics()); 
                EntityManager.Add(fireball);
            }
            else
                isAttacking = false;
        }
        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Chilled_Status(CHILLED_DURATION, CHILLED_TICK_INTERVAL, CHILLED_DAMAGE_PER_TICK, source));
        }
    }
}
