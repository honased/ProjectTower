using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.JSON;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ProjectTower.Entities;
using ProjectTower.Entities.Menus;
using ProjectTower.Entities.Players;

namespace ProjectTower
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            Camera.CameraSize = new Vector2(640, 360);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetLibrary.AddAsset("tileGrass", Content.Load<Texture2D>("Tiled/Tilesets/tileGrass"));
            AssetLibrary.AddAsset("player", Content.Load<Texture2D>("Sprites/Player"));
            var spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("player"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 16, 24));
            AssetLibrary.AddAsset("sprPlayer", spr);

            AssetLibrary.AddAsset("tilesetGrass", new TiledTileset(JSON.FromFile("Content/Tiled/Tilesets/tilesetGrass.json") as JObject));
            AssetLibrary.AddAsset("map_0_0", new TiledMap(JSON.FromFile("Content/Tiled/Maps/map_0_0.json") as JObject));
            AssetLibrary.AddAsset("map_menu", new TiledMap(JSON.FromFile("Content/Tiled/Maps/map_menu.json") as JObject));
            TiledManager.AddSpawnerDefinition("Menu", obj => { return new MainMenu(); });
            TiledManager.AddSpawnerDefinition("Player", obj => { return new Player(obj.X, obj.Y); });
            TiledManager.AddSpawnerDefinition("PolyPath", obj => 
            { 
                var pt = new PolyTest(new Vector2(obj.X, obj.Y)); 
                if(obj.PolyLine != null)
                {
                    foreach(var tup in obj.PolyLine.Positions)
                    {
                        pt.PolyList.Add(new Vector2(tup.Item1, tup.Item2));
                    }
                }
                return pt;
            });


            AssetLibrary.GetAsset<TiledMap>("map_0_0").Goto();

            //font
            AssetLibrary.AddAsset("fntText", Content.Load<SpriteFont>("fonts/fntText"));


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if(Input.IsKeyPressed(Keys.R))
            {
                AssetLibrary.GetAsset<TiledMap>("map_0_0").Goto();
            }

            Scene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            Scene.Draw(gameTime, _spriteBatch, new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight));

            base.Draw(gameTime);
        }
    }
}
