using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using TKGame.BackEnd;

namespace TKGame.Level_Editor_Content
{
    public class StageGenerator
    {
        private static Random random = new Random();

        private static string[] wallTypes = { "Wall", "Spikes"};

        public static string GenerateStage(string filename)
        {
            StageData stageData = new StageData();
            stageData.blocks = GenerateBlocks();
            stageData.entities = GenerateEntities();
            stageData.background = GenerateBackground();

            // Serializes the data set. The Options make the output human-readable.
            string json = JsonSerializer.Serialize(stageData, new JsonSerializerOptions
            {
                WriteIndented = true,
            });

            // Saves the json into the Level Editor Content Folder for now.
            string directory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../Level Editor Content/Stages"));
            // = "[Your path into TKGames]\\TKGames\\TKGame\\Level Editor Content\\Stages\\"
            // ^ So that we can change it later easily

            // Creates the Directory if it doesn't exist.
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string path = Path.Combine(directory, filename);
            for (int i = 1; i <= 100; i++)
            {
                if (File.Exists(path))
                    path = Path.Combine(directory, filename + "_" + i.ToString() + ".json");
                else
                    break;
            }

            File.WriteAllText(path, json);
            return filename;
        }

        private static List<BlockData> GenerateBlocks()
        {
            List<BlockData> blocks = new List<BlockData>();

            blocks.Add(new BlockData
            {
                type = "Wall",
                X = 0,
                Y = 0,
                width = 32,
                height = 640,
                action = "None"
            });
            blocks.Add(new BlockData
            {
                type = "Wall",
                X = 32,
                Y = 0,
                width = 1536,
                height = 32,
                action = "None"
            });
            blocks.Add(new BlockData
            {
                type = "Wall",
                X = 1568,
                Y = 0,
                width = 32,
                height = 640,
                action = "None"
            });
            blocks.Add(new BlockData
            {
                type = "Wall",
                X = 800,
                Y = 448,
                width = 256,
                height = 256,
                action = "None"
            });
            blocks.Add(new BlockData
            {
                type = "Wall",
                X = 640,
                Y = 576,
                width = 128,
                height = 64,
                action = "None"
            });
            blocks.Add(new BlockData
            {
                type = "Wall",
                X = 0,
                Y = 864,
                width = 1600,
                height = 32,
                action = "None"
            });
            blocks.Add(new BlockData
            {
                type = "Door",
                X = 0,
                Y = 660,
                width = 10,
                height = 195,
                action = "goPrevious"
            });
            blocks.Add(new BlockData
            {
                type = "Door",
                X = 1590,
                Y = 660,
                width = 10,
                height = 195,
                action = "goNext"
            });

            // Generate random walls
            int numWalls = random.Next(5, 10); // Random number of walls between 5 and 10

            for (int i = 0; i < numWalls; i++)
            {
                BlockData wall = new BlockData
                {
                    type = wallTypes[random.Next(wallTypes.Length)],
                    X = random.Next(0, 1500),
                    Y = random.Next(0, 800),
                    width = random.Next(32, 256),
                    height = random.Next(32, 256),
                    action = "None"
                };

                blocks.Add(wall);
            }

            return blocks;
        }

        private static List<EntityData> GenerateEntities()
        {
            List<EntityData> entities = new List<EntityData>();

            entities.Add(new EntityData()
            {
                type = "KnightEnemy",
                X = random.Next(100, 1500),
                Y = random.Next(400, 800)
            });
            entities.Add(new EntityData
            {
                type = "GoblinEnemy",
                X = random.Next(100, 1500),
                Y = random.Next(400, 800)
            });

            string[] itemTypes = { "FireStoneItem", "IceItem", "PoisonItem", "PotionItem" };
            string itemType = itemTypes[random.Next(itemTypes.Length)];

            entities.Add(new EntityData
            {
                type = itemType,
                X = random.Next(100, 1500),
                Y = random.Next(400, 800)
            });

            return entities;
        }

        private static BackgroundData GenerateBackground()
        {
            BackgroundData background = new BackgroundData
            {
                texture = "cobble"
            };

            return background;
        }
    }
}
