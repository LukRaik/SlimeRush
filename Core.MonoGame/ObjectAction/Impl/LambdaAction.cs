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

namespace Core.MonoGame.ObjectAction.Impl
{
    public class LambdaAction : IObjectAction
    {
        private readonly Func<GameObject.GameObject, GameTime, bool> _action;

        //Zwraca true gdy akcja ma siê zakoñczyæ
        public LambdaAction(Func<GameObject.GameObject, GameTime, bool> action)
        {
            _action = action;
            IsFinished = false;
        }

        public void Do(GameObject.GameObject gameObject, GameTime gameTime)
        {
            if (IsFinished == false)
            {
                if (_action.Invoke(gameObject, gameTime))
                {
                    IsFinished = true;
                }
            }
        }

        public bool IsFinished { get; private set; }
    }
}