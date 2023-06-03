using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public enum AttackType
    {
        Primary,
        Special,
        Ultimate,
        Movement,
    }
    public interface IAttackComponent : ICollideComponent, IComponent
    {
        AttackType AttackType { get; }
        bool isAttacking { get; }
        void Update(Entity entity);
        void OnHit(Entity source, Entity target);
    }
}
