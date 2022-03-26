using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Menus
{
    public class MainMenu : Entity
    {
        private SpriteFont _font;
        private bool _MenuSelection = true;
        public bool Quit { get; private set; } = false;

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
            if(Input.IsKeyPressed(Keys.D) || Input.IsKeyPressed(Keys.Right) || Input.IsButtonPressed(Buttons.DPadRight)||Input.CheckAnalogPressed(true, true, 1)) _MenuSelection = false;
            if( Input.IsKeyPressed(Keys.A) || Input.IsKeyPressed(Keys.LeftShift) || Input.IsButtonPressed(Buttons.DPadLeft) || Input.CheckAnalogPressed(true, true, -1)) _MenuSelection = true;
            var choice = Input.IsKeyPressed(Keys.Space) || Input.IsKeyPressed(Keys.Enter) || Input.IsButtonPressed(Buttons.A);

            if(choice)
            {
                if(_MenuSelection)
                {
                    //scene transition here to game

                }
                else
                {
                    //quit the game

                    Quit = true;
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetLibrary.GetAsset<Texture2D>("backgroundMenu"), new Vector2(0, 0), Color.White);
            float t = (float)gameTime.TotalGameTime.TotalSeconds;
            var origin = _font.MeasureString("Play") / 2.0f;
            spriteBatch.DrawString(_font, "Play", new Vector2(Camera.CameraSize.X / 3.0f, Camera.CameraSize.Y - 18.0f),_MenuSelection ? Color.Blue : Color.Black, 0.0f, origin, 3.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Quit", new Vector2(Camera.CameraSize.X / 3.0f * 2, Camera.CameraSize.Y - 18.0f), _MenuSelection ? Color.Black : Color.Blue, 0.0f, origin, 3.0f, SpriteEffects.None, 0.0f);

           
            origin = _font.MeasureString("Tower Rush") / 2.0f;
            //Vector2 pos = new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 1.35f) + (Vector2.UnitY * ((MathF.Cos(t) * 7.0f)/ MathF.Sin(t) * 7.0f));
            Vector2 pos = new Vector2(Camera.CameraSize.X / 2.0f, Camera.CameraSize.Y / 1.35f) + (Vector2.UnitY * ((MathF.Cos(t) * 7.0f)));

            spriteBatch.DrawString(_font, "Tower Rush", pos + Vector2.UnitX, Color.White, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos - Vector2.UnitX, Color.Gray, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos + Vector2.UnitY, Color.Gray, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos - Vector2.UnitY, Color.White, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(_font, "Tower Rush", pos, Color.Black, 0.0f, origin, 4.0f, SpriteEffects.None, 0.0f);
            //spriteBatch.DrawString(_font, "Tower Rush", new Vector2(400, 200), Color.Red);
            
            base.Draw(gameTime, spriteBatch);
        }
    }
}
