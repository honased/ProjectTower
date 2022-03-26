using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class TowerHealth : Component
    {
        private const double MAX_TIMER = 0.4;

        private Transform2D _transform;
        private HealthComponent _health;
        private double _timer;

        public TowerHealth(Entity parent, Transform2D transform, HealthComponent health) : base(parent)
        {
            _transform = transform;
            _health = health;
            _timer = MAX_TIMER;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Rectangle maxRect = new Rectangle((int)_transform.Position.X - 6, (int)_transform.Position.Y - 18, 12, 4);
            Vector2 hpSize = new Vector2(((float)_health.Health / _health.MaxHealth) * 12, 4);
            spriteBatch.DrawFilledRectangle(maxRect, Color.DarkGray);
            spriteBatch.DrawFilledRectangle(new Vector2(maxRect.X + 1, maxRect.Y + 1), hpSize - Vector2.One, Color.Aqua);
            spriteBatch.DrawRectangle(maxRect, Color.Black, 1.0f);

            base.Draw(gameTime, spriteBatch);
        }

        public bool IsActive => _health.Health > 0;

        public override void Update(GameTime gameTime)
        {
            if (_timer <= 0)
            {
                _timer = MAX_TIMER;
                _health.Damage(1);
            }
            else _timer -= (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }
    }
}
