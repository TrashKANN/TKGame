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
        private List<IBlock> stageBlocks;
        private List<Entity> stageEntities;
        public string stageName;
        public Background Background;
        public string prevStageName { get; set; }
        public string nextStageName { get; set; }


        public List<IBlock> StageBlocks { get { return stageBlocks; } }
        public List<Entity> StageEntities { get { return stageEntities; } }

        public Stage()
        {
            stageName = "defaultStage.json";
            this.stageBlocks = new List<IBlock>() { };
            this.stageEntities = new List<Entity>() { };
            //this.background = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight, background.backgroundName);
            this.stageEntities.Add(Player.Instance);
        }
        public Stage(string name) 
        {
            stageName = name;
            this.stageBlocks = new List<IBlock>() { };
            this.stageEntities = new List<Entity>() { };
            //this.background = new Background(TKGame.ScreenWidth, TKGame.ScreenHeight, background.backgroundName);
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
            if (stageName == "defaultStage.json")
            {
                throw new Exception("DEFAULTED");
            }
            Stage loaded = LevelEditor.LoadStageDataFromJSON(stageName);

            stageEntities = loaded.stageEntities;
            stageBlocks = loaded.stageBlocks;
            Background = loaded.Background;
            EntityManager.GetEntities().Clear();
            foreach (Entity entity in stageEntities)
            {
                EntityManager.Add(entity);
            }
        }

        public void Update()
        {
            EntityManager.Update(TKGame.GameTime);
            foreach (IBlock block in stageBlocks)
            {
                if (block.Type == ComponentType.Door && (block as Door).checkTrigger())
                {
                    TKGame.levelComponent.GetCurrentLevel().isTransitioning = true;
                    changeStage(block as Door);
                }
            }   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //Draws the image into the Background
                spriteBatch.Draw(Background.BackgroundTexture, Background.BackgroundRect, Color.White);
           
        }

        private void changeStage(Door triggered)
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
