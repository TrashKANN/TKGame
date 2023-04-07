using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections;
using TKGame.BackEnd;
using System.IO;

namespace TKGame.Level_Editor_Content
{
    public class Stage : IEnumerable<Stage>
    {
        private List<Entity> stageEntities;
        private List<Wall> stageWalls;
        private List<Trigger> stageTriggers;
        private string stageName;

        public List<Entity> StageEntities { get { return stageEntities; } }
        public List<Wall> StageWalls { get { return stageWalls; } }
        public List<Trigger> StageTriggers { get { return stageTriggers; } }

        public Stage()
        {
            stageName = "defaultStage.json";
            this.stageWalls = new List<Wall>() { };
            this.stageEntities = new List<Entity>() { };
            this.stageTriggers = new List<Trigger>() { };
            this.stageEntities.Add(Player.Instance);
        }
        public Stage(string name) 
        {
            stageName = name;
            this.stageWalls = new List<Wall>() { };
            this.stageEntities = new List<Entity>() { };
            this.stageTriggers = new List<Trigger>() { };
            this.stageEntities.Add(Player.Instance);
            this.Initialize();
        }

        /// <summary>
        /// When moving stages (aka loading a new stage), clear the entities in the EntityManager and add all
        /// Entities owned by the new Stage.
        /// </summary>
        private void Initialize()
        {
            stageName = File.Exists(stageName)
                ? stageName
                : "defaultStage";
            stageWalls = LevelEditor.LoadStageDataFromJSON(stageName, TKGame.Graphics.GraphicsDevice).StageWalls;
            
            // Will need to put entities in the stage data and do something similar to ^
            //stageEntities = new List<Entity>((IEnumerable<Entity>)Player.Instance);

            EntityManager.GetEntities().Clear();
            foreach (Entity entity in stageEntities)
            {
                EntityManager.Add(entity);
            }
        }

        public void Update()
        {
            EntityManager.Update(TKGame.GameTime);
        }

        public IEnumerator<Stage> GetEnumerator()
        {
            // This is what google said to do
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return this;
        }
    }
}
