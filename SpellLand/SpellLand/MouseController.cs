using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpellLand
{
    public delegate void MouseEvent(MouseState state);
    public static class MouseController
    {
        public static MouseEvent OnDown, OnUp, OnMove;        
        public static MouseState Mouse { get; private set; }
        public static bool LeftPressed
        {
            get
            {
                return Mouse.LeftButton == ButtonState.Pressed;
            }
        }
        public static bool RightPressed
        {
            get
            {
                return Mouse.RightButton == ButtonState.Pressed;
            }
        }
        public static bool MiddlePressed
        {
            get
            {
                return Mouse.MiddleButton == ButtonState.Pressed;
            }
        }
        public static void Initialize()
        {
            Utilities.Game.OnUpdate += Update;
        }
        public static void Update(GameTime gameTime)
        {
            MouseState newms = Microsoft.Xna.Framework.Input.Mouse.GetState();

            if (((newms.LeftButton == ButtonState.Pressed && Mouse.LeftButton == ButtonState.Released) ||
                (newms.RightButton == ButtonState.Pressed && Mouse.RightButton == ButtonState.Released))
                && OnDown != null)
                OnDown(newms);
            if (((newms.LeftButton == ButtonState.Released && Mouse.LeftButton == ButtonState.Pressed) ||
                (newms.RightButton == ButtonState.Released && Mouse.RightButton == ButtonState.Pressed))
                && OnUp != null)
                OnUp(newms);              
            
            if ((Mouse.X != newms.X || Mouse.Y != newms.Y) && OnMove != null)
                OnMove(newms);

            Mouse = newms;
        }
    }
}
