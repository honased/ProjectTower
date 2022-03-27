using HonasGame.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    class SpeedComponent : Component
    {
        public float Speed { get; set; }
        public SpeedComponent(Entity parent) : base(parent)
        {

        }
    }
}
