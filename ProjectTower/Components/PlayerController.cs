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
using ProjectTower.Entities.Towers;

namespace ProjectTower.Components
{
    public class PlayerController : Component
    {
        private Transform2D _transform;
        private SpriteRenderer _renderer;
        private Mover2D _mover;
        private Velocity2D _velocity;
        private WalkAnimation _animation;
        private Collider2D _collider;

        public PlayerController(Entity parent, Transform2D transform, Collider2D collider, SpriteRenderer renderer, Mover2D mover) : base(parent)
        {
            _transform = transform;
            _renderer = renderer;
            _mover = mover;
            _collider = collider;
            _velocity = new Velocity2D();
            _animation = new WalkAnimation(parent, _renderer);
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

            _animation.SetVelocity(new Vector2(vx, vy) * 60.0f);

            _velocity.Set(new Vector2(vx, vy) * 60.0f);
            Vector2 _moveVector = _velocity.CalculateVelocity(gameTime);

            _mover.MoveX(_moveVector.X, 0);
            _mover.MoveY(_moveVector.Y, 0);

            Entity e = _collider.CollidesWithAnything(out uint tag);
            if(e != null)
            {
                if ((tag & Globals.TAG_TOWER_PLOT) > 0 && e is TowerPlot tp)
                {
                    if (Input.IsKeyPressed(Keys.E) && tp.GetComponent<Transform2D>(out var tpTransf))
                    {
                        Scene.AddEntity(new ArcherTower(tpTransf.Position.X, tpTransf.Position.Y));
                        tp.Destroy();
                    }
                }
                else if((tag & Globals.TAG_TOWER) > 0 && e.GetComponent<HealthComponent>(out var hp) && Input.IsKeyPressed(Keys.E))
                {
                    hp.Health += 10;
                }
            }

            
        }
    }
}
