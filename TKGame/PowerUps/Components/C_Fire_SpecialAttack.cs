using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using Microsoft.Xna.Framework.Input;
using TKGame.Components.Interface;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using TKGame.Status_Effects;
using TKGame.Players;

namespace TKGame.PowerUps.Components
{
    class C_Fire_SpecialAttack : ISpecialAttackComponent
    {
        ComponentType IComponent.Type => ComponentType.AttackSpecial;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; }
        public bool isAttacking { get; private set; }

        private SpriteBatch atkSpriteBatch;

        public C_Fire_SpecialAttack()
        {
            NameID = "FireSpecialAttack";
            AttackType = AttackType.Special;
            HitBox = new Rectangle(new Point(Players.Player.Instance.HitBox.X - 150, Players.Player.Instance.HitBox.Y), new Point(150, 150));
            atkSpriteBatch = new SpriteBatch(TKGame.Graphics.GraphicsDevice);
        }
        public void Update(Entity entity)
        {
            // if the player is attacking, the hitbox is updated to follow the player
            // Move this into InputComponent
            if (Input.MouseState.RightButton == ButtonState.Pressed)
            {
                isAttacking = true;
                ConfigureHitBox();

                //List<Entity> entities = EntityManager.GetEntities()
                //    .Where(e => EntityManager.HasComponent<ICollideComponent>(e))
                //    .ToList();

                List<Entity> entities = EntityManager.GetEntities();

                foreach (Entity e in entities)
                {
                    if (e != entity && HitBox.Intersects(e.HitBox) && !EntityManager.HasComponent<C_Burning_Status>(e))
                    {
                        OnHit(entity, e);
                    }
                }
            }
            else
            {
                isAttacking = false;
            }
        }
        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Burning_Status(8, 1f, 0.2f, Players.Player.Instance));
        }

        private void ConfigureHitBox()
        {
            // offsets the hitbox adjacent to the player's hitbox based on the direction the player is facing
            int offset = Players.Player.Instance.HitBox.Center.X - Players.Player.Instance.HitBox.X;

            if (Players.Player.Instance.isLookingLeft)
            {
                offset = offset * -1 - HitBox.Width;
            }
            HitBox = new Rectangle(Players.Player.Instance.HitBox.Center.X + offset, Players.Player.Instance.HitBox.Y, HitBox.Width, HitBox.Height);
        }
    }
}
