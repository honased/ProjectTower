﻿using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ProjectTower.Components;
using ProjectTower.Entities.Towers.Projectiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Towers
{
    public class MortarTower : Entity
    {
        private Transform2D _transform;
        private const float MAX_DIST = 100.0f;
        private ScaleAnimator _animator;

        public MortarTower(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var r2D = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprMortarTower"), Animation = "default" };
            _animator = new ScaleAnimator(this, Vector2.One, r2D);

            new TowerHealth(this, t2D, new HealthComponent(this, 38));

            _transform = t2D;

            var routine = new Coroutine(this, ShootRoutine());
            routine.Start();

            r2D.CenterOrigin();

            new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(12, 24) { Offset = -r2D.Origin }, Tag = Globals.TAG_TOWER };
        }

        private IEnumerator<double> ShootRoutine()
        {
            while (true)
            {
                yield return 3.0;
                if (!GetComponent<TowerHealth>(out var th) || !th.IsActive) continue;
                foreach (Entity e in Scene.GetEntities())
                {
                    if (e.GetComponent<Transform2D>(out var transform) && e.GetComponent<Collider2D>(out var collider) && (collider.Tag & Globals.TAG_ENEMY) > 0)
                    {
                        if (Vector2.Distance(transform.Position, _transform.Position) <= MAX_DIST && e.GetComponent<PathFollower>(out var pf))
                        {
                            _animator.Scale = 1.5f * Vector2.One;
                            Vector2 projectileVel = pf.Velocity / 2.0f;
                            Scene.AddEntity(new Mortar(transform.Position.X + projectileVel.X, transform.Position.Y + projectileVel.Y));
                            AssetLibrary.GetAsset<SoundEffect>("Explosion").Play();
                            break;
                        }
                    }
                }
            }
        }

        protected override void Cleanup()
        {

        }
    }
}
