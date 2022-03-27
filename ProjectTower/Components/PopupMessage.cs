using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.Helper;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class PopupMessage : Component
    {
        private SpriteFont _font;
        private string _message;

        public string Message
        {
            get => _message;
            set
            {
                _message = " " + value + " ";
                _bounds = _font.MeasureString(_message);
                _rect = new Rectangle(0, 0, (int)_bounds.X, (int)_bounds.Y);
                _pos = new Vector2(Camera.CameraSize.X - _rect.Width, Camera.CameraSize.Y);
            }
        }

        Vector2 _bounds;
        Rectangle _rect;
        Vector2 _pos;

        public bool Active { get; set; }

        public PopupMessage(Entity parent, string message) : base(parent)
        {
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
            Active = false;
            Message = message;
        }

        public override void Update(GameTime gameTime)
        {
            if(Active)
            {
                _pos = HonasMathHelper.LerpDelta(_pos, new Vector2(_pos.X, Camera.CameraSize.Y - _rect.Height), 0.2f, gameTime);
                Visible = true;
            }
            else
            {
                _pos = HonasMathHelper.LerpDelta(_pos, new Vector2(_pos.X, Camera.CameraSize.Y + 10), 0.2f, gameTime);
                if (_pos.Y > Camera.CameraSize.Y) Visible = false;
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawFilledRectangle(new Rectangle(_rect.X + (int)_pos.X, _rect.Y + (int)_pos.Y, _rect.Width, _rect.Height), Color.Black);
            spriteBatch.DrawString(_font, _message, _pos, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0.0f);
        }
    }
}
