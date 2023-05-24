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
    class C_Ice_UltimateAttack : IUltimateAttackComponent
    {
        ComponentType IComponent.Type => ComponentType.AttackUltimate;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }

        public C_Ice_UltimateAttack()
        {
            throw new NotImplementedException();
        }
        public void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
        public void OnHit(Entity source, Entity target)
        {
            throw new NotImplementedException();
        }
    }
}
