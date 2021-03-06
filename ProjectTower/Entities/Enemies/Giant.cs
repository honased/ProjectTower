using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using ProjectTower.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Enemies
{
    public class Giant : Entity
    {
        public Giant()
        {
            var t2D = new Transform2D(this) { Position = new Vector2(0, 10000) };
            var r2D = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprGiant"), Animation = "default" };
            r2D.CenterOrigin();

            new Collider2D(this) { Shape = new BoundingRectangle(27, 30) { Offset = -r2D.Origin }, Transform = t2D, Tag = Globals.TAG_ENEMY };
            new HealthComponent(this, 100, Dead);
            new SpeedComponent(this) { Speed = 15.0f };
            new EnemyThrough(this, t2D);
        }

        private void Dead()
        {
            Globals.Money += 70;
            AssetLibrary.GetAsset<SoundEffect>("Death").Play();
            Destroy();
        }

        protected override void Cleanup()
        {
        }
    }
}
