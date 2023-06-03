using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;
using TKGame.Level_Editor_Content;

namespace TKGame.BackEnd
{
    // Might change from static
    static class EntityManager
    {
        static List<Entity> entities = new List<Entity>();

        private static int playerCount = 0;
        private static int enemyCount = 0;

        static bool IsUpdating;

        // This will be all the entities that are added this frame
        static List<Entity> addedEntities = new List<Entity>();

        public static int EntityCount { get { return entities.Count; } }

        public static List<Entity> GetEntities() { return entities; }

        public static bool HasComponent<T>(Entity entity) where T : IComponent
        {
            // Flatten the collection of lists into a single collection of components
            var allComponents = entity.components.Values.SelectMany(x => x);

            // Check if any component is of the desired type
            if (allComponents.Any(component => component.GetType() == typeof(T)))
            {
                return true;
            }

            // Check if the entity itself is of the desired type
            if (entity.GetType() == typeof(T))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds any entities to the current entity list if the entity count is currently being updated, otherwise add it to the being added lists.
        /// </summary>
        /// <param name="entity"></param>
        public static void Add(Entity entity)
        {
            if (!IsUpdating)
                AddEntity(entity);
            else
                addedEntities.Add(entity);
        }

        /// <summary>
        /// Adds entity to the World entity list.
        /// </summary>
        /// <param name="entity"></param>
        private static void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        /// <summary>
        /// Updates each entity in the World entity list. Then adds all new entities to the World list, then clears the new entity list as well as the Expired entities in the World.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime)
        {
            Stage currentStage = TKGame.levelComponent.GetCurrentStage();
            IsUpdating = true;
            // HandleCollision();

            // Update each entity from previous frame
            foreach (var entity in entities)
            {
                entity.Update(gameTime);
                entity.Collide(currentStage.StageBlocks);
            }

            IsUpdating = false;

            // Add all new entities to the new entity list
            foreach (var entity in addedEntities)
                AddEntity(entity);

            // Empty the new entity list
            addedEntities.Clear();

            // Clears expired/despawned entities from the active entity list.
            // Will need to do this for all unique entity lists, i.e. enemies, projectiles, etc.
            entities = entities.Where(x => !x.IsExpired).ToList();

            //Damages Enemies
            if(Input.KeyboardState.CapsLock)
                DamageEnemy(playerCount);
            //Check For Enemies Damaging Player
            EnemyDamage(enemyCount);
        }



        /// <summary>
        /// Calls the Draw() function on each entity.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
            {
                entity.Draw(spriteBatch);
                DrawHealth(spriteBatch, entity);
            }
        }

        /// <summary>
        /// Draws entity health bar
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="entity"></param>
        public static void DrawHealth(SpriteBatch spriteBatch, Entity entity)
        {
            if (entity.needsHealth) //Makes sure that items don't get health bars
            {
                entity.healthTexture = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
                if(entity.entityName == "goblin")
                    entity.healthBar = new Rectangle((int)entity.Position.X - 50, (int)entity.Position.Y - 70, (int)entity.health * 2, 10);
                else
                    entity.healthBar = new Rectangle((int)entity.Position.X - 50, (int)entity.Position.Y - 70, (int)entity.health, 10);
                entity.healthTexture.SetData(new Color[] { Color.Red });
                spriteBatch.Draw(entity.healthTexture, entity.healthBar, Color.White);
            }
        }

        /// <summary>
        /// Function To run through entities and damage any touching the player
        /// </summary>
        /// <param name="count"></param>
        public static void DamageEnemy(int count)
        {

            foreach(Entity entity in entities)
            {
                if(entity.isEnemy && entity.hitBox.Intersects(entities[0].weapon.hitbox)) //checks if players weapon's hitbox intersects with entity's hitbox and if entity is an enemy
                {
                    if (count % 30 == 0)
                    {
                        entity.health = entity.health - entities[0].weapon.damageStat; //don't know how, but this is working now
                    }
                    playerCount++; //updates this loops counter 
                }
            }
        }

        /// <summary>
        /// Checks if the player hitbox intersects with any enemy hitboxes and then decrements player health
        /// </summary>
        /// <param name="count"></param>
        public static void EnemyDamage(int count)
        {
            foreach(Entity entity in entities)
            {
                if ( entity.hitBox.Intersects(entities[0].hitBox) && entity.isEnemy) //checks if player's hitbox intersects with enemy's hitbox
                {
                    if (count % 30 == 0)
                    {
                        entities[0].health -= 2;
                    }
                    enemyCount++; //updates this loops counter
                }
            }

        }
    }
}
