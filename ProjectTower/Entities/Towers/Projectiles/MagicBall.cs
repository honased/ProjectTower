using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ProjectTower.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Towers.Projectiles
{
    public class MagicBall : Entity
    {
        private Transform2D _transform;
        private Velocity2D _velocity;
        private Collider2D _collider;
        private Texture2D _texture;

        public MagicBall(float x, float y, Vector2 vel)
        {
            _transform = new Transform2D(this) { Position = new Vector2(x, y) };
            _collider = new Collider2D(this) { Shape = new BoundingCircle(4), Transform = _transform };
            _velocity = new Velocity2D();
            _velocity.Set(vel);

            _texture = AssetLibrary.GetAsset<Texture2D>("magicBall");
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _transform.Position += _velocity.CalculateVelocity(gameTime);

            if(_collider.CollidesWith(Globals.TAG_ENEMY, out var e) && e.GetComponent<HealthComponent>(out var hp))
            {
                hp.Health -= 1;
                Destroy();
            }

            if(_transform.Position.X <= -16 || _transform.Position.Y <= -16 || _transform.Position.X >= Camera.CameraSize.X + 16 || _transform.Position.Y >= Camera.CameraSize.Y + 16)
            {
                Destroy();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _transform.Position, null, Color.White, 0.0f, new Vector2(4, 4), 1.0f, SpriteEffects.None, 0.0f);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
