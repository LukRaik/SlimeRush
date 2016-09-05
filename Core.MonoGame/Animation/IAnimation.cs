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
        /// <summary>
        /// Ustawia skalê
        /// </summary>
        /// <param name="scale"></param>
        void SetScale(float scale = 1.0f);

        /// <summary>
        /// Rysuje animacje
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="position"></param>
        /// <param name="gameTime"></param>
        void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime);

        /// <summary>
        /// Aktualizuje animacje
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Ustawia animacjê
        /// </summary>
        /// <param name="code"></param>
        void SetAnim(AnimCode code);

        /// <summary>
        /// Zwraca aktualn¹ animacje
        /// </summary>
        AnimCode CurrentAnimation { get; }

        /// <summary>
        /// Zwraca obszar animacji
        /// </summary>
        /// <returns></returns>
        Rectangle GetRectangle();
    }
}