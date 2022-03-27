using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Tutorial
{
    public class Tutorial : Entity
    {
        double _timer;
        SpriteFont _font;
        public bool _box;
        int stringCounter;
        int stringMax;
        int offset;
        public Tutorial()
        {
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
            new Transform2D(this);
            GetComponent<Transform2D>(out var t);
            t.Position = new Vector2(10, Camera.CameraSize.X - 10);
            _timer = 0.0f;
            _box = false;
            stringCounter = 1;
            offset = 5;
        }

        public override void Update(GameTime gameTime)
        {
            
            GetComponent<Transform2D>(out var t);
            if (t.Position.Y > Camera.CameraSize.Y  - 100)
            {
                t.Position -= 10 * Vector2.UnitY;
            }
            else
            {
                _box = true;
                _timer += gameTime.ElapsedGameTime.TotalSeconds;
            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            //spriteBatch.DrawString(_font, "this is a test", new Vector2(100, 100), Color.Red);
            GetComponent<Transform2D>(out var t);
            //Vector2 posi = new Vector2(10, Camera.CameraSize.Y - 10) + (Vector2.UnitY * ((MathF.Cos(ti) * 7.0f) / MathF.Sin(ti) * 7.0f));

            //t.Position = new Vector2(0, 350);
            spriteBatch.DrawFilledRectangle(new Rectangle((int)t.Position.X, (int)t.Position.Y, (int)Camera.CameraSize.X - 20, (int)Camera.CameraSize.Y / 4), Color.Black);
            string test = "this is still a test message, but wouldn't it be \ninteresting to see it go to a second line";
            
                stringMax = test.Length;
            
            
            if (_box)
            {
               
                if(_timer > .1f && stringCounter < stringMax)
                {
                   // spriteBatch.DrawString(_font, test.Substring(0,stringCounter), new Vector2((int)t.Position.X+5, (int)t.Position.Y+5), Color.White);
                    stringCounter++;
                    _timer -= .1f;
                }
            
            }
            spriteBatch.DrawString(_font, test.Substring(0, stringCounter), new Vector2((int)t.Position.X + 5, (int)t.Position.Y + offset), Color.White, 0.0f, Vector2.Zero, 2.0f, SpriteEffects.None, 0.0f);

        }
        protected override void Cleanup()
        {
            
        }
    }
}
