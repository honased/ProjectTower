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
            var routine = new Coroutine(this, EnemyRoutine());
            routine.Start();
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
                CreateEnemy<Orc>();
            }

            base.Update(gameTime);
        }

        private IEnumerator<double> EnemyRoutine()
        {
            int i;
            yield return 1.0;
            for(i = 0; i < 3; i++)
            {
                CreateEnemy<Orc>();
                yield return 1.0;
            }

            CreateEnemy<Rat>();
            CreateEnemy<Giant>();
        }

        private void CreateEnemy<T>() where T : Entity, new()
        {
            Entity e = new T();
            if (e.GetComponent<Transform2D>(out var t2D) && e.GetComponent<SpeedComponent>(out var spd))
            {
                new PathFollower(e, t2D, _path.Path, spd.Speed);
            }

            Scene.AddEntity(e);
        }

        protected override void Cleanup()
        {
        }
    }
}
