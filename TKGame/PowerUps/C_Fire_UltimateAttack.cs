﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.PowerUps
{
    class C_Fire_UltimateAttack : IUltimateAttackComponent
    {
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }

        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }


        public void Attack()
        {
            NameID = "UltimateAttack";
        }
        public void Update(Entity entity)
        {
            throw new NotImplementedException();
        }
        public void OnHit(Entity target)
        {
            throw new NotImplementedException();
        }
    }
}
