﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using Myra.Graphics2D.UI;
using System.Drawing;
using TKGame.Players;

namespace TKGame.Enemies.Goblin.Components
{
    class C_GoblinEnemy_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/)
        {
            Players.Player player = Players.Player.Instance;
            entity.Velocity.X = 2;
            entity.Velocity.Y = 2;

            if (player != null)
            {
                Vector2 playerPosition = player.Position;

                if (entity.Position.X > playerPosition.X)
                {
                    entity.Position.X -= entity.Velocity.X;
                }
                if (entity.Position.X < playerPosition.X)
                {
                    entity.Position.X += entity.Velocity.X;
                }
                //if (entity.Position.Y > playerPosition.Y)
                //{
                //    entity.Position.Y -= entity.Velocity.Y;
                //}
                //if (entity.Position.Y < playerPosition.Y)
                //{
                //    entity.Position.Y += entity.Velocity.Y;
                //}

                entity.hitBox.X = (int)entity.Position.X - (int)entity.Size.X / 2;
                entity.hitBox.Y = (int)entity.Position.Y - (int)entity.Size.Y / 2;
                entity.Position = Vector2.Clamp(entity.Position, entity.Size / 2, TKGame.ScreenSize - entity.Size / 2);

                entity.isDead();
            }
        }
    }
}