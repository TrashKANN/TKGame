﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;

namespace TKGame.Components.Concrete
{
    internal class Enemy_GraphicsComponent : GraphicsComponent
    {
        void GraphicsComponent.Update(Entity entity, SpriteBatch spriteBatch)
        {
            Player player = Player.Instance;
            entity.Velocity.X = 1;
            entity.Velocity.Y = 1;

            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            spriteBatch.Draw(entity.entityTexture,
                                entity.Position,
                                null,
                                entity.color,
                                0,
                                entity.Size / 2f,
                                1f,
                                entity.Orientation,
                                0);
            //spriteBatch.End();

            if (player != null)
            {
                Vector2 playerPosition = player.Position;

                if (entity.Position.X > playerPosition.X)
                {
                    entity.Orientation = SpriteEffects.None; 
                }
                if (entity.Position.X < playerPosition.X)
                {
                    entity.Orientation = SpriteEffects.FlipHorizontally; 
                }
            }
        }
    }
}