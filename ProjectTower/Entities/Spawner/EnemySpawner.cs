using HonasGame;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using ProjectTower.Components;
using ProjectTower.Entities.Enemies;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Spawner
{
    public class EnemySpawner : Entity
    {
        private string _pathName;
        private EnemyPath _path;

        public EnemySpawner(string path)
        {
            _pathName = path;
            _path = null;
        }

        public override void Update(GameTime gameTime)
        {
            if(_path == null)
            {
                foreach(Entity e in Scene.GetEntities())
                {
                    if(e is EnemyPath ep && ep.Name == _pathName)
                    {
                        _path = ep;
                        break;
                    }
                }
            }

            if(Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                var orc = new Orc(_path.Path[0].X, _path.Path[0].Y);

                if(orc.GetComponent<Transform2D>(out var t2D) && orc.GetComponent<Mover2D>(out var m2D))
                {
                    new PathFollower(orc, t2D, m2D, _path.Path, 30.0f);
                }

                Scene.AddEntity(orc);
            }

            base.Update(gameTime);
        }

        protected override void Cleanup()
        {
        }
    }
}
