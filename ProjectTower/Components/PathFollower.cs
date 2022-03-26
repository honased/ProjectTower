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
        private Mover2D _mover;
        private Velocity2D _velocity;
        private int _pathTarget;
        private Vector2 _dir;
        private float _speed;
        private WalkAnimation _animation;

        public PathFollower(Entity parent, Transform2D transf, Mover2D mover, List<Vector2> path, float speed) : base(parent)
        {
            _path = path;
            _transform = transf;
            _mover = mover;
            _speed = speed;
            _velocity = new Velocity2D();
            _pathTarget = 0;
            _dir = Vector2.Zero;
            SetNextTarget();
            if (parent.GetComponent<SpriteRenderer>(out var renderer))
            {
                _animation = new WalkAnimation(parent, renderer);
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

            _velocity.Set(_dir * _speed);

            _animation?.SetVelocity(_dir * _speed);

            Vector2 actVel = _velocity.CalculateVelocity(gameTime);

            _mover.MoveX(actVel.X, 0);
            _mover.MoveY(actVel.Y, 0);
        }
    }
}
