using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Components.Interface
{
    public enum ComponentType
    {
        Input,
        Physics,
        Graphics,
        AttackPrimary,
        AttackSpecial,
        AttackUltimate,
        AttackMovement,
        StatusEffect,
        Burning,
        Scorched,
        Chilled,
        Frozen,
        Shocked,
        Editor,
        Level,
        Item,
    }
    public interface IComponent
    {
        abstract ComponentType Type { get; }
    }
}
