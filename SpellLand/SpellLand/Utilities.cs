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
        internal static Game1 game;
        public static int frameRate = 0;
        static int frameCounter = 0;
        static TimeSpan elapsedTime = TimeSpan.Zero;
        public static void Initialize()
        {            
        }
        public static void ConsoleInitialize()
        {
            Console.Options.Font = game.Content.Load<SpriteFont>("consoleFont");

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

            Console.AddCommand("particles_ttl", (args) =>
            {
                if (args.Length > 0)
                {
                    game.particleEngine.TTL = int.Parse(args[0]);
                    return "";
                }
                else
                    return game.particleEngine.TTL.ToString();
            }, "Particles time to live");

            Console.AddCommand("particles_total_count", (args) =>
            {
                if (args.Length > 0)
                {
                    game.particleEngine.Total = int.Parse(args[0]);
                    return "";
                }
                else
                    return game.particleEngine.Total.ToString();
            }, "Particles count, creating every update cycle");           
        }
        public static void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
                game.Window.Title = frameRate.ToString();            
            }            
        }
        public static void Draw()
        {
            frameCounter++;
        }
    }
}
