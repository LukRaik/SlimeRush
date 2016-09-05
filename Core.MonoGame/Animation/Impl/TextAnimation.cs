using Core.MonoGame.Animation.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.Animation.Impl
{
    public class TextAnimation : ITextAnimation
    {
        private readonly SpriteFont _spriteFont;

        private readonly IAnimation _background;

        private Color _color;

        private string _text;

        private Vector2 _origin;

        private float _scale = 1;

        private Rectangle _stringRectangle;

        public TextAnimation(
            string text,
            Color baseColor,
            SpriteFont spriteFont,
            IAnimation background)
        {
            _spriteFont = spriteFont;
            _background = background;
            SetColor(baseColor);
            SetText(text);
        }

        public TextAnimation(
            string text,
            Color baseColor,
            SpriteFont spriteFont)
        {
            _spriteFont = spriteFont;
            SetColor(baseColor);
            SetText(text);
        }


        public void SetScale(float scale = 1)
        {
            _background?.SetScale(scale);

            _scale = scale;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            _background?.Draw(spriteBatch, position, gameTime);

            spriteBatch.DrawString(_spriteFont, _text, position, _color, 0f, _origin, _scale, SpriteEffects.None, 0f);
        }

        public void Update(GameTime gameTime)
        {
            //TODO COKOLWIEK TUTAJ ? D:
        }

        public void SetAnim(AnimCode code)
        {
            _background?.SetAnim(code);
        }

        public AnimCode CurrentAnimation => _background?.CurrentAnimation ?? AnimCode.BnFree;

        public Rectangle GetRectangle()
        {
            return _background?.GetRectangle() ?? _stringRectangle;
        }

        public void SetText(string text)
        {
            _text = text;
            var vector = _spriteFont.MeasureString(_text);
            _origin = new Vector2(vector.X / 2, 0);
            _stringRectangle = new Rectangle((int)(-1*vector.X / 2), 0, (int)vector.X, (int)vector.Y);
        }

        public void SetColor(Color color)
        {
            _color = color;
        }
    }
}