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
using Core.MonoGame.Animation.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.Animation.Impl
{
    //Ta animacja jest dla obiektów które nie potrzebuj¹ grafiki
    public class BlankAnimation : IAnimation
    {
        readonly Rectangle _rectangle = Rectangle.Empty;

        public void SetScale(float scale = 1)
        {

        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {

        }

        public void Update(GameTime gameTime)
        {

        }

        public void SetAnim(AnimCode code)
        {

        }

        public AnimCode CurrentAnimation => AnimCode.GoStatic;

        public Rectangle GetRectangle()
        {
            return _rectangle;
        }
    }
}