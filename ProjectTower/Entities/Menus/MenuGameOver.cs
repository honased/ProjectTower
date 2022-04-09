using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.Rendering;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Menus
{
    public class MenuGameOver : Entity
    {
        private SpriteFont _font;

        public MenuGameOver()
        {
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
            if(Globals.GameWon)
            {
                Destroy();
                Scene.AddEntity(new MenuGameWon());
            }
        }

        public override void Update(GameTime gameTime)
        {
            if(Input.IsKeyPressed(Keys.Enter) || Input.IsButtonPressed(Buttons.A))
            {
                Scene.AddEntity(new RoomTransition("map_menu"));
                Enabled = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawFilledRectangle(Vector2.Zero, Camera.CameraSize, Color.Black);

            var str = "Game Over!";
            var bounds = _font.MeasureString(str) / 2.0f;

            spriteBatch.DrawString(_font, str, Camera.CameraSize / 2.0f, Color.White, 0.0f, bounds, 2.0f, SpriteEffects.None, 0.0f);

            str = "Press Enter/[A] To Continue Because You Can't Stop...Playing?";
            bounds = _font.MeasureString(str) / 2.0f;
            spriteBatch.DrawString(_font, str, new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 3.0f * 2.0f), Color.White, 0.0f, bounds, 1.5f, SpriteEffects.None, 0.0f);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
