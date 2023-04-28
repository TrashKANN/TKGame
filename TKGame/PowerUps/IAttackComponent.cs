using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.PowerUps
{
    public enum AttackType
    {
        Primary,
        Special,
        Ultimate,
        Movement,
    }
    interface IAttackComponent : ICollideComponent
    {
        AttackType AttackType { get; }
        void Update(Entity entity);
        void OnHit(Entity target);
    }
}
