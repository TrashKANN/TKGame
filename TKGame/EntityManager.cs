using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Level_Editor_Content;

namespace TKGame
{
    // Might change from static
    static class EntityManager
    {
        static List<Entity> entities = new List<Entity>();

        // Will be used later
        //static List<Enemy> enemies = new List<Enemy>();

        static bool IsUpdating;

        // This will be all the entities that are added this frame
        static List<Entity> addedEntities = new List<Entity>();

        public static int EntityCount { get { return entities.Count; } }

        public static List<Entity> GetEntities() { return entities; }

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
        public static void AddEntity(Entity entity)
        {
            entities.Add(entity);
            // Will be needed for different entity types
            //if (entity is Enemy)
            //    enemies.Add(entity as Enemy);
        }

        /// <summary>
        /// Updates each entity in the World entity list. Then adds all new entities to the World list, then clears the new entity list as well as the Expired entities in the World.
        /// </summary>
        /// <param name="gameTime"></param>
        public static void Update(GameTime gameTime, SpriteBatch spriteBatch, Stage stage)
        {
            IsUpdating = true;
            // HandleCollision();
            
            // Update each entity from previous frame
            foreach (var entity in entities)
            {
                entity.Update(gameTime, spriteBatch);
                entity.Collide(stage.walls);
            }

            IsUpdating= false;

            // Add all new entities to the new entity list
            foreach (var entity in addedEntities)
                AddEntity(entity);

            // Empty the new entity list
            addedEntities.Clear();

            // Clears expired/despawned entities from the active entity list.
            // Will need to do this for all unique entity lists, i.e. enemies, projectiles, etc.
            entities = entities.Where(x => !x.IsExpired).ToList();
        }



        /// <summary>
        /// Calls the Draw() function on each entity.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
