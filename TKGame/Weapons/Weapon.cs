﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame.Content.Weapons
{
    public abstract class Weapon
    { 

        public Texture2D weaponTexture;
        public Vector2 position, velocity;
        public Rectangle hitbox;
        public SpriteEffects orientation;
        public bool isActiveSword = true;
        public bool isActiveSpear = false;
        public bool isActiveAxe = false;
        public Color color = Color.White;
        public float damageStat;
        public Entity currentEntity;
        public Vector2 size;
        public int xoff, yoff;

        /// <summary>
        /// Function to draw Weapon Sprite
        /// </summary>
        /// <param name="spriteBatch"></param>
        public abstract void Draw(SpriteBatch spriteBatch);

        /// <summary>
        /// Activates Weapon
        /// </summary>
        public abstract void Activate();
        public abstract void Deactivate();
        /// <summary>
        /// Function to Update Weapon Position
        /// </summary>
        /// <param name="w"></param>
        public abstract void Update(Entity E);
    }
}
