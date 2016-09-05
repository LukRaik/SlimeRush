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
    public interface ITextAnimation : IAnimation
    {
        /// <summary>
        /// ustawia tekst
        /// </summary>
        /// <param name="text"></param>
        void SetText(string text);


        /// <summary>
        /// Ustawia kolor
        /// </summary>
        /// <param name="color"></param>
        void SetColor(Color color);
    }
}