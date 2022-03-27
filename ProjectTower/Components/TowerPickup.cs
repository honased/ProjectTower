using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class TowerPickup : Component
    {
        private Texture2D _texture;
        private Transform2D _transform;
        public string TowerType { get; private set; }
        public TowerPickup(Entity parent, string towerSprite, string towerType) : base(parent)
        {
            _texture = AssetLibrary.GetAsset<Sprite>(towerSprite).Texture;
            TowerType = towerType;
            parent.GetComponent<Transform2D>(out _transform);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _transform.Position - new Vector2(0, 8), null, Color.White, 0.0f, new Vector2(_texture.Width / 2, _texture.Height), 0.75f, SpriteEffects.None, 0.0f);
        }
    }
}
