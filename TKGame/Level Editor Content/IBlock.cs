using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGame.Components.Interface;

namespace TKGame.Level_Editor_Content
{
    public interface IBlock : ICollideComponent
    {
        Texture2D Texture { get; set; }
        string Action { get; set; }
    }
}
