using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TKGame.Components.Interface
{
    public interface IInputComponent
    {
        public void InputComponent() { }
        public void Update(Entity entity);
    }
}
