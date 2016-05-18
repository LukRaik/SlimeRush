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
using Core.MonoGame.Utils;
using Core.MonoGame.Utils.Impl;
using Java.IO;
using Ninject;

namespace SlimeRush.IoC
{
    public static class Container
    {
        private static StandardKernel _container;

        public static void Initialize()
        {
            _container = new Ninject.StandardKernel();



            _container.Bind<IFpsMeter>().To<FpsMeter>().InSingletonScope();
        }


        public static T Get<T>()
        {
            return _container.Get<T>();
        }
    }
}