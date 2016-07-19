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

namespace Core.MonoGame.Animation
{
    public interface IAnimation
    {
        void SetScale(float scale = 1.0f);

        void Draw(SpriteBatch spriteBatch, Vector2 position);

        void Update(GameTime gameTime);

        void SetAnim(AnimCode code);

        Rectangle GetRectangle();
    }
}