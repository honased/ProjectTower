using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using Microsoft.Xna.Framework;
using ProjectTower.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Enemies
{
    public class Orc : Entity
    {
        private WalkAnimation _animation;

        public Orc(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var r2D = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprOrc"), Animation = "default" };
            r2D.CenterOrigin();

            t2D.Position += r2D.Origin;

            _animation = new WalkAnimation(this, r2D);
        }

        public override void Update(GameTime gameTime)
        {
            float t = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            float vx = Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right) ? 1 : -1;
            vx *= 60;

            _animation.SetVelocity(new Vector2(vx, 0));
            if(GetComponent<Transform2D>(out var tt))
            {
                tt.Position += Vector2.UnitX * vx * t;
            }

            base.Update(gameTime);
        }

        protected override void Cleanup()
        {
        }
    }
}
