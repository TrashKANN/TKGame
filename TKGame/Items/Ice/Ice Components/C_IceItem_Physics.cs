using Microsoft.Xna.Framework;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using System.Drawing;
using TKGame.PowerUps.Components;
using TKGame.Players;
using TKGame.PowerUps.Components.FirePowerUps;

namespace TKGame.Items.Ice.Components
{
    class C_IceItem_Physics : IPhysicsComponent
    {
        public ComponentType Type => throw new System.NotImplementedException();

        public void Update(Entity entity, GameTime gameTime)
        {
            //Player player = Player.Instance;

            //if (player != null)
            //{
            //    Vector2 playerPosition = player.Position;

            //    // if player is within pickup range
            //    if (player.HitBox.Intersects(entity.HitBox))
            //    {
            //        Player.Instance.PickUpPowerUp(new C_Fire_UltimateAttack());
            //    }
            //}
            entity.HitBox = new Microsoft.Xna.Framework.Rectangle((int)entity.Position.X - (int)entity.Size.X / 2,
                                (int)entity.Position.Y - (int)entity.Size.Y / 2,
                                (int)entity.Size.X,
                                (int)entity.Size.Y);
        }
    }
}
