using HonasGame.ECS;
using HonasGame.ECS.Components;
using HonasGame.Helper;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTower.Components
{
    public class ScaleAnimator : Component
    {
        public Vector2 Scale { get; set; }
        private Vector2 _defaultScale;
        private SpriteRenderer _renderer;

        public ScaleAnimator(Entity parent, Vector2 defaultScale, SpriteRenderer renderer) : base(parent)
        {
            _defaultScale = defaultScale;
            _renderer = renderer;
        }

        public override void Update(GameTime gameTime)
        {
            _renderer.Scale = Scale;

            Scale = HonasMathHelper.LerpDelta(Scale, _defaultScale, 0.2f, gameTime);
        }
    }
}
