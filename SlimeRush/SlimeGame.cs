using System;
using Core.MonoGame.Content;
using Core.MonoGame.ContentManagement;
using Core.MonoGame.Controler;
using Core.MonoGame.Scene;
using Core.MonoGame.Utils;
using Core.MonoGame.Utils.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace SlimeRush
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class SlimeGame : Game, IGame
    {
        private readonly Matrix _scaleMatrix;

        private SpriteBatch _mainSpriteBatch;

        private readonly IContentLibary _contentLibary;

        private IFpsMeter _fpsMeter;

        public ContentManager GameContentManager => this.Content;

        private readonly IScene _scene;

        private readonly IController _controller;

        public T GetService<T>() where T : class
        {
            return this.Services.GetService<T>();
        }

        public SlimeGame(IContentLibary contentLibary, IScene scene, IController controller)
        {
            _contentLibary = contentLibary;
            _scene = scene;
            _controller = controller;
            var graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";

            //Skalowanie ekranu
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.IsFullScreen = true;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;

            _scaleMatrix = Matrix.CreateScale(
                Window.ClientBounds.Width / 801.0f,
                Window.ClientBounds.Height / 481.0f,
                0f);

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _mainSpriteBatch = new SpriteBatch(GraphicsDevice);

            //Skalowanie ekranu
            TouchPanel.DisplayHeight = 480;
            TouchPanel.DisplayWidth = 800;
            TouchPanel.DisplayOrientation = DisplayOrientation.LandscapeLeft;


            _contentLibary.LoadContent<FpsMeter>();

            _fpsMeter = new FpsMeter(_contentLibary);

            _scene.LoadContents();

            //var contentLibary = _container.Get<IContentManager>();

            //contentManager.LoadContent<FpsMeter>();

            //contentManager.LoadContent<SlimeTestObject>();

            //contentManager.LoadContent<ButtonObject>();


            //button = new ButtonObject(contentManager, new Vector2(320, 320));

            //button.OnClick += (gameObject, time) =>
            //{
            //    gameObject.SetAction(new LambdaAction((x, y) =>
            //    {
            //        var obj = x as ButtonObject;

            //        obj?.SetText($"GameTime:{y.TotalGameTime}!");

            //        return true;
            //    }));
            //};

            //for (int i = 0; i < 3; i++)
            //{
            //    _gameObjects.Add(new SlimeTestObject(contentManager, new Vector2(i * 130, 100)));
            //}

            //_gameObjects.ForEach(x => x.SetCollisionObjects(_gameObjects.Select(y => (ICollidable)y).ToList()));

            //_fpsMeter = _container.Get<IFpsMeter>();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            _scene.UnloadContents();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    Exit();

            //// TODO: Add your update logic here

            //var touchCollection = TouchPanel.GetState();

            //if (touchCollection.Count > 0)
            //{
            //    _lastTouch = touchCollection.First();

            //    button.IsClicked(_lastTouch.Position, gameTime);

            //    foreach (var obj in _gameObjects)
            //    {
            //        var clickableObj = obj as IClickable;
            //        if (clickableObj != null)
            //        {

            //            if (_lastTouch.State == TouchLocationState.Released)
            //            {
            //                if (!clickableObj.IsClicked(_lastTouch.Position, gameTime))
            //                {
            //                    var end = _lastTouch.Position;

            //                    obj.SetAction(new GoToPointAction((int)end.X, (int)end.Y));

            //                }
            //            }

            //        }
            //    }
            //}

            //foreach (var obj in _gameObjects) obj.Update(gameTime);
            //button.Update(gameTime);
            _controller.Update(gameTime);

            _scene.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(ClearOptions.Target, Color.CornflowerBlue, 1.0f, 0);
            _fpsMeter.Update(gameTime);

            _mainSpriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, transformMatrix: _scaleMatrix);

            _scene.Draw(_mainSpriteBatch, gameTime);

            _mainSpriteBatch.DrawString(_fpsMeter.SpriteFont, _fpsMeter.FpsString, new Vector2(0, 0), Color.Red);

            _mainSpriteBatch.End();

            GraphicsDevice.SetRenderTarget(null);

            base.Draw(gameTime);
        }
    }
}
