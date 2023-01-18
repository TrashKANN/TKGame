using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TKGame
{
    static class Art
    {
        public static Texture2D Player { get; private set; }

        // "Enemy" will be the name of one such as "Goblin" since it has its own art
        // public static Texture2D Enemy { get; private set; }

        public static void LoadContent(ContentManager content)
        {
            Player = content.Load<Texture2D>(@"Art/Player"); // Will need to be modified when we create a better structure for our files.
        }
    }
}
