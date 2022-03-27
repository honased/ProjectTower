using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class EnemyThrough : Component
    {
        private Transform2D _transform;

        public EnemyThrough(Entity parent, Transform2D transform) : base(parent)
        {
            _transform = transform;    
        }

        public override void Update(GameTime gameTime)
        {
            if(_transform.Position.Y < - 32.0f)
            {
                Parent.Destroy();
                AssetLibrary.GetAsset<SoundEffect>("EnemyThorugh").Play();
                Globals.Health -= 1;
            }
        }
    }
}
