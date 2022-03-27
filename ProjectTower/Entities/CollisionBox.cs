using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities
{
    public class CollisionBox : Entity
    {
        public CollisionBox(float x, float y, float width, float height)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            new Collider2D(this) { Transform = t2D, Tag = Globals.TAG_SOLID, Shape = new BoundingRectangle(width, height) };
        }

        protected override void Cleanup()
        {

        }
    }
}
