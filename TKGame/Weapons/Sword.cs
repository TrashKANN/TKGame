﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Content.Weapons
{
    class Sword : Weapon
    {
        private static readonly int WALK_ACCELERATION = 1;
        private static readonly int JUMP_HEIGHT = -1;
        private float MOVEMENT_SPEED = 500f;

        ////Components
        //InputComponent input;
        //PhysicsComponent physics;
        //GraphicsComponent graphics;


        /// <summary>
        /// Private Constructor for Sword
        /// </summary>
        public Sword()
        {
            //input = input_;
            //physics = physics_;
            //graphics = graphics_;
            weaponTexture = Art.WeaponTexture;
            position = new Vector2(815, 730);
            weaponRect = new Rectangle(815, 730, 85, 85);
            damageStat = 1;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            weaponTexture = Art.WeaponTexture;
            //spriteBatch.Draw(weaponTexture, position, weaponRect, color, 0, position, 0.2f, orientation, 0);
            //spriteBatch.Draw(weaponTexture, weaponRect, color);
        }

        public override int DamageEnemy(Weapon weapon)
        {
            throw new NotImplementedException();
        }

        public override void Update(Weapon w)
        {
            if (Input.KeyboardState.IsKeyDown(Keys.A))
                w.velocity.X = -WALK_ACCELERATION * MOVEMENT_SPEED;
            else if (Input.KeyboardState.IsKeyDown(Keys.D))
                w.velocity.X = WALK_ACCELERATION * MOVEMENT_SPEED;
            else if (Input.KeyboardState.IsKeyDown(Keys.W))
                w.velocity.Y = -WALK_ACCELERATION * MOVEMENT_SPEED;
            else if (Input.KeyboardState.IsKeyDown(Keys.S))
                w.velocity.Y = WALK_ACCELERATION * MOVEMENT_SPEED;
            else
                w.velocity = Vector2.Zero;
        }
    }
}
