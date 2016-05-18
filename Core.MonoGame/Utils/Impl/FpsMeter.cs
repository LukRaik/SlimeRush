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

namespace Core.MonoGame.Utils.Impl
{
    public class FpsMeter:IFpsMeter
    {
        private double _ms=0;
        private int _frames = 0;
        private int _lastFrames = 0;

        public void Update(GameTime time)
        {
            _frames++;
            _ms+=time.ElapsedGameTime.TotalMilliseconds;
            if (_ms < 1000) return;
            _ms = 0;
            _lastFrames = _frames;
            _frames = 0;
        }

        public string FpsString => $"FPS:{_lastFrames}";

        public int Fps => _lastFrames;
    }
}