using HonasGame.ECS;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Spawner
{
    public class EnemyPath : Entity
    {
        public List<Vector2> Path { get; private set; }
        public string Name { get; private set; }

        public EnemyPath(string name)
        {
            Name = name;
            Path = new List<Vector2>();
        }

        protected override void Cleanup()
        {

        }
    }
}
