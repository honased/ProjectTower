using System;
using System.Collections.Generic;
using System.Text;
using HonasGame.Helper;
using HonasGame.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ProjectTower.Particles
{
    public class ExplosionParticleSystem : ParticleSystem
    {
        public ExplosionParticleSystem(int maxExplosions) : base(maxExplosions * 25) { }

        protected override void InitializeConstants()
        {
            textureFilename = "explosion";

            minNumParticles = 20;
            maxNumParticles = 25;

            blendState = BlendState.Additive;
            DrawOrder = AdditiveBlendDrawOrder;
        }

        protected override void InitializeParticle(ref Particle p, Vector2 where)
        {
            var velocity = RandomHelper.NextDir() * RandomHelper.NextFloat(40, 60);

            var lifetime = RandomHelper.NextFloat(0.35f, 0.75f);

            var acceleration = -velocity / lifetime;

            var rotation = RandomHelper.NextFloat(0, MathHelper.TwoPi);

            var angularVelocity = RandomHelper.NextFloat(-MathHelper.PiOver4, MathHelper.PiOver4);

            p.Initialize(where, velocity, acceleration, lifetime: lifetime, rotation: rotation, angularVelocity: angularVelocity, scale:0.75f);
        }

        protected override void UpdateParticle(ref Particle particle, float dt)
        {
            base.UpdateParticle(ref particle, dt);

            float normalizedLifetime = particle.TimeSinceStart / particle.Lifetime;

            float alpha = 4 * normalizedLifetime * (1 - normalizedLifetime);
            particle.Color = Color.White * alpha;

            particle.Scale = 0.75f + normalizedLifetime;
        }

        public void PlaceExplosion(Vector2 where) => AddParticles(where);
    }
}