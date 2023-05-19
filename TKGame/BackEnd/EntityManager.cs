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
                entity.Collide(currentStage.StageWalls);
            }

            IsUpdating = false;

            // Add all new entities to the new entity list
            foreach (var entity in addedEntities)
                AddEntity(entity);

            // Empty the new entity list
            addedEntities.Clear();

            // Clears expired/despawned entities from the active entity list.
            // Will need to do this for all unique entity lists, i.e. enemies, projectiles, etc.
            entities = entities.Where(x => !x.IsExpired/* || x.health <= 0*/).ToList();

            //Damages Enemies
            DamageEnemy();
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
                entity.healthBar = new Rectangle((int)entity.Position.X - 50, (int)entity.Position.Y - 70, 100, 10);
                entity.healthTexture.SetData(new Color[] { Color.Red });
                spriteBatch.Draw(entity.healthTexture, entity.healthBar, Color.White);
            }
        }
        
        /// <summary>
        /// Function To run through entities and damage any touching the player
        /// </summary>
        public static void DamageEnemy()
        {
            foreach (Entity entity in entities)
            {
                if (entities[0].entityTexture == Art.PlayerTexture) //Checks if player has sword out
                {
                    if (entities[0].weapon.hitbox.Intersects(entity.hitBox) && entity != entities[0]) //Checks if hitboxes intersect and is not the player
                    {
                        entity.health -= (int)(entities[0].weapon.damageStat);
                    }
                }
            }
        }
    }
}
