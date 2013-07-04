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
        static string symbols = "ABCDEFGHIJKLMNOPQRSTUWXYZD0D1D2D3D4D5D6D7D8D9";
        public static KeyboardEvent OnKeyDown, OnSymbolDown;
        public static KeyboardEvent OnKeyUp, OnSymbolUp;
        public static KeyboardState Keyboard { get; private set; }
        public static bool CtrlPressed
        {
            get
            {
                return Keyboard.IsKeyDown(Keys.LeftControl) || Keyboard.IsKeyDown(Keys.RightControl);
            }
        }
        public static bool AltPressed
        {
            get
            {
                return Keyboard.IsKeyDown(Keys.LeftAlt) || Keyboard.IsKeyDown(Keys.RightAlt);
            }
        }
        public static bool ShiftPressed
        {
            get
            {
                return Keyboard.IsKeyDown(Keys.LeftShift) || Keyboard.IsKeyDown(Keys.RightShift);
            }
        }
        public static void Initialize()
        {
            Utilities.Game.OnUpdate += Update;
        }
        public static void Update(GameTime gameTime)
        {
            KeyboardState newkb = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            foreach (Keys k in newkb.GetPressedKeys())            
                if (Keyboard.IsKeyUp(k))
                {
                    if (OnKeyDown != null)
                        OnKeyDown(k);
                    if (OnSymbolDown != null && symbols.Contains(k.ToString()))
                        OnSymbolDown(k);
                }
            foreach (Keys k in Keyboard.GetPressedKeys())
                if (Keyboard.IsKeyDown(k))
                {
                    if (OnKeyUp != null)
                        OnKeyUp(k);
                    if (OnSymbolUp!=null && symbols.Contains(k.ToString()))
                        OnSymbolUp(k);
                }
            Keyboard = newkb;
        }
    }    
}
