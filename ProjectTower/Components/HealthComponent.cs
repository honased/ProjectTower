using HonasGame.ECS;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class HealthComponent : Component
    {
        public delegate void DeathCallback();
        private int _health;
        private int _maxHealth;
        private DeathCallback _callback;

        public int Health
        {
            get => _health;
            set
            {
                _health = value;
                if (_health > _maxHealth) _health = _maxHealth;
                else if(_health <= 0)
                {
                    _health = 0;
                    if (_callback != null) _callback();
                }
            }
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                if (_health > _maxHealth) _health = _maxHealth;
            }
        }

        public HealthComponent(Entity parent, int maxHealth, DeathCallback callback = null) : base(parent)
        {
            _maxHealth = maxHealth;
            _health = maxHealth;
            _callback = callback;
        }

        public void Damage(int damage)
        {
            Health -= damage;
        }
    }
}
