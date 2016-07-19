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
using Core.MonoGame.Animation.Impl;
using Core.MonoGame.Attributes;
using Core.MonoGame.ContentManagement;
using Core.MonoGame.Events;
using Core.MonoGame.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.GameObject.TestObject
{
    [Content(typeof(Texture2D), "Characters/SlimeBlue/Slime1")]
    public class SlimeTestObject : GameObject
    {


        public SlimeTestObject(IContentManager contentManager, Vector2 position) : base(position, CreateAnimation(contentManager))
        {
            //this.Register(OnUpdate);

            Random rnd = new Random();
            this.SetSpeed(2f*(float)rnd.Next(1,13));
        }


        private int _side = 1;

        private void OnUpdate(GameObject gameObject, GameTime gameTime)
        {
            if (gameObject.CurPosition().X.IsBeetwen(300, 340))
            {
                _side = -1;
            }

            if (gameObject.CurPosition().X.IsBeetwen(100, 140))
            {
                _side = 1;
            }

            gameObject.Move(new Vector2(3 * _side, 0));
        }



        static IAnimation CreateAnimation(IContentManager contentManager)
        {
            return new StaticAnimation(contentManager.GetContent<Texture2D>("Characters/SlimeBlue/Slime1"));
        }
    }
}