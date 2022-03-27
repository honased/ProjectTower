using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using HonasGame.Helper;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ProjectTower.Components;
using ProjectTower.Particles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Towers.Projectiles
{
    public class Mortar : Entity
    {
        private Transform2D _transform;
        private Texture2D _texture;
        private float alpha;
        private float _timer;
        private float _rotation;

        public Mortar(float x, float y)
        {
            _transform = new Transform2D(this) { Position = new Vector2(x, y) };

            alpha = 0.0f;
            _timer = 1.0f;

            _rotation = MathHelper.TwoPi * 2.0f;

            _texture = AssetLibrary.GetAsset<Texture2D>("mortarTarget");
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _timer -= t;
            if(_timer <= 0.0)
            {
                Scene.GetParticleSystem<ExplosionParticleSystem>().PlaceExplosion(_transform.Position);
                AssetLibrary.GetAsset<SoundEffect>("Explosion").Play();
                foreach (Entity e in Scene.GetEntities())
                {
                    if(e.GetComponent<Transform2D>(out var tf) && e.GetComponent<Collider2D>(out var c2D) && (c2D.Tag & Globals.TAG_ENEMY) > 0)
                    {
                        if(Vector2.Distance(_transform.Position, tf.Position) <= 45.0f && e.GetComponent<HealthComponent>(out var hp))
                        {
                            hp.Damage(5);
                            AssetLibrary.GetAsset<SoundEffect>("EnemyHit").Play();
                        }
                    }
                }

                Destroy();
            }

            alpha = HonasMathHelper.LerpDelta(alpha, 1.0f, 0.2f, gameTime);

            _rotation = HonasMathHelper.LerpDelta(_rotation, 0.0f, 0.2f, gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _transform.Position, null, Color.FromNonPremultiplied(255, 0, 0, (int)(alpha * 255)), _rotation, new Vector2(12, 12), 1.0f, SpriteEffects.None, 0.0f);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
