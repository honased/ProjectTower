using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Helper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class WalkAnimation : Component
    {
        private const double WALK_TIME = 0.2;

        private SpriteRenderer _renderer;
        private Vector2 _velocity;
        private double _walkTimer;
        private float _rotationGoal;
        private bool _walkFlip;
        private int _xScaleTarget;
        private float _fakeXScale;

        public WalkAnimation(Entity parent, SpriteRenderer renderer) : base(parent)
        {
            _renderer = renderer;
            _velocity = Vector2.Zero;
            _walkTimer = 0.0;
            _rotationGoal = 0.0f;
            _walkFlip = false;
            _xScaleTarget = 1;
            _fakeXScale = 1;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(MathF.Abs(_velocity.X) > 1.0f)
            {
                _xScaleTarget = MathF.Sign(_velocity.X);
            }

            if(_velocity.LengthSquared() > 1.0f)
            {
                if (_walkTimer <= 0.0)
                {
                    _walkTimer = WALK_TIME;
                    _rotationGoal = (_walkFlip ? 1 : -1) * MathHelper.PiOver4 / 2.0f;
                    _walkFlip = !_walkFlip;
                }
                else _walkTimer -= t;
            }
            else
            {
                _walkTimer = 0.0;
                _rotationGoal = 0.0f;
            }

            _renderer.Rotation = HonasMathHelper.LerpDelta(_renderer.Rotation, _rotationGoal, 0.2f, gameTime);

            _fakeXScale = HonasMathHelper.LerpDelta(_fakeXScale, _xScaleTarget, 0.2f, gameTime);

            _renderer.Scale = new Vector2(MathF.Abs(_fakeXScale), _renderer.Scale.Y);
            _renderer.SpriteEffects = (_xScaleTarget == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
    }
}
