using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TKGame.BackEnd;
using TKGame.Components.Concrete;
using TKGame.Components.Interface;
using TKGame.Items.FireStone.Components;
using TKGame.PowerUps;
using TKGame.Status_Effects;

// base class for firestone item
namespace TKGame.Items.FireStone
{
    public class FireStoneItem : Item
    {
        private IPhysicsComponent fireStoneItemPhysics = new C_FireStoneItem_Physics();
        private IGraphicsComponent fireStoneItemGraphics = new C_FireStoneItem_Graphics();
        static Random rand = new Random();
        int x = rand.Next(900, 1200);

        public FireStoneItem() 
        {
            Name = "FireStone"; // name for this item is "FireStone"
            Stats = 0;       // stats for this item is 0
            entityTexture = Art.FireStoneItemTexture;
            entityType = EntityType.Item;
            Position = new Vector2(x, 839); // random x-coor spawn
            HitBox = new Microsoft.Xna.Framework.Rectangle((int)Position.X - (int)(Size.X / 2), (int)Position.Y - (int)(Size.Y / 2), (int)Size.X, (int)Size.Y);
            components = new Dictionary<ComponentType, List<IComponent>>
            {

            };
        }

        public override void Update(GameTime gameTime)
        {
            fireStoneItemPhysics.Update(this, gameTime/*, world*/);
        }

        public void Draw()
        {
            fireStoneItemGraphics.Update(this);
        }
    }
}
