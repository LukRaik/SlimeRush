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
using Core.MonoGame.Interfaces;
using Microsoft.Xna.Framework;

namespace Core.MonoGame.ObjectAction.Impl
{
    public class GoToPointAction : IObjectAction
    {
        private readonly Vector2 _destination;

        private readonly Rectangle _destRectangle;

        public GoToPointAction(int x, int y)
        {
            _destination = new Vector2(x, y);

            _destRectangle = new Rectangle(x - 2, y - 2, 4, 4);

            IsFinished = false;
        }

        public void Do(GameObject.GameObject gameObject, GameTime gameTime)
        {
            if (IsFinished) return;
            if (gameObject.GetBoundry().Contains(_destRectangle))
            {
                IsFinished = true;
                return;
            }
            var from = gameObject.CurPosition();
            Vector2 dir = Vector2.Normalize(_destination - from);
            float distance = Vector2.Distance(from, _destination);

            if (distance <= 16)
            {
                gameObject.TrySetPosition(_destination);
                IsFinished = true;
            }

            if (gameObject.ColidingMove(dir).Any())
            {
                IsFinished = true;
            }
        }

        public bool IsFinished { get; private set; }
    }
}