using Microsoft.Xna.Framework;
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
        private readonly Random random = new Random();
        private const float MinVelocity = 0.4f;
        private const float MaxVelocity = 2.0f;
        private const float PowerLawExponent = 2.0f; // Adjust this value to control the distribution shape

        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/)
        {
            Players.Player player = Players.Player.Instance;
            float velocity = GetRandomVelocity();
            entity.Velocity.X = velocity;
            //entity.Velocity.Y = velocity;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

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

                entity.Position.Y += entity.Velocity.Y * deltaTime;

                entity.resolveVerticalCollision(entity, deltaTime);


                entity.hitBox.X = (int)entity.Position.X - (int)entity.Size.X / 2;
                entity.hitBox.Y = (int)entity.Position.Y - (int)entity.Size.Y / 2;
                entity.Position = Vector2.Clamp(entity.Position, entity.Size / 2, TKGame.ScreenSize - entity.Size / 2);

                entity.isDead();
            }
        }

        private float GetRandomVelocity()
        {
            float u = (float)random.NextDouble(); // Random value between 0 and 1
            float velocity = (float)(MinVelocity + (MaxVelocity - MinVelocity) * Math.Pow(u, PowerLawExponent));
            return velocity;
        }
    }
}
