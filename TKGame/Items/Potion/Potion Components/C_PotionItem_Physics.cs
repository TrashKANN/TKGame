using Microsoft.Xna.Framework;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using System.Drawing;
using TKGame.PowerUps.Components;
using TKGame.Players;
using TKGame.PowerUps.Components.FirePowerUps;
using TKGame.PowerUps.Components.FireStonePowerUps;
using System;

namespace TKGame.Items.Potion.Components
{
    class C_PotionItem_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/)
        {
            Player player = Player.Instance;

            if (player != null)
            {
                Vector2 playerPosition = player.Position;

                // if player is within pickup range
                // This is mainly for testing purposes. I will make powerup items soon
                if (player.HitBox.Intersects(entity.HitBox))
                {
                    Random random = new Random();
					switch (random.Next(3))
                    {
                        case 0:
					        Player.Instance.PickUpPowerUp(new C_Fire_MovementAttack());
                            break;
                        case 1:
					        Player.Instance.PickUpPowerUp(new C_Fire_SpecialAttack());
                            break;
                        case 2:
					        Player.Instance.PickUpPowerUp(new C_Fire_UltimateAttack());
                            break;
                        default:
                            break;
                    }
				}
            }
            entity.HitBox = new Microsoft.Xna.Framework.Rectangle((int)entity.Position.X - (int)entity.Size.X / 2,
                                (int)entity.Position.Y - (int)entity.Size.Y / 2,
                                (int)entity.Size.X,
                                (int)entity.Size.Y);
        }
    }
}
