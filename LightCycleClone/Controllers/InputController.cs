using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightCycleClone
{
    public class InputController
    {
        private KeyboardState _keyState;

        public void Update(KeyboardState keyboardState)
        {
            _keyState = keyboardState;
        }

        public PlayerAction GetPlayerAction()
        {
            var keysPressed = _keyState.GetPressedKeys().FirstOrDefault();

            switch (keysPressed)
            {
                case (Keys.W):
                    return PlayerAction.MoveUp;
                case (Keys.S):
                    return PlayerAction.MoveDown;
                case (Keys.A):
                    return PlayerAction.MoveLeft;
                case (Keys.D):
                    return PlayerAction.MoveRight; 
            }

            return PlayerAction.NoAction;
        }
    }
}
