using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.BackEnd;
using TKGame.Components.Interface;
using TKGame.Players;
using TKGame.PowerUps.RelatedEntities;
using TKGame.Status_Effects;

namespace TKGame.PowerUps.Components.FirePowerUps
{
    class C_Fire_MovementAttack : IMovementAttackComponent
    {
        #region Constants
        private readonly float BURNING_DURATION = 8f;
        private readonly float BURNING_TICK_INTERVAL = 0.5f;
        private readonly float BURNING_DAMAGE_PER_TICK = 1.5f;
        private readonly float DashSpeed = 800f;
        private readonly float DashDuration = 0.3f;
        private readonly float FireSpawnInterval = 0.05f;
        #endregion Constants
        ComponentType IComponent.Type => ComponentType.AttackMovement;
        public string NameID { get; private set; }
        public Rectangle HitBox { get; set; }
        public AttackType AttackType { get; private set; }
        public bool isAttacking { get; private set; }
        private float ElapsedTime;
        private float TimeSinceFireSpawned;



        public void MovementAttack()
        {
            NameID = "FireMovementAttack";
            AttackType = AttackType.Special;
            HitBox = new Rectangle();
        }
        public void Update(Entity entity)
        {
            if (Input.WasKeyPressed(Keys.LeftShift))
            {
                ElapsedTime = 0f;
                TimeSinceFireSpawned = 0f;
                isAttacking = true;
                
                //ConfigureHitBox();

                List<Entity> entities = EntityManager.GetEntities();

                foreach (Entity e in entities)
                {
                    if (e != entity && HitBox.Intersects(e.HitBox))
                    {
                        OnHit(entity, e);
                    }
                }
            }

            if (isAttacking)
            {
                ElapsedTime += (float)TKGame.GameTime.ElapsedGameTime.TotalSeconds;

                DashUpdate(TKGame.GameTime, entity);

                if (ElapsedTime >= DashDuration)
                {
                    isAttacking = false;
                }
            }


        }

        private void DashUpdate(GameTime gameTime, Entity entity)
        {
            if (!isAttacking)
            {
                return;
            }

            TimeSinceFireSpawned += (float)gameTime.ElapsedGameTime.TotalSeconds;

            int direction = Player.Instance.isLookingLeft ? -1 : 1;
            entity.Position.X += direction * DashSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (TimeSinceFireSpawned >= FireSpawnInterval)
            {
                // Instantiate a fire object and place it at the player's current position
                Entity fire = new FireStep(5f, 5f, 1f, 0.5f, new C_FireStep_Physics(), new C_FireStep_Graphics()); // Replace with your fire entity instantiation logic
                fire.Position.X = entity.Position.X; // Set the fire object's position to the player's position
                fire.Position.Y = entity.HitBox.Bottom - fire.Size.Y/2;

                EntityManager.Add(fire);

                TimeSinceFireSpawned = 0f;
            }
        }

        public void OnHit(Entity source, Entity target)
        {
            target.AddComponent(new C_Burning_Status(BURNING_DURATION, BURNING_TICK_INTERVAL, BURNING_DAMAGE_PER_TICK, source));
        }
    }
}
