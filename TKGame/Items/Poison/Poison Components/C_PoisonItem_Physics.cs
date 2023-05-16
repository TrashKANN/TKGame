using Microsoft.Xna.Framework;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using System.Drawing;
using TKGame.PowerUps.Components;
using TKGame.Players;

namespace TKGame.Items.Poison.Components
{
    class C_PoisonItem_Physics : IPhysicsComponent
    {
        public ComponentType Type => throw new System.NotImplementedException();

        public void Update(Entity entity, GameTime gameTime)
        {
            entity.HitBox = new Microsoft.Xna.Framework.Rectangle((int)entity.Position.X - (int)entity.Size.X / 2,
                                (int)entity.Position.Y - (int)entity.Size.Y / 2,
                                (int)entity.Size.X,
                                (int)entity.Size.Y);
        }
    }
}
