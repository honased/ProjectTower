using HonasGame;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using HonasGame.Helper;

namespace ProjectTower.Components
{
    public class PlayerController : Component
    {
        private Transform2D _transform;
        private SpriteRenderer _renderer;
        private Mover2D _mover;
        private Velocity2D _velocity;
        private Coroutine _walkAnimroutine;
        private float _rotationTarget;
        private int _xScaleTarget;
        private float _fakeXScale;

        public PlayerController(Entity parent, Transform2D transform, SpriteRenderer renderer, Mover2D mover) : base(parent)
        {
            _transform = transform;
            _renderer = renderer;
            _mover = mover;
            _velocity = new Velocity2D();
            _walkAnimroutine = new Coroutine(parent, WalkAnimation());
            _rotationTarget = 0.0f;
            _walkAnimroutine.Start();
            _xScaleTarget = 1;
            _fakeXScale = 1;
        }

        private IEnumerator<double> WalkAnimation()
        {
            while(true)
            {
                _rotationTarget = MathHelper.PiOver4 / 2.0f;
                yield return 0.2;
                _rotationTarget = -MathHelper.PiOver4 / 2.0f;
                yield return 0.2;
            }
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var right = Input.IsKeyDown(Keys.Right) || Input.IsKeyDown(Keys.D);
            var left = Input.IsKeyDown(Keys.Left) || Input.IsKeyDown(Keys.A);
            var up = Input.IsKeyDown(Keys.Up) || Input.IsKeyDown(Keys.W);
            var down = Input.IsKeyDown(Keys.Down) || Input.IsKeyDown(Keys.S);

            int vx = ((right ? 1 : 0) - (left ? 1 : 0));
            int vy = ((down ? 1 : 0) - (up ? 1 : 0));

            _velocity.Set(new Vector2(vx, vy) * 60.0f);
            Vector2 _moveVector = _velocity.CalculateVelocity(gameTime);

            _mover.MoveX(_moveVector.X, 0);
            _mover.MoveY(_moveVector.Y, 0);

            if(vx != 0 || vy != 0)
            {
                _walkAnimroutine.Enabled = true;
                if (vx != 0) _xScaleTarget = vx;
            }
            else
            {
                _walkAnimroutine.Enabled = false;
                _rotationTarget = 0.0f;
            }

            _renderer.Rotation = HonasMathHelper.LerpDelta(_renderer.Rotation, _rotationTarget, 0.2f, gameTime);
            _fakeXScale = HonasMathHelper.LerpDelta(_fakeXScale, _xScaleTarget, 0.2f, gameTime);

            _renderer.Scale = new Vector2(MathF.Abs(_fakeXScale), _renderer.Scale.Y);
            _renderer.SpriteEffects = (_xScaleTarget == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        }
    }
}
