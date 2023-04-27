﻿using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;

namespace TKGame.Components.Concrete
{
    internal class Player_GraphicsComponent : GraphicsComponent
    {
        GraphicsComponent weaponGraphics = new Weapon_GraphicsComponent();
        Player player = Player.Instance; // instance of Player to access variables
        void GraphicsComponent.Update(Entity entity)
        {
            weaponGraphics.Update(entity);
            if (entity.Velocity.X > 0)
            {
                entity.Orientation = SpriteEffects.None;
            }
            else if (entity.Velocity.X < 0)
            {
                entity.Orientation = SpriteEffects.FlipHorizontally;
            }
            // Moved this entirely out of Entity and into this component.
            TKGame.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);
            
            TKGame.SpriteBatch.Draw(entity.entityTexture,
                                entity.Position,
                                null,
                                entity.color,
                                0,
                                entity.Size / 2f,
                                1f,
                                entity.Orientation,
                                0);
            TKGame.SpriteBatch.End();
        }
    }
}
