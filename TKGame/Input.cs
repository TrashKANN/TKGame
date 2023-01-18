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

        private static bool isAimingWithMouse = false;

        public static Vector2 MousePosition { get { return new Vector2(mouseState.X, mouseState.Y); } }

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
        public static bool WasKeyPressed(Keys key)
        {
            return lastKeyboardState.IsKeyUp(key) && keyboardState.IsKeyDown(key);
        }

        // Same but for GamePad
        public static bool WasButtonPressed(Buttons button)
        {
            return lastGamepadState.IsButtonUp(button) && gamepadState.IsButtonDown(button);
        }

        public static Vector2 GetMovementDirection()
        {
            Vector2 direction = gamepadState.ThumbSticks.Left;

            if (keyboardState.IsKeyDown(Keys.A))
                direction.X -= 1;
            if (keyboardState.IsKeyDown(Keys.D))
                direction.X += 1;
            if (keyboardState.IsKeyDown(Keys.W))
                direction.Y -= 1;
            if (keyboardState.IsKeyDown(Keys.S))
                direction.Y += 1;

            //Clamp Vector length to a max of 1
            if (direction.LengthSquared() > 1)
                direction.Normalize();

            return direction;
        }

        // TODO: Implement Gamepad/Mouse input. Not using either for now
    }
}
