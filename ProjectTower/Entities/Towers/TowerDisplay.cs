using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Towers
{
    public class TowerDisplay : Entity
    {
        public TowerDisplay(float x, float y, string sprite)
        {
            Transform2D transf = new Transform2D(this) { Position = new Vector2(x, y) };
            SpriteRenderer renderer = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>(sprite), Animation = "default" };
            renderer.CenterOrigin();
            new Collider2D(this) { Transform = transf, Tag = Globals.TAG_SOLID, Shape = new BoundingRectangle(12, 24) { Offset = -renderer.Origin } };
            transf.Position += renderer.Origin;
        }

        protected override void Cleanup()
        {

        }
    }
}
