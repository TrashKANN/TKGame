using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Linq;


namespace TKGame
{
    static class Input
    {
        private static KeyboardState keyboardState, lastKeyboardState;
        private static MouseState mouseState, lastMouseState;
        private static GamePadState gamepadState, lastGamepadState;

        //No mouse input has been added, this is just for reference later on
        private static bool isAimingWithMouse = false;
        private static readonly float JUMP_HEIGHT = -25.0f;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

        /// <summary>
        /// Updates all game inputs every frame.
        /// </summary>
        public static void Update()
        {
            // Maintain previous frame's state
            lastKeyboardState = keyboardState;
            lastMouseState = mouseState;
            lastGamepadState = gamepadState;

            // Update state to current frame's input
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
            gamepadState = GamePad.GetState(PlayerIndex.One);

            // Disable mouse aiming if GamePad Input detected
            if (gamepadState.ThumbSticks.Left != Vector2.Zero)
                isAimingWithMouse = false;
            else if (MousePosition != new Vector2(lastMouseState.X, lastMouseState.Y))
                isAimingWithMouse = true;
        }

        // Checks if ANY key was just pressed
        /// <summary>
        /// Bool for if any key was not pressed during last frame and it was pressed this frame.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        // Same but for GamePad
        /// <summary>
        /// Bool for if any button was not pressed during last frame and it was pressed this frame.
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public static bool WasButtonPressed(Buttons button)
        {
            return lastGamepadState.IsButtonUp(button) && gamepadState.IsButtonDown(button);
        }

        /// <summary>
        /// Takes the input from Keyboard or GamePad and returns the value on the current frame for movement implementation.
        /// </summary>
        /// <returns></returns>
        public static Vector2 GetMovementDirection()
        {
            Vector2 direction = gamepadState.ThumbSticks.Left;

            if (keyboardState.IsKeyDown(Keys.A))
                direction.X = -1;
            if (keyboardState.IsKeyDown(Keys.D))
                direction.X = 1;
            if (keyboardState.IsKeyDown(Keys.W))
                direction.Y = -1;
            if (keyboardState.IsKeyDown(Keys.S))
                direction.Y = 1;
            if (WasKeyPressed(Keys.Space))
                direction.Y = JUMP_HEIGHT;

            //Clamp Vector length to a max of 1
            //if (direction.LengthSquared() > 1)
            //    direction.Normalize();

            return direction;
        }

        // TODO: Implement Gamepad/Mouse input. Not using either for now
    }
}
