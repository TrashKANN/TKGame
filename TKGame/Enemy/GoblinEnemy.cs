using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.BackEnd;
using TKGame.Status_Effects;

namespace TKGame
{
    public class GoblinEnemy : Enemy
    {
        private IPhysicsComponent goblinEnemyPhysics = new C_Goblin_Physics();
        private IGraphicsComponent goblinEnemyGraphics = new C_Enemy_Graphics();

        /// <summary>
        /// goblin enemy components
        /// </summary>
        public GoblinEnemy()
        {
            entityTexture = Art.GoblinEnemyTexture; 
            Position = new Vector2(200, 800); // hard coded spawn position at the moment
            velocity = new Vector2((float)1.5, 1);
            HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
            components = new Dictionary<ComponentType, IComponent>
            {

            };
        }

        /// <summary>
        /// Update goblin enemy components
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        public override void Update(GameTime gameTime)
        {
            goblinEnemyPhysics.Update(this, gameTime/*, world*/);
            foreach (IComponent component in components.Values)
            {
                ((C_Burning_Status)component).Update(gameTime, this);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //goblinEnemyGraphics.Update(this/*, spriteBatch*/);
            base.Draw(spriteBatch);
        }
    }
}
