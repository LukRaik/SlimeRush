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
using Core.MonoGame.Controler;
using Core.MonoGame.GameObject.ButtonObject;
using Core.MonoGame.Interfaces;
using Core.MonoGame.ObjectAction.Impl;
using Core.MonoGame.ObjectManager;
using Core.MonoGame.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.Scenes
{
    public class TestScene : IScene
    {
        private readonly IContentLibary _contentLibary;

        private readonly IObjectManager _objectManager;

        private readonly IController _controller;

        public TestScene(IContentLibary contentLibary, IObjectManager objectManager, IController controller)
        {
            _contentLibary = contentLibary;
            _objectManager = objectManager;
            _controller = controller;
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            _objectManager.RunForAll(x => x.Draw(spriteBatch, gameTime));
        }

        public void Update(GameTime gameTime)
        {
            _objectManager.RunForAll(x => x.Update(gameTime));

            _objectManager.RunForAll<IClickable>(x => x.IsClicked(_controller.GetCurrentClickPosition()));

            _objectManager.Update(gameTime);
        }

        public void LoadContents()
        {
            _contentLibary.LoadContent<ButtonObject>();

            var identity = _objectManager
                .CreateObject(new ButtonObject(_contentLibary, new Vector2(25, 25), "Tekst wtf"));

            _objectManager.Update(new GameTime());

            _objectManager.GetObject<ButtonObject>(identity).OnClick += (gameObject, time) =>
            {
                gameObject.SetAction(new LambdaAction(((o, gameTime) =>
                {
                    var obj = o as ButtonObject;

                    obj?.SetText($"GameTime:{gameTime.TotalGameTime}!");

                    return true;
                })));
            };

        }

        public void UnloadContents()
        {
            _contentLibary.ClearContent();
            _objectManager.Reset();
        }
    }
}