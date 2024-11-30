using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Data;

namespace samochod
{
    namespace monogame_test
    {
        public static class Input
        {
            public static KeyboardState previousKeyboard;
            public static KeyboardState currentKeyboard;
            public static MouseState previousMouse;
            public static MouseState currentMouse;
            public static Point mousePosition;

            // player input
            public static bool steerLeft;
            public static bool steerRight;
            public static bool accelerate;
            public static bool brake;
            public static bool shiftGear;

            public static void UpdatePlayer()
            {
                steerLeft = false;
                steerRight = false;
                accelerate = false;
                brake = false;
                shiftGear = false;
                if (IsKeyDown(Keys.A))
                {
                    steerLeft = true;
                }
                if (IsKeyDown(Keys.D))
                {
                    steerRight = true;
                }
                if (IsKeyDown(Keys.W))
                {
                    accelerate = true;
                }
                if (IsKeyDown(Keys.S))
                {
                    brake = true;
                }
                if (IsKeyPressed(Keys.Space))
                {
                    shiftGear = true;
                }

            }

            public static void Update(MouseState mouse, KeyboardState keyboard)
            {
                previousKeyboard = currentKeyboard;
                currentKeyboard = keyboard;
                previousMouse = currentMouse;
                currentMouse = mouse;
                mousePosition = mouse.Position;

                UpdatePlayer();
            }

            public static bool IsKeyPressed(Keys key)
            {
                return (currentKeyboard.IsKeyDown(key) && previousKeyboard.IsKeyUp(key));
            }
            public static bool IsLeftClicked()
            {
                return (currentMouse.LeftButton == ButtonState.Pressed
                    && previousMouse.LeftButton == ButtonState.Released);
            }
            public static bool IsRightClicked()
            {
                return (currentMouse.RightButton == ButtonState.Pressed
                    && previousMouse.RightButton == ButtonState.Released);
            }
            public static bool IsLeftPressed()
            {
                return (currentMouse.LeftButton == ButtonState.Pressed);
            }
            public static bool IsRightPressed()
            {
                return (currentMouse.RightButton == ButtonState.Pressed);
            }
            public static bool IsKeyDown(Keys key)
            {
                return (currentKeyboard.IsKeyDown(key));
            }
            public static bool IsKeyUp(Keys key)
            {
                return (currentKeyboard.IsKeyUp(key));
            }
        }
    }
}
