using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.JSON;
using HonasGame.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using ProjectTower.Entities;
using ProjectTower.Entities.Enemies;
using ProjectTower.Entities.Menus;
using ProjectTower.Entities.Players;
using ProjectTower.Entities.Spawner;
using ProjectTower.Entities.Towers;
using ProjectTower.Entities.Tutorial;
using ProjectTower.Particles;

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
            _graphics.IsFullScreen = false;

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
            AssetLibrary.AddAsset("orc", Content.Load<Texture2D>("Sprites/Orc"));
            AssetLibrary.AddAsset("rat", Content.Load<Texture2D>("Sprites/Rat"));
            AssetLibrary.AddAsset("giant", Content.Load<Texture2D>("Sprites/Giant"));
            AssetLibrary.AddAsset("backgroundMenu", Content.Load<Texture2D>("Sprites/BackgroundMenu"));
            AssetLibrary.AddAsset("towerPlot", Content.Load<Texture2D>("Sprites/TowerPlot"));
            AssetLibrary.AddAsset("archerTower", Content.Load<Texture2D>("Sprites/ArcherTower"));
            AssetLibrary.AddAsset("mortarTower", Content.Load<Texture2D>("Sprites/MortarTower"));
            AssetLibrary.AddAsset("divineTower", Content.Load<Texture2D>("Sprites/DivineTower"));
            AssetLibrary.AddAsset("magicBall", Content.Load<Texture2D>("Sprites/MagicBall"));
            AssetLibrary.AddAsset("heart", Content.Load<Texture2D>("Sprites/Heart"));
            AssetLibrary.AddAsset("explosion", Content.Load<Texture2D>("Sprites/Explosion"));
            AssetLibrary.AddAsset("circle", Content.Load<Texture2D>("Sprites/circle"));
            AssetLibrary.AddAsset("mortarTarget", Content.Load<Texture2D>("Sprites/MortarTarget"));

            //soundeffects
            AssetLibrary.AddAsset("Repair", Content.Load<SoundEffect>("SoundEffects/Repair"));
            AssetLibrary.AddAsset("CantBuy", Content.Load<SoundEffect>("SoundEffects/CantBuy"));
            AssetLibrary.AddAsset("Death", Content.Load<SoundEffect>("SoundEffects/Death"));
            AssetLibrary.AddAsset("EnemyHit", Content.Load<SoundEffect>("SoundEffects/EnemyHit"));
            AssetLibrary.AddAsset("EnemyThorugh", Content.Load<SoundEffect>("SoundEffects/EnemyThorugh"));
            AssetLibrary.AddAsset("Explosion", Content.Load<SoundEffect>("SoundEffects/Explosion"));
            AssetLibrary.AddAsset("LaserShoot", Content.Load<SoundEffect>("SoundEffects/LaserShoot"));
            AssetLibrary.AddAsset("MageTowerShot", Content.Load<SoundEffect>("SoundEffects/MageTowerShot"));
            AssetLibrary.AddAsset("TowerBought", Content.Load<SoundEffect>("SoundEffects/TowerBought"));
            AssetLibrary.AddAsset("TowerPlace", Content.Load<SoundEffect>("SoundEffects/TowerPlace"));

            var spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("player"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 16, 24));
            AssetLibrary.AddAsset("sprPlayer", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("orc"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 16, 21));
            AssetLibrary.AddAsset("sprOrc", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("rat"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 16, 16));
            AssetLibrary.AddAsset("sprRat", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("giant"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 27, 30));
            AssetLibrary.AddAsset("sprGiant", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("towerPlot"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 12, 12));
            AssetLibrary.AddAsset("sprTowerPlot", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("archerTower"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 12, 24));
            AssetLibrary.AddAsset("sprArcherTower", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("mortarTower"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 12, 24));
            AssetLibrary.AddAsset("sprMortarTower", spr);

            spr = new Sprite(AssetLibrary.GetAsset<Texture2D>("divineTower"));
            spr.Animations.Add("default", SpriteAnimation.FromSpritesheet(1, 0.0, 0, 0, 12, 24));
            AssetLibrary.AddAsset("sprDivineTower", spr);

            AssetLibrary.AddAsset("tilesetGrass", new TiledTileset(JSON.FromFile("Content/Tiled/Tilesets/tilesetGrass.json") as JObject));
            AssetLibrary.AddAsset("map_0_0", new TiledMap(JSON.FromFile("Content/Tiled/Maps/map_0_0.json") as JObject));
            AssetLibrary.AddAsset("map_menu", new TiledMap(JSON.FromFile("Content/Tiled/Maps/map_menu.json") as JObject));
            AssetLibrary.AddAsset("map_tutorial", new TiledMap(JSON.FromFile("Content/Tiled/Maps/map_tutorial.json") as JObject));
            AssetLibrary.AddAsset("map_game_over", new TiledMap(JSON.FromFile("Content/Tiled/Maps/map_game_over.json") as JObject));
            TiledManager.AddSpawnerDefinition("Menu", obj => { return new MainMenu(); });
            TiledManager.AddSpawnerDefinition("GameOver", obj => { return new MenuGameOver(); });
            TiledManager.AddSpawnerDefinition("Tutorial", obj => { return new Tutorial(); });
            TiledManager.AddSpawnerDefinition("Player", obj => { return new Player(obj.X, obj.Y); });
            TiledManager.AddSpawnerDefinition("TowerPlot", obj => { return new TowerPlot(obj.X, obj.Y); });
            TiledManager.AddSpawnerDefinition("CollisionBox", obj => { return new CollisionBox(obj.X, obj.Y, obj.Width, obj.Height); });
            TiledManager.AddSpawnerDefinition("EnemySpawner", obj => { return new EnemySpawner(obj.CustomProperties["Path"] as string); });
            TiledManager.AddSpawnerDefinition("TowerShop", obj => { return new TowerShop(obj.X, obj.Y, obj.Width, obj.Height, obj.CustomProperties["TowerType"] as string, (int)((double)obj.CustomProperties["Cost"])); });
            TiledManager.AddSpawnerDefinition("TowerDisplay", obj => { return new TowerDisplay(obj.X, obj.Y, obj.CustomProperties["Sprite"] as string); });
            TiledManager.AddSpawnerDefinition("EnemyPath", obj => 
            { 
                var ep = new EnemyPath(obj.Name); 
                if(obj.PolyLine != null)
                {
                    foreach(var tup in obj.PolyLine.Positions)
                    {
                        ep.Path.Add(new Vector2(tup.Item1, tup.Item2));
                    }
                }
                return ep;
            });

            Scene.AddParticleSystem(new ExplosionParticleSystem(25));
            Scene.AddParticleSystem(new FireworksParticleSystem(25));

            AssetLibrary.GetAsset<TiledMap>("map_menu").Goto();

            //font
            AssetLibrary.AddAsset("fntText", Content.Load<SpriteFont>("fonts/fntText"));


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            Scene.Update(gameTime);

            if (Scene.GetEntity<MainMenu>(out var menu))
            {
                if (menu.Quit) Exit();
            }

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
