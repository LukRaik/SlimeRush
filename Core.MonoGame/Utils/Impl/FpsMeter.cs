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
using Core.MonoGame.Attributes;
using Core.MonoGame.ContentManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.Utils.Impl
{
    [Content(typeof(SpriteFont), "Fonts/Default")]
    public class FpsMeter : IFpsMeter
    {
        private double? _ms;
        private int _frames = 0;
        private int _lastFrames = 0;

        public SpriteFont SpriteFont { get; set; }

        public FpsMeter(IContentLibary contentLibary)
        {
            SpriteFont = contentLibary.GetContent<SpriteFont>("Fonts/Default");
        }

        public void Update(GameTime time)
        {
            _frames++;
            if (_ms == null) _ms = time.TotalGameTime.TotalMilliseconds;
            if (time.TotalGameTime.TotalMilliseconds - _ms < 1000) return;
            _ms = null;
            _lastFrames = _frames;
            _frames = 0;
        }

        public string FpsString => $"FPS:{_lastFrames}";

        public int Fps => _lastFrames;
    }
}