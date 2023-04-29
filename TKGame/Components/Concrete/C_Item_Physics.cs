using Microsoft.Xna.Framework;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using System.Drawing;
using TKGame.PowerUps;

namespace TKGame.Components.Concrete
{
    class C_Item_Physics : IPhysicsComponent
    {
        ComponentType IComponent.Type => ComponentType.Physics;
        void IPhysicsComponent.Update(Entity entity, GameTime gameTime/*, World &world*/)
        {
            Player player = Player.Instance;

            if (player != null)
            {
                Vector2 playerPosition = player.Position;
                // if player is within pickup range
                if (player.HitBox.Intersects(entity.HitBox))
                {
                    Player.Instance.PickUpItem(new C_Fire_SpecialAttack());
                }
            }
            entity.HitBox = new Microsoft.Xna.Framework.Rectangle(((int)entity.Position.X - ((int)entity.Size.X / 2)),
                                ((int)entity.Position.Y - (int)entity.Size.Y / 2),
                                (int)entity.Size.X,
                                (int)entity.Size.Y);
        }
    }
}
