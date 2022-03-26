using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using ProjectTower.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Enemies
{
    public class Orc : Entity
    {
        public Orc(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var r2D = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprOrc"), Animation = "default" };
            r2D.CenterOrigin();

            new Collider2D(this) { Shape = new BoundingRectangle(16, 22) { Offset = -r2D.Origin }, Tag = Globals.TAG_ENEMY };
            new Mover2D(this);
            new HealthComponent(this, 3, Dead);
        }

        private void Dead()
        {
            Destroy();
        }

        protected override void Cleanup()
        {
        }
    }
}
