using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Data;

namespace samochod
{
    namespace monogame_test
    {
        public static class Input
        {
            private static KeyboardState previousKeyboard;
            private static KeyboardState currentKeyboard;
            private static MouseState previousMouse;
            private static MouseState currentMouse;
            public static Point mousePosition;
            public static bool mouseInBounds;
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
            public static bool mouseInside(Rectangle rect){
                if (rect.Contains(mousePosition))
                return true;
                return false;
                }

            public static void Update(MouseState mouse, KeyboardState keyboard)
            {
                previousKeyboard = currentKeyboard;
                currentKeyboard = keyboard;
                previousMouse = currentMouse;
                currentMouse = mouse;
                mousePosition = mouse.Position;
                mouseInBounds = mouse.Position.X > 0 && mouse.Position.Y > 0 && 
                    mouse.Position.X < Game1.ResX && mouse.Position.Y < Game1.ResY;
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
