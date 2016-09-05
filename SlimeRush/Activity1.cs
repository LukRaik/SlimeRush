using System;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Core.MonoGame.Content;
using Core.MonoGame.ContentManagement;
using Core.MonoGame.Controler;
using Core.MonoGame.ObjectManager;
using Core.MonoGame.Scene;
using Core.MonoGame.Scenes;
using Core.MonoGame.Utils;
using Core.MonoGame.Utils.Impl;
using Ninject;
using SlimeRush.ContentManagement;

namespace SlimeRush
{
    [Activity(Label = "SlimeRush"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , AlwaysRetainTaskState = true
        , LaunchMode = Android.Content.PM.LaunchMode.SingleInstance
        , ScreenOrientation = ScreenOrientation.SensorLandscape
        , ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class Activity1 : Microsoft.Xna.Framework.AndroidGameActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var kernel = new StandardKernel();

            Register(kernel);

            base.OnCreate(bundle);

            var game = kernel.Get<IGame>();

            SetContentView(game.GetService<View>());

            game.Run();
        }

        private void Register(StandardKernel kernel)
        {
            RegisterCore(kernel);

            RegisterUtils(kernel);
        }

        private void RegisterCore(StandardKernel kernel)
        {
            kernel.Bind<IContentLibary>().To<ContentLibary>().InSingletonScope();

            kernel.Bind<IGame>().To<SlimeGame>().InSingletonScope()
                .OnActivation(x =>
                {
                    kernel.Get<IContentLibary>().InjectGameContentManager(x);
                });

            kernel.Bind<IObjectManager>().To<ObjectManager>().InSingletonScope();

            kernel.Bind<IScene>().To<TestScene>().InSingletonScope();

            kernel.Bind<IController>().To<Controller>().InSingletonScope();
        }

        private void RegisterUtils(StandardKernel kernel)
        {

        }
    }
}

