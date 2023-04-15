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
        private List<Wall> stageWalls;
        private List<Entity> stageEntities;
        private List<Trigger> stageTriggers;
        public string stageName;

        public string prevStageName { get; set; }
        public string nextStageName { get; set; }


        public List<Wall> StageWalls { get { return stageWalls; } }
        public List<Entity> StageEntities { get { return stageEntities; } }
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
            stageName = name + ".json";
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
        internal void Initialize()
        {
            string stagePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Level Editor Content/Stages/" + stageName));
            stageName = File.Exists(stagePath)
                ? stageName
                : "defaultStage.json";
            Stage loaded = LevelEditor.LoadStageDataFromJSON(stageName, TKGame.Graphics.GraphicsDevice);

            stageEntities = loaded.stageEntities;
            stageWalls = loaded.stageWalls;
            stageTriggers = loaded.stageTriggers;

            EntityManager.GetEntities().Clear();
            foreach (Entity entity in stageEntities)
            {
                EntityManager.Add(entity);
            }
        }

        public void Update()
        {
            EntityManager.Update(TKGame.GameTime);
            foreach (Trigger trigger in stageTriggers)
            {
                if (trigger.checkTrigger())
                {
                    changeStage(trigger);
                }
            }   
        }

        private void changeStage(Trigger triggered)
        {
            switch (triggered.Action)
            {
                case "goNext":
                    // Go to next stage
                    TKGame.levelComponent.GoToNextStage();
                    break;
                case "goPrevious":
                    // Go to previous stage
                    TKGame.levelComponent.GoToPreviousStage();
                    break;
                default:
                    // Do nothing
                    break;
            }
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
