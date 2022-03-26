using HonasGame.ECS;
using HonasGame.ECS.Components;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using HonasGame.Assets;
using HonasGame.ECS.Components.Physics;
using ProjectTower.Components;

namespace ProjectTower.Entities.Players
{
    public class Player : Entity
    {
        public Player(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var sr = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprPlayer"), Animation = "default" };
            sr.CenterOrigin();
            t2D.Position += sr.Origin;
            var c2D = new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(16, 24) { Offset = -sr.Origin } };
            var m2D = new Mover2D(this);
            new PlayerController(this, t2D, c2D, sr, m2D);
        }

        protected override void Cleanup()
        {

        }
    }
}
