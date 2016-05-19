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

namespace Core.MonoGame.GameObjectFactory
{
    public interface IGameObjectFactory<in T>
    {
        void Create(T @object);
    }
}