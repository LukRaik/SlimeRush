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
using Core.MonoGame.Events;
using Microsoft.Xna.Framework;

namespace Core.MonoGame.Interfaces
{
    public interface IClickable
    {
        GameObjectEvent OnClick { get; set; }

        bool IsClicked(Vector2 pos, GameTime gameTime);
    }
}