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
using Core.MonoGame.Animation;
using Core.MonoGame.Animation.Enum;
using Core.MonoGame.Animation.Impl;
using Core.MonoGame.Attributes;
using Core.MonoGame.ContentManagement;
using Core.MonoGame.Events;
using Core.MonoGame.Interfaces;
using Core.MonoGame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.GameObject.TestObject
{
    [Content(typeof(Texture2D), "Characters/SlimeGreen/Slime1")]
    [Content(typeof(Texture2D), "Characters/SlimeGreen/Slime2")]
    [Content(typeof(Texture2D), "Characters/SlimeGreen/Slime3")]
    [Content(typeof(Texture2D), "Characters/SlimeGreen/Slime4")]
    public class SlimeTestObject : GameObject, IClickable
    {

        public SlimeTestObject(IContentManager contentManager, Vector2 position) : base(position, CreateAnimation(contentManager))
        {
            this.OnClick += OnObjectClick;

            Random rnd = new Random();
            this.SetSpeed(2f * (float)rnd.Next(1, 13));
        }


        private void OnObjectClick(GameObject gameObject, GameTime gameTime)
        {
            this.ChangeAnimation(CurrentAnimation == AnimCode.MovLeft ? AnimCode.MovRight : AnimCode.MovLeft);
        }

        static IAnimation CreateAnimation(IContentManager contentManager)
        {
            return new AnimatedAnimation(new Dictionary<AnimCode, Texture2D[]>()
            {
                {AnimCode.MovRight, new []
                {
                    contentManager.GetContent<Texture2D>("Characters/SlimeGreen/Slime1"),
                    contentManager.GetContent<Texture2D>("Characters/SlimeGreen/Slime2"),
                    contentManager.GetContent<Texture2D>("Characters/SlimeGreen/Slime3"),
                    contentManager.GetContent<Texture2D>("Characters/SlimeGreen/Slime4")
                } }

            }, AnimCode.MovRight, 275);
        }

        public GameObjectEvent OnClick { get; set; }

        public bool IsClicked(Vector2 pos, GameTime gameTime)
        {
            if (OnClick != null)
            {
                if (this.GetBoundry().Contains(pos))
                {
                    OnClick(this, gameTime);
                    return true;
                }
            }
            return false;
        }
    }
}