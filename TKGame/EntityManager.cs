using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGame
{
    static class EntityManager
    {
        static List<Entity> entities = new List<Entity>();

        // Will be used later
        //static List<Enemy> enemies = new List<Enemy>();

        static bool IsUpdating;

        // This will be all the entities that are added this frame
        static List<Entity> addedEntities = new List<Entity>();

        public static int Count { get { return entities.Count; } }

        public static void Add(Entity entity)
        {
            if (!IsUpdating)
                AddEntity(entity);
            else 
                addedEntities.Add(entity);
        }

        public static void AddEntity(Entity entity)
        {
            entities.Add(entity);
            // Will be needed for different entity types
            //if (entity is Enemy)
            //    enemies.Add(entity as Enemy);
        }

        public static void Update(GameTime gameTime)
        {
            IsUpdating= true;
            // HandleCollision();

            // Update each entity that from previous frame
            foreach (var entity in entities)
                entity.Update(gameTime);

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

        public static void Draw(SpriteBatch spriteBatch)
        {
            foreach (var entity in entities)
                entity.Draw(spriteBatch);
        }
    }
}
