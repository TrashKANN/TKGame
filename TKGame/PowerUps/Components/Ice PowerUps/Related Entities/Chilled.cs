using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Content.Weapons;
using TKGame.Players;
using TKGame.Status_Effects;

namespace TKGame.PowerUps.RelatedEntities
{
    public class Chilled : Entity
    {
        private float LifeTime { get; set; }
        private float Duration { get; set; }
        private float TickInterval { get; set; }
        private float DamagePerTick { get; set; }
        private float ElapsedTime { get; set; }

        public Chilled(float lifeTime,
                        float duration,
                        float tickInterval,
                        float damagePerTick,
                        IPhysicsComponent physics_,
                        IGraphicsComponent graphics_)
        {
            // TODO
        }
        public override void Update(GameTime gameTime)
        {
            // TODO
            throw new NotImplementedException();
        }
        public void OnHit(Entity source, Entity target)
        {
            // TODO
            throw new NotImplementedException();
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
