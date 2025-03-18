using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group4_Ng_Santos_Final.Games
{
    public static class Controls
    {
        private static bool controlsEnabled = true;

        public static void UpdatePosition(ref Vector2 position, float speed = 8f)
        {
            // If controls are disabled, the plane will fall to the right
            if (!controlsEnabled)
            {
                position.X += speed;
                position.Y += speed; // Plane falls to the right when controls are disabled
                return;
            }
            
            if (SkyFall._platform == Platform.Windows) // If the platform is Windows, use keyboard controls
            {
                var keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.W) || keyboardState.IsKeyDown(Keys.Up))
                    position.Y -= speed;
                if (keyboardState.IsKeyDown(Keys.S) || keyboardState.IsKeyDown(Keys.Down))
                    position.Y += speed;
                if (keyboardState.IsKeyDown(Keys.A) || keyboardState.IsKeyDown(Keys.Left))
                    position.X -= speed;
                if (keyboardState.IsKeyDown(Keys.D) || keyboardState.IsKeyDown(Keys.Right))
                    position.X += speed;
            }
            else if (SkyFall._platform == Platform.Android) // If the platform is Android, use touch controls
            {
                while (TouchPanel.IsGestureAvailable)
                {
                    GestureSample gesture = TouchPanel.ReadGesture();
                    switch (gesture.GestureType)
                    {
                        case GestureType.Tap:
                            // Handle tap gesture
                            break;
                        case GestureType.FreeDrag:
                            Airplane.Instance.Position = gesture.Position;
                            break;
                    }
                }
            }
        }

        public static void DisableControls() // Disables controls
        {
            controlsEnabled = false;
        }

        public static void EnableControls() // Enables controls
        {
            controlsEnabled = true;
        }
    }
}
