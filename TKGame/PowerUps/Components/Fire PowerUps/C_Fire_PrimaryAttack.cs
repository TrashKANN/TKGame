﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.PowerUps.Components.FirePowerUps
{
    class C_Fire_PrimaryAttack : IPrimaryAttackComponent
    {
        ComponentType IComponent.Type => ComponentType.AttackPrimary;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }


        public void PrimaryAttack()
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
