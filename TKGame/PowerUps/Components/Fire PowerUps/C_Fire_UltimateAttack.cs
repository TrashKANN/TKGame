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
using TKGame.Status_Effects;

namespace TKGame.PowerUps.Components.FirePowerUps
{

    class C_Fire_UltimateAttack : IUltimateAttackComponent
    {
        #region Constants
        private readonly float SCORCHED_DURATION = 8f;
        private readonly float SCORCHED_TICK_INTERVAL = 0.5f;
        private readonly float SCORCHED_DAMAGE_PER_TICK = 1.5f;
        #endregion Constants

        ComponentType IComponent.Type => ComponentType.AttackUltimate;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }


        public C_Fire_UltimateAttack()
        {
            NameID = "FireUltimateAttack";
            AttackType = AttackType.Ultimate;
            HitBox = new Rectangle(0, 0, TKGame.ScreenWidth, TKGame.ScreenHeight);
        }
        public void Update(Entity entity)
        {
            // Move this into InputComponent
            if (Input.WasKeyPressed(Keys.Q))
            {
                isAttacking = true;
                //ConfigureHitBox();

                List<Entity> entities = EntityManager.GetEntities();

                foreach (Entity e in entities)
                {
                    if (e != entity && HitBox.Intersects(e.HitBox))
                    {
                        OnHit(entity, e);
                    }
                }
            }
            else
            {
                isAttacking = false;
            }
        }

        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Scorched_Status(SCORCHED_DURATION, SCORCHED_TICK_INTERVAL, SCORCHED_DAMAGE_PER_TICK, source));
        }
    }
}
