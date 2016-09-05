using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core.MonoGame.Animation.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace Core.MonoGame.Animation.Impl
{
    public class StaticAnimation : IAnimation
    {
        private readonly Texture2D _texture;

        private Vector2 _scale;

        private Vector2 _origin;

        private Rectangle _colRectangle = Rectangle.Empty;

        public StaticAnimation(Texture2D texture)
        {
            _texture = texture;

            SetScale(10);

            SetOrigin(x => new Vector2(_texture.Width / 2f, x.Height));
        }

        public void SetOrigin(Func<Texture2D, Vector2> originSetter)
        {
            _origin = originSetter(_texture);
        }

        public void SetScale(float scale = 1.0f)
        {
            _scale = new Vector2() { X = scale, Y = scale };
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            spriteBatch.Draw(_texture, position, null, null, _origin, 0F, _scale, Color.White);
        }

        public void FlippedDraw(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(_texture, position, null, null, _origin, 0F, _scale, Color.White, SpriteEffects.FlipHorizontally);
        }

        public void Update(GameTime gameTime)
        {
            return;
        }

        public void SetAnim(AnimCode code)
        {
            return;
        }

        public AnimCode CurrentAnimation => AnimCode.GoStatic;

        public Rectangle GetRectangle()
        {
            if (_colRectangle == Rectangle.Empty)
            {
                _colRectangle = _texture.Bounds;
                _colRectangle.Width *= (int)_scale.X;
                _colRectangle.Height *= (int)_scale.Y;
                _colRectangle.X -= (int)(_origin.X * _scale.X);
                _colRectangle.Y -= (int)(_origin.Y * _scale.Y);
            }
            return _colRectangle;

        }
    }
}