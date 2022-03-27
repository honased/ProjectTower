using System;
using System.Collections.Generic;
using System.Text;
using HonasGame.Helper;
using HonasGame.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTower.Particles
{
    public class FireworksParticleSystem : ParticleSystem
    {
        Color[] colors = new Color[]
        {
            Color.Fuchsia,
            Color.Red,
            Color.Crimson,
            Color.CadetBlue,
            Color.Aqua,
            Color.HotPink,
            Color.LimeGreen
        };

        Color color;

        public FireworksParticleSystem(int maxFireworks) : base(maxFireworks * 45) { }

        protected override void InitializeConstants()
        {
            textureFilename = "circle";

            minNumParticles = 40;
            maxNumParticles = 45;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDir() * RandomHelper.NextFloat(30, 105);

            var lifetime = RandomHelper.NextFloat(0.5f, 1.0f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            p.Initialize(where, velocity, acceleration, color, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale: .1f);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            particle.Scale = .1f + .2f * normalizedLifetime;
        }

        public void PlaceFirework(Vector2 where)
        {
            color = colors[RandomHelper.NextInt(colors.Length)];
            AddParticles(where);
        }
    }
}