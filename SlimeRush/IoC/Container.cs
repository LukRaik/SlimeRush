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
using Core.MonoGame.ContentManagement;
using Core.MonoGame.Utils;
using Core.MonoGame.Utils.Impl;
using Java.IO;
using Ninject;
using ContentManager = Microsoft.Xna.Framework.Content.ContentManager;

namespace SlimeRush.IoC
{
    public static class Container
    {
        private static StandardKernel _container;

        public static void Initialize(Game1 game)
        {
            _container = new Ninject.StandardKernel();


            _container.Bind<ContentManager>().ToConstant(game.Content).InSingletonScope();
            _container.Bind<IContentManager>().To<Core.MonoGame.ContentManagement.ContentManager>().InSingletonScope();
            _container.Bind<IFpsMeter>().To<FpsMeter>().InSingletonScope();
        }


        public static T Get<T>()
        {
            return _container.Get<T>();
        }
    }
}