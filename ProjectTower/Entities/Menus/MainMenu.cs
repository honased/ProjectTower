using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Menus
{
    public class MainMenu : Entity
    {
        private SpriteFont _font;

        public MainMenu()
        {
            int a;
            a = 0;
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
        }
        protected override void Cleanup()
        {
            
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            float t = (float)gameTime.TotalGameTime.TotalSeconds;
            var origin = _font.MeasureString("Play") / 2.0f;
            spriteBatch.DrawString(_font, "Play", new Vector2(Camera.CameraSize.X / 3.0f, Camera.CameraSize.Y - 18.0f),  Color.Black, 0.0f, origin, 3.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Quit", new Vector2(Camera.CameraSize.X / 3.0f * 2, Camera.CameraSize.Y - 18.0f),  Color.Yellow, 0.0f, origin, 3.0f, SpriteEffects.None, 0.0f);

           
            origin = _font.MeasureString("Tower Rush") / 2.0f;
            Vector2 pos = new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 3.0f) + (Vector2.UnitY * (MathF.Sin(t) * 7.0f));
            spriteBatch.DrawString(_font, "Tower Rush", pos + Vector2.UnitX, Color.Black, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos - Vector2.UnitX, Color.Black, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos + Vector2.UnitY, Color.Black, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos - Vector2.UnitY, Color.Black, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos, Color.White, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            //spriteBatch.DrawString(_font, "Tower Rush", new Vector2(400, 200), Color.Red);
            
            base.Draw(gameTime, spriteBatch);
        }
    }
}
