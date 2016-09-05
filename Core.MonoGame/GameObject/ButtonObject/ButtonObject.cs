using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Core.MonoGame.Animation;
using Core.MonoGame.Animation.Impl;
using Core.MonoGame.Attributes;
using Core.MonoGame.ContentManagement;
using Core.MonoGame.Events;
using Core.MonoGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.GameObject.ButtonObject
{
    [Content(typeof(SpriteFont), "Fonts/Default")]
    public class ButtonObject : GameObject, IClickable
    {
        private readonly ITextAnimation _textAnimation;

        public void SetText(string text) => _textAnimation.SetText(text);

        public void SetColor(Color color) => _textAnimation.SetColor(color);

        public ButtonObject(IContentLibary contentLibary, Vector2 position, string text = "button") :
            base(position, new TextAnimation("", Color.Red, contentLibary.GetContent<SpriteFont>("Fonts/Default")))
        {
            _textAnimation = this.GetAnimation<ITextAnimation>();
            _textAnimation.SetColor(Color.Red);
            _textAnimation.SetText(text);
        }

        public GameObjectEvent OnClick { get; set; }
        public bool IsClicked(Vector2 pos, GameTime gameTime)
        {
            var rectangle = _textAnimation.GetRectangle();
            rectangle.X += (int)this.Position.X;
            rectangle.Y += (int)this.Position.Y;
            if (!rectangle.Contains(pos)) return false;
            OnClick?.Invoke(this, gameTime);
            return true;
        }
    }
}