﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame
{
    class PotionItem : Item
    {
        public PotionItem()
        {
            entityTexture = Art.PotionItemTexture;
            Position = new Vector2(1200, 840); // hard coded spawn position at the moment
        }

        public override void Update(GameTime gameTime)
        {
            //Player player = Player.Instance;

            //if (player != null)
            //{
            //    if (player.Position.X >= Position.X - 20 &&
            //        player.Position.X <= Position.X + 20 &&
            //        player.Position.Y >= Position.Y - 80 &&
            //        player.Position.Y <= Position.Y + 80)
            //    {
            //        Position = new Vector2(1500, 100);
            //    }
            //}
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(entityTexture, Position, null, color, 0, Size / 2f, 1f, Orientation, 0);
        }
    }
}

