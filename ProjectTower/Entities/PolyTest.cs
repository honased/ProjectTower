using HonasGame.ECS;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities
{
    public class PolyTest : Entity
    {
        public List<Vector2> PolyList { get; private set; }

        private Vector2 _pos;
        private Vector2 _vel;

        private int _polyTarget;

        public PolyTest(Vector2 pos)
        {
            PolyList = new List<Vector2>();
            _pos = pos;
            _vel = Vector2.Zero;
            _polyTarget = 0;
        }

        public override void Update(GameTime gameTime)
        {
            if (_polyTarget == 0) SetTarget();
            if(_polyTarget < PolyList.Count)
            {
                Vector2 velNorm = _vel, polyNorm = PolyList[_polyTarget] - _pos;
                velNorm.Normalize();
                polyNorm.Normalize();
                if(Vector2.Dot(velNorm, polyNorm) <= -.95f )
                {
                    SetTarget();
                }
            }

            _pos += _vel * (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);
        }

        private void SetTarget()
        {
            _polyTarget++;
            if(_polyTarget < PolyList.Count)
            {
                _vel = (PolyList[_polyTarget] - _pos);
                _vel.Normalize();
                _vel *= 60;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle(_pos, 16, Color.Red);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
