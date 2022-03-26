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

        public PathFollower(Entity parent, Transform2D transf, Mover2D mover, List<Vector2> path) : base(parent)
        {
            _path = path;
            _transform = transf;
            _mover = mover;
            _velocity = new Velocity2D();
            _pathTarget = 0;
        }

        private void SetNextTarget()
        {
            _pathTarget += 1;
            _transform.Position = _path[_pathTarget - 1];

            if(_pathTarget < _path.Count)
            {

            }
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
