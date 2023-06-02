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
    class C_Ice_SpecialAttack : ISpecialAttackComponent
    {
        #region Constants
        private readonly float FROZEN_DURATION = 8f;
        private readonly float FROZEN_TICK_INTERVAL = 0.5f;
        private readonly float FROZEN_DAMAGE_PER_TICK = 0f;
        #endregion Constants
        ComponentType IComponent.Type => ComponentType.AttackSpecial;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }

        public C_Ice_SpecialAttack()
        {
            NameID = "IceSpecialAttack";
            AttackType = AttackType.Special;
            HitBox = new Rectangle(new Point(Player.Instance.HitBox.X - 150, Player.Instance.HitBox.Y), new Point(150, 150));
        }
        public void Update(Entity entity)
        {
            if (Input.WasKeyPressed(Keys.F))
            {
                isAttacking = true;
                Entity frozen = new Frozen(0.5f, FROZEN_DURATION, FROZEN_TICK_INTERVAL, FROZEN_DAMAGE_PER_TICK, new C_Frozen_Physics(), new C_Frozen_Graphics());
                EntityManager.Add(frozen);
            }
            else
                isAttacking = false;
        }
        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Frozen_Status(FROZEN_DURATION, FROZEN_TICK_INTERVAL, FROZEN_DAMAGE_PER_TICK, source));
        }
    }
}
