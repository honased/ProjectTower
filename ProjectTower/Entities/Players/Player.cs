using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using HonasGame.Assets;
using HonasGame.ECS.Components.Physics;
using ProjectTower.Components;
using Microsoft.Xna.Framework.Graphics;
using HonasGame;

namespace ProjectTower.Entities.Players
{
    public class Player : Entity
    {
        SpriteFont _font;
        public Player(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var sr = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprPlayer"), Animation = "default" };
            sr.CenterOrigin();
            t2D.Position += sr.Origin;
            var c2D = new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(16, 24) { Offset = -sr.Origin }, Tag = Globals.TAG_PLAYER };
            var m2D = new Mover2D(this);
            new PlayerController(this, t2D, c2D, sr, m2D);

            Globals.Money = 100;
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var str = $"${Globals.Money}";
            var bounds = _font.MeasureString(str);
            spriteBatch.DrawFilledRectangle(new Vector2(Camera.CameraSize.X - 5 - bounds.X * 2 - 2, 0), new Vector2(bounds.X * 2 + 7, bounds.Y * 2 + 2), Color.Black);
            spriteBatch.DrawRectangle(new Rectangle((int)Camera.CameraSize.X - 5 - (int)bounds.X * 2 - 2, 0, (int)bounds.X * 2 + 7, (int)bounds.Y * 2 + 2), Color.White, 1.0f);
            spriteBatch.DrawString(_font, str, new Vector2(Camera.CameraSize.X - 5, 3), Color.Yellow, 0.0f, new Vector2(bounds.X, 0), 2.0f, SpriteEffects.None, 0.0f);

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
