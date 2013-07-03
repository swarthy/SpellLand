using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using XNAGameConsole;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SpellLand
{
    public static class Utilities
    {
        public static GameConsole Console;
        internal static bool Debug = true;
        internal static Game1 Game;
        public static int frameRate = 0;
        static int frameCounter = 0;
        static TimeSpan elapsedTime = TimeSpan.Zero;
        public static void Initialize()
        {
            Game.OnDraw += Draw;
            Game.OnUpdate += Update;
            Game.OnUpdate += KeyboardController.Update;                        
            Game.OnLoadContent += () => { Console.Options.Font = Game.Content.Load<SpriteFont>("consoleFont"); ConsoleInitialize(); };
            KeyboardController.OnKeyDown += (key) => { if (key.ToString() == "Escape") Environment.Exit(0); };
        }
        public static void ConsoleInitialize()
        {
            Console.AddCommand("cursor_position", (args) =>
            {
                if (args.Length > 0)
                {
                    Mouse.SetPosition(int.Parse(args[0]), int.Parse(args[1]));
                    return "";
                }
                else
                    return Mouse.GetState().ToString();
            }, "Position of mouse controller");          
        }
        public static void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
                Game.Window.Title = frameRate.ToString();            
            }            
        }
        public static void Draw(SpriteBatch sb, GameTime gt)
        {
            frameCounter++;
        }
    }
}
