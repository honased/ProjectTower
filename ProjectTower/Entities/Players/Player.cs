using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Rendering;
using Microsoft.Xna.Framework;
using HonasGame.Assets;
using HonasGame.ECS.Components.Physics;
using ProjectTower.Components;
using Microsoft.Xna.Framework.Graphics;
using HonasGame;
using HonasGame.Tiled;

namespace ProjectTower.Entities.Players
{
    public class Player : Entity
    {
        private SpriteFont _font;
        private Texture2D _textureHeart;
        public Player(float x, float y)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            var sr = new SpriteRenderer(this) { Sprite = AssetLibrary.GetAsset<Sprite>("sprPlayer"), Animation = "default" };
            sr.CenterOrigin();
            t2D.Position += sr.Origin;
            var c2D = new Collider2D(this) { Transform = t2D, Shape = new BoundingRectangle(16, 24) { Offset = -sr.Origin }, Tag = Globals.TAG_PLAYER };
            var m2D = new Mover2D(this);
            new PlayerController(this, t2D, c2D, sr, m2D);

            Globals.Money = 225;
            Globals.Health = 5;
            Globals.GameWon = false;
            Globals.LastEnemyToGo = false;
            _font = AssetLibrary.GetAsset<SpriteFont>("fntText");
            _textureHeart = AssetLibrary.GetAsset<Texture2D>("heart");
        }

        public override void Update(GameTime gameTime)
        {
            if(Globals.Health <= 0)
            {
                if (!Scene.GetEntity<RoomTransition>(out var rt))
                {
                    Globals.GameWon = false;
                    Scene.AddEntity(new RoomTransition("map_game_over"));
                }
            }
            else if(Globals.LastEnemyToGo)
            {
                if (!Scene.GetEntity<RoomTransition>(out var rt))
                {
                    bool foundEnemy = false;
                    foreach (Entity e in Scene.GetEntities())
                    {
                        if (e.GetComponent<Collider2D>(out var c2D) && (c2D.Tag & Globals.TAG_ENEMY) > 0)
                        {
                            foundEnemy = true;
                        }
                    }

                    if (!foundEnemy)
                    {
                        Globals.GameWon = true;
                        Scene.AddEntity(new RoomTransition("map_game_over"));
                    }
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var str = $"${Globals.Money}";
            var bounds = _font.MeasureString(str);
            spriteBatch.DrawFilledRectangle(new Vector2(Camera.CameraSize.X - 5 - bounds.X * 2 - 2, 0), new Vector2(bounds.X * 2 + 7, bounds.Y * 2 + 2), Color.Black);
            spriteBatch.DrawRectangle(new Rectangle((int)Camera.CameraSize.X - 5 - (int)bounds.X * 2 - 2, 0, (int)bounds.X * 2 + 7, (int)bounds.Y * 2 + 2), Color.White, 1.0f);
            spriteBatch.DrawString(_font, str, new Vector2(Camera.CameraSize.X - 5, 3), Color.Yellow, 0.0f, new Vector2(bounds.X, 0), 2.0f, SpriteEffects.None, 0.0f);

            Vector2 startPos = new Vector2(Camera.CameraSize.X / 3.0f, 10);
            for(int i = 0; i < Globals.Health; i++)
            {
                spriteBatch.Draw(_textureHeart, startPos + Vector2.UnitX * i * 8, Color.White);
            }

            base.Draw(gameTime, spriteBatch);
        }

        protected override void Cleanup()
        {

        }
    }
}
