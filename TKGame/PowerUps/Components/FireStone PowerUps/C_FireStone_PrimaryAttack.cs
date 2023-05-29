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

namespace TKGame.PowerUps.Components.FireStonePowerUps
{
    public class C_FireStone_PrimaryAttack : IPrimaryAttackComponent
    {
        #region Constants
        private readonly float SHOCKED_DURATION = 8f;
        private readonly float SHOCKED_TICK_INTERVAL = 0.5f;
        private readonly float SHOCKED_DAMAGE_PER_TICK = 0f;
        #endregion Constants
        ComponentType IComponent.Type => ComponentType.AttackPrimary;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }

        public C_FireStone_PrimaryAttack()
        {
            NameID = "FireStonePrimaryAttack";
            AttackType = AttackType.Primary;
            // does player hitbox always move when attacking? If so, this isn't necessary
            //HitBox = new Rectangle(new Point(Player.Instance.HitBox.X - 150, Player.Instance.HitBox.Y), new Point(150, 150));
        }
        public void Update(Entity entity)
        {
            // TODO: put in input component

            if (Input.WasKeyPressed(Keys.O))
            {
                isAttacking = true;
                Entity shocked = new Shocked(0.5f, SHOCKED_DURATION, SHOCKED_TICK_INTERVAL, SHOCKED_DAMAGE_PER_TICK, new C_Shocked_Physics(), new C_Shocked_Graphics());
                EntityManager.Add(shocked);
            }
            else
                isAttacking = false;
        }
        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Shocked_Status(SHOCKED_DURATION, SHOCKED_TICK_INTERVAL, SHOCKED_DAMAGE_PER_TICK, source));
        }
    }
}
