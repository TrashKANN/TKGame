using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.PowerUps;

namespace TKGame.Status_Effects
{
    public enum StatusType
    {
        Burning,
        Chilled,
        Frozen,
        Shocked,
    }
    public interface IStatusComponent : IComponent
    {
        StatusType StatusType { get; }
        float Duration { get; }
        float TickInterval { get; }
        float DamagePerTick { get; }
        Entity SourceEntity { get; }
        float ElapsedTime { get; set; }
        float TimeSinceLastTick { get; set; }
        void Initialize(float duration, float tickInterval, float damagePerTick, Entity sourceEntity);
    }
}
