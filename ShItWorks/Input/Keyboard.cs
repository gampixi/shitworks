using System;
using OpenTK;

namespace ShItWorks.Input
{
    public static class Keyboard
    {
        private static OpenTK.Input.KeyboardState lastState;
        private static OpenTK.Input.KeyboardState currentState;
        private static bool stateSet = false;
        private static uint stateFrame = 0;
        public static OpenTK.Input.KeyboardState CurrentState
        { get
            {
                if(stateSet == false || stateFrame != Game.Current.TotalFrames)
                {
                    lastState = currentState;
                    currentState = OpenTK.Input.Keyboard.GetState();
                    stateSet = true;
                    stateFrame = Game.Current.TotalFrames;
                }
                return currentState;
            }
        }

        public static bool KeyDown(OpenTK.Input.Key key)
        {
            return CurrentState.IsKeyDown(key);
        }

        public static bool KeyPressed(OpenTK.Input.Key key)
        {
            return (CurrentState.IsKeyDown(key) && lastState.IsKeyUp(key));
        }

        public static bool KeyUp(OpenTK.Input.Key key)
        {
            return CurrentState.IsKeyUp(key);
        }

        public static bool KeyReleased(OpenTK.Input.Key key)
        {
            return (CurrentState.IsKeyUp(key) && lastState.IsKeyDown(key));
        }
    }
}
