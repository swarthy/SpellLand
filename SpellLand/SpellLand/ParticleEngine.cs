﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace SpellLand
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles;
        private List<Texture2D> textures;
        internal int TTL = 50, Total = 15;
        public ParticleEngine(List<Texture2D> textures, Vector2 location)
        {
            EmitterLocation = location;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();

            Utilities.Game.OnUpdate += Update;
            Utilities.Game.OnDraw += Draw;

            MouseController.OnMove += (m) => {
                EmitterLocation = new Vector2(m.X, m.Y);
                for (int i = 0; i < Total; i++)                
                    particles.Add(GenerateNewParticle());                
            };

            Utilities.Console.AddCommand("particles_ttl", (args) =>
            {
                if (args.Length > 0)
                {
                    Utilities.Game.particleEngine.TTL = int.Parse(args[0]);
                    return "";
                }
                else
                    return Utilities.Game.particleEngine.TTL.ToString();
            }, "Particles time to live");

            Utilities.Console.AddCommand("particles_total_count", (args) =>
            {
                if (args.Length > 0)
                {
                    Utilities.Game.particleEngine.Total = int.Parse(args[0]);
                    return "";
                }
                else
                    return Utilities.Game.particleEngine.Total.ToString();
            }, "Particles count, creating every update cycle"); 
        }
        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                    (float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
            float size = (float)random.NextDouble();
            int ttl = TTL + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }
        public void Update(GameTime gameTime)
        {
            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if (particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }            
        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Begin();
            for (int index = 0; index < particles.Count; index++)
            {
                particles[index].Draw(spriteBatch);
            }
            spriteBatch.End();
        }

    }
    public class Particle
    {
        public Texture2D Texture { get; set; } 
        public Vector2 Position { get; set; }
        public Vector2 Velocity { get; set; }
        public float Angle { get; set; }
        public float AngularVelocity { get; set; }
        public Color Color { get; set; }
        public float Size { get; set; }
        public int TTL { get; set; }
        public Particle(Texture2D texture, Vector2 position, Vector2 velocity,
            float angle, float angularVelocity, Color color, float size, int ttl)
        {
            Texture = texture;
            Position = position;
            Velocity = velocity;
            Angle = angle;
            AngularVelocity = angularVelocity;
            Color = color;
            Size = size;
            TTL = ttl;
        }
        public void Update()
        {
            TTL--;
            Position += Velocity;
            Angle += AngularVelocity;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(Texture.Width / 2, Texture.Height / 2);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color,
                Angle, origin, Size, SpriteEffects.None, 0f);
        }
    }

}
