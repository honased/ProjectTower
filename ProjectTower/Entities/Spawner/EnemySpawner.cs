using HonasGame.ECS;
using Microsoft.Xna.Framework;
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

            base.Update(gameTime);
        }

        protected override void Cleanup()
        {
        }
    }
}
