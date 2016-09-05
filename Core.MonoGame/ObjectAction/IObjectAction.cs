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
using Microsoft.Xna.Framework;

namespace Core.MonoGame.ObjectAction
{
    public interface IObjectAction
    {
        void Do(GameObject.GameObject gameObject, GameTime gameTime);

        bool IsFinished { get; }
    }
}