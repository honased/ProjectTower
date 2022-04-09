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

            base.Update(gameTime);
        }

        private IEnumerator<double> EnemyRoutine()
        {
            int i;
            bool towerPlacedYet = false;
            while(!towerPlacedYet)
            {
                foreach(Entity e in Scene.GetEntities())
                {
                    if(e.GetComponent<Collider2D>(out var collider) && (collider.Tag & Globals.TAG_TOWER) > 0)
                    {
                        towerPlacedYet = true;
                        break;
                    }
                }
                yield return 1.0;
            }

            for(i = 0; i < 6; i++)
            {
                CreateEnemy<Orc>();
                yield return 4.2;
            }

            for (i = 0; i < 6; i++)
            {
                CreateEnemy<Orc>();
                yield return 1.2;
                CreateEnemy<Orc>();
                yield return 3.5;
            }

            for (i = 0; i < 8; i++)
            {
                CreateEnemy<Orc>();
                yield return 0.8;
            }

            yield return 4.0;

            for (i = 0; i < 2; i++)
            {
                CreateEnemy<Rat>();
                yield return 1.5;
            }

            yield return 2.0;

            for (i = 0; i < 5; i++)
            {
                CreateEnemy<Rat>();
                yield return 0.3;
            }

            yield return 2.0;

            for (i = 0; i < 15; i++)
            {
                CreateEnemy<Rat>();
                yield return 0.2;
            }

            yield return 3.0;
            for (i = 0; i < 10; i++)
            {
                CreateEnemy<Orc>();
                yield return 0.5;
                CreateEnemy<Rat>();
                yield return 0.8;
            }

            yield return 4.0;
            CreateEnemy<Giant>();

            bool giantExists = true;
            do
            {
                yield return 1.0;
                giantExists = Scene.GetEntity<Giant>(out var giant);
            } while (giantExists);

            yield return 1.0;

            for (i = 0; i < 13; i++)
            {
                CreateEnemy<Orc>();
                yield return 0.3;
                CreateEnemy<Rat>();
                yield return 0.4;
            }

            yield return 2.0;

            CreateEnemy<Giant>();
            for (i = 0; i < 10; i++)
            {
                CreateEnemy<Orc>();
                yield return 0.4;
            }
            yield return 1.0;
            CreateEnemy<Giant>();

            yield return 2.0;
            for (i = 0; i < 10; i++)
            {
                CreateEnemy<Rat>();
                yield return 0.4;
            }

            Globals.LastEnemyToGo = true;
        }

        private void CreateEnemy<T>() where T : Entity, new()
        {
            Entity e = new T();
            if (e.GetComponent<Transform2D>(out var t2D) && e.GetComponent<SpeedComponent>(out var spd))
            {
                new PathFollower(e, t2D, _path.Path, spd.Speed);
            }

            Scene.AddEntity(e, "Enemies");
        }

        protected override void Cleanup()
        {
        }
    }
}
