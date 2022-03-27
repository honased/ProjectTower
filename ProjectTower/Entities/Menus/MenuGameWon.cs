using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.Helper;
using HonasGame.Rendering;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectTower.Particles;
using Microsoft.Xna.Framework.Audio;
using HonasGame.ECS.Components;

namespace ProjectTower.Entities.Menus
{
    public class MenuGameWon : Entity
    {
        private SpriteFont _font;

        public MenuGameWon()
        {
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
            var routine = new Coroutine(this, Fireworks());
            routine.Start();

            routine = new Coroutine(this, Fireworks());
            routine.Start();
        }

        private IEnumerator<double> Fireworks()
        {
            while(true)
            {
                yield return RandomHelper.NextFloat(0.2f, 3.0f);
                Scene.GetParticleSystem<FireworksParticleSystem>().PlaceFirework(RandomHelper.RandomPosition(new Rectangle(0, 0, (int)Camera.CameraSize.X, (int)Camera.CameraSize.Y)));
                AssetLibrary.GetAsset<SoundEffect>("Explosion").Play();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Enter))
            {
                Scene.AddEntity(new RoomTransition("map_menu"));
                Enabled = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawFilledRectangle(Vector2.Zero, Camera.CameraSize, Color.Black);

            var str = "You won! Congratulations!";
            var bounds = _font.MeasureString(str) / 2.0f;

            spriteBatch.DrawString(_font, str, Camera.CameraSize / 2.0f, Color.White, 0.0f, bounds, 2.0f, SpriteEffects.None, 0.0f);

            str = "Press Enter To Continue Because You Can't Stop...Playing?";
            bounds = _font.MeasureString(str) / 2.0f;
            spriteBatch.DrawString(_font, str, new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 3.0f * 2.0f), Color.White, 0.0f, bounds, 1.5f, SpriteEffects.None, 0.0f);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
