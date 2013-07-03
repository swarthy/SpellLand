using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpellLand
{
    public static class Interface
    {
        public static void Initialize()
        {
            Utilities.Game.OnUpdate += Update;
            Utilities.Game.OnDraw += Draw;
        }
        public static void Update(GameTime gameTime)
        {

        }
        public static void Draw(SpriteBatch sp, GameTime gt)
        {
            //заготовка на будущее :)

        }
    }
}
