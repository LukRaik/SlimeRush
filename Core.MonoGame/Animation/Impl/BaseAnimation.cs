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
using Core.MonoGame.Animation.Enum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.Animation.Impl
{
    public class BaseAnimation : IAnimation
    {
        public AnimCode CurrentAnimation { get; private set; }

        private readonly Dictionary<AnimCode, StaticAnimation[]> _frames = new Dictionary<AnimCode, StaticAnimation[]>();

        private readonly double _frameSpeed;

        public BaseAnimation(Dictionary<AnimCode, Texture2D[]> textures, AnimCode curAnim, double frameSpeed = 1000)
        {

            foreach (var animation in textures)
            {
                var len = animation.Value.Length;

                _frames.Add(animation.Key, new StaticAnimation[len]);
                if (animation.Key == AnimCode.GoMovRight) _frames.Add(AnimCode.GoMovLeft, new StaticAnimation[len]);
                for (int i = 0; i < len; i++)
                {
                    _frames[animation.Key][i] = new StaticAnimation(animation.Value[i]);
                    if (animation.Key == AnimCode.GoMovRight) _frames[AnimCode.GoMovLeft][i] = new StaticAnimation(animation.Value[i]);
                }
            }

            SetAnim(curAnim);

            SetScale(10);

            _frameSpeed = frameSpeed;
        }


        public void SetScale(float scale = 1)
        {
            foreach (var anim in _frames)
            {
                foreach (var frame in anim.Value)
                {
                    frame.SetScale(scale);
                }
            }
        }

        private int _curFrame = 0;

        private int _curAnimationFrames = 0;

        private double _timeElapsed = 0;

        public void Draw(SpriteBatch spriteBatch, Vector2 position, GameTime gameTime)
        {
            if (_frames[CurrentAnimation].Length != 1)
            {
                _timeElapsed = _timeElapsed + gameTime.ElapsedGameTime.TotalMilliseconds;

                if (_timeElapsed > _frameSpeed)
                {
                    _curFrame++;

                    if (_curFrame >= _curAnimationFrames) _curFrame = 0;

                    _timeElapsed = 0;
                }

                if (CurrentAnimation == AnimCode.GoMovLeft) _frames[AnimCode.GoMovRight][_curFrame].FlippedDraw(spriteBatch, position);
                else _frames[CurrentAnimation][_curFrame].Draw(spriteBatch, position, gameTime);
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void SetAnim(AnimCode code)
        {
            CurrentAnimation = code;

            _timeElapsed = 0;

            _curFrame = 0;

            _curAnimationFrames = _frames[CurrentAnimation].Length;
        }

        public Rectangle GetRectangle()
        {
            return _frames[CurrentAnimation][_curFrame].GetRectangle();
        }
    }
}