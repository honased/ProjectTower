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
    public class TowerPlot : Entity
    {
        public TowerPlot(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var r2D = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprTowerPlot"), Animation = "default" };
            r2D.CenterOrigin();
            t2D.Position += r2D.Origin;
            t2D.Position -= Vector2.UnitY * 8;
            var collider = new Collider2D(this) { Shape = new BoundingRectangle(12, 12) { Offset = -r2D.Origin }, Transform = t2D, Tag = Globals.TAG_TOWER_PLOT };
        }

        protected override void Cleanup()
        {

        }
    }
}
