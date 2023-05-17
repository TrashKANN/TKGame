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
using TKGame.Players;
using TKGame.Components.Interface;

namespace TKGame.Level_Editor_Content
{
    public class Stage : IEnumerable<Stage>
    {
        private List<Wall> stageWalls;
        private List<Entity> stageEntities;
        private List<Trigger> stageTriggers;
        private Background background;
        public string stageName;

        public string prevStageName { get; set; }
        public string nextStageName { get; set; }


        public List<Wall> StageWalls { get { return stageWalls; } }
        public List<Entity> StageEntities { get { return stageEntities; } }
        public List<Trigger> StageTriggers { get { return stageTriggers; } }
        public Background Background { get { return background; } }

        public Stage()
        {
            stageName = "defaultStage.json";
            this.stageWalls = new List<Wall>() { };
            this.stageEntities = new List<Entity>() { };
            this.stageTriggers = new List<Trigger>() { };
            this.background = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight);
            this.stageEntities.Add(Player.Instance);
        }
        public Stage(string name) 
        {
            stageName = name + ".json";
            this.stageWalls = new List<Wall>() { };
            this.stageEntities = new List<Entity>() { };
            this.stageTriggers = new List<Trigger>() { };
            this.background = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight);
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
            Stage loaded = LevelEditor.LoadStageDataFromJSON(stageName);

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
                    TKGame.levelComponent.GetCurrentLevel().isTransitioning = true;
                    changeStage(trigger);
                }
            }   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws the image into the Background
            if (stageName == "room0.json")
                spriteBatch.Draw(Art.BackgroundTexture1, Background.BackgroundRect, Color.White);
            else if (stageName == "room1.json")
                spriteBatch.Draw(Art.BackgroundTexture2, Background.BackgroundRect, Color.White);
            else
                spriteBatch.Draw(Art.BackgroundTexture3, Background.BackgroundRect, Color.White);
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
