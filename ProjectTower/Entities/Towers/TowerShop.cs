using HonasGame;
using HonasGame.Assets;
using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.ECS.Components.Physics;
using Microsoft.Xna.Framework;
using ProjectTower.Components;
using ProjectTower.Entities.Players;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Entities.Towers
{
    public class TowerShop : Entity
    {
        string _towerType;
        int _cost;
        private Collider2D _collider;
        private PopupMessage _pm;
        public TowerShop(float x, float y, float width, float height, string type, int cost)
        {
            var t2D = new Transform2D(this) { Position = new Vector2(x, y) };
            _collider = new Collider2D(this) { Shape = new BoundingRectangle(width, height), Transform = t2D, Tag = Globals.TAG_SHOP };
            _towerType = type;
            _cost = cost;
            _pm = new PopupMessage(this, $"Buy {type} for {cost} [Press 'E']") { Active = false };
        }

        public override void Update(GameTime gameTime)
        {
            if(Scene.GetEntity<Player>(out var player))
            {
                _pm.Active = _collider.CollidesWith(Globals.TAG_PLAYER) && !player.GetComponent<TowerPickup>(out var tp);
                if (_pm.Active && Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.E) && Globals.Money >= _cost)
                {
                    string towerSprite = "";
                    Globals.Money -= _cost;
                    switch(_towerType)
                    {
                        case "Mage Tower":
                            towerSprite = "sprArcherTower";
                            _cost += 50;
                            break;

                        case "Mortar Tower":
                            towerSprite = "sprMortarTower";
                            _cost += 50;
                            break;

                        case "Divine Tower":
                            towerSprite = "sprDivineTower";
                            _cost += 50;
                            break;

                        default:
                            throw new Exception();
                    }
                    TowerPickup pickup = new TowerPickup(player, towerSprite, _towerType);
                    _pm.Message = $"Buy {_towerType} for {_cost} [Press 'E']";
                }
            }

            base.Update(gameTime);
        }

        protected override void Cleanup()
        {

        }
    }
}
