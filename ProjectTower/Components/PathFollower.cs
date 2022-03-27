using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class PathFollower : Component
    {
        private List<Vector2> _path;
        private Transform2D _transform;
        private Vector2 _velocity;

        public Vector2 Velocity => new Vector2(_velocity.X, _velocity.Y);

        private int _pathTarget;
        private Vector2 _dir;
        private float _speed;
        private WalkAnimation _animation;

        public PathFollower(Entity parent, Transform2D transf, List<Vector2> path, float speed) : base(parent)
        {
            _path = path;
            _transform = transf;
            _speed = speed;
            _velocity = Vector2.Zero;
            _pathTarget = 0;
            _dir = Vector2.Zero;
            SetNextTarget();
            if (parent.GetComponent<SpriteRenderer>(out var renderer) && parent.GetComponent<SpeedComponent>(out var spd))
            {
                _animation = new WalkAnimation(parent, renderer, (30/spd.Speed)*.2);
            }
            else _animation = null;
        }

        private void SetNextTarget()
        {
            _pathTarget += 1;
            _transform.Position = new Vector2(MathF.Round(_path[_pathTarget - 1].X), MathF.Round(_path[_pathTarget - 1].Y));

            if(_pathTarget < _path.Count)
            {
                _dir = _path[_pathTarget] - _transform.Position;
                _dir.Normalize();
            }
        }

        private bool AtTarget()
        {
            if (_pathTarget >= _path.Count) return false;

            Vector2 posDifVec = _path[_pathTarget] - _transform.Position;
            posDifVec.Normalize();

            if(Vector2.Dot(_dir, posDifVec) < -0.9f)
            {
                return true;
            }

            return false;
        }

        public override void Update(GameTime gameTime)
        {
            if (AtTarget()) SetNextTarget();

            _velocity = _dir * _speed;

            _animation?.SetVelocity(_dir * _speed);

            Vector2 actVel = _velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            _transform.Position += actVel;
        }
    }
}
