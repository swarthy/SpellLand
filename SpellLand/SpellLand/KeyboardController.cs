using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpellLand
{
    public delegate void KeyboardEvent(Keys key);
    public static class KeyboardController
    {
        public static KeyboardEvent OnKeyDown;
        public static KeyboardEvent OnKeyUp;
        public static KeyboardState Keyboard { get; private set; }
        public static void Update(GameTime gameTime)
        {
            KeyboardState newkb = Microsoft.Xna.Framework.Input.Keyboard.GetState();            
            foreach (Keys k in newkb.GetPressedKeys())
                if (Keyboard.IsKeyUp(k) && OnKeyDown != null)
                    OnKeyDown(k);
            foreach (Keys k in Keyboard.GetPressedKeys())
                if (newkb.IsKeyUp(k) && OnKeyUp != null)
                    OnKeyUp(k);
            Keyboard = newkb;
        }
    }    
}
