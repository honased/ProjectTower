using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Helper;
using HonasGame.Rendering;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities
{
    public class RoomTransition : Entity
    {
        TiledMap _map;
        private float alpha;
        bool state;
        public RoomTransition(string roomTarget)
        {
            Persistent = true;
            Depth = 999999;
            alpha = 0.0f;
            state = false;

            _map = AssetLibrary.GetAsset<TiledMap>(roomTarget);
        }

        public override void Update(GameTime gameTime)
        {
            if(!state)
            {
                alpha = HonasMathHelper.LerpDelta(alpha, 1.05f, 0.1f, gameTime);

                if(alpha >= 0.99)
                {
                    _map.Goto();
                    state = !state;
                }
            }
            else
            {
                alpha = HonasMathHelper.LerpDelta(alpha, 0.00f, 0.1f, gameTime);

                if (alpha <= 0.02) Destroy();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawFilledRectangle(new Rectangle(0, 0, (int)Camera.CameraSize.X, (int)Camera.CameraSize.Y), Color.FromNonPremultiplied(0, 0, 0, (int)(alpha * 255)));

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
