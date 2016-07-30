using System;
using System.Collections.Generic;
using System.Linq;
using Core.MonoGame.ContentManagement;
using Core.MonoGame.GameObject;
using Core.MonoGame.GameObject.TestObject;
using Core.MonoGame.Interfaces;
using Core.MonoGame.Utils;
using Core.MonoGame.Utils.Impl;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using SlimeRush.IoC;

namespace SlimeRush
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private IFpsMeter _fpsMeter;

        private GameObject _testGameObject;

        private List<GameObject> _gameObjects = new List<GameObject>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = this.Window.ClientBounds.Width;
            graphics.PreferredBackBufferHeight = this.Window.ClientBounds.Height;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);


            Container.Initialize(this);

            var contentManager = Container.Get<IContentManager>();

            contentManager.LoadContent<FpsMeter>();

            contentManager.LoadContent<SlimeTestObject>();

            for (int i = 0; i < 3; i++)
            {
                _gameObjects.Add(new SlimeTestObject(contentManager, new Vector2(i * 130, 100)));
            }

            _gameObjects.ForEach(x => x.SetCollisionObjects(_gameObjects.Select(y => (ICollidable)y).ToList()));

            _fpsMeter = Container.Get<IFpsMeter>();

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();

            // TODO: Add your update logic here

            var touchCollection = TouchPanel.GetState();

            if (touchCollection.Count > 0)
            {
                foreach (var obj in _gameObjects)
                {
                    var clickableObj = obj as IClickable;
                    if (clickableObj != null)
                    {
                        var touchLocation = touchCollection.First();

                        if (touchLocation.State == TouchLocationState.Released)
                        {
                            clickableObj.IsClicked(touchLocation.Position, gameTime);
                        }
                        else
                        {
                            var start = obj.CurPosition();
                            var end = touchLocation.Position;
                            float distance = Vector2.Distance(start, end);
                            Vector2 dir = Vector2.Normalize(end - start);
                            obj.ColidingMove(dir);
                        }
                        obj.Update(gameTime);
                    }


                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _fpsMeter.Update(gameTime);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            spriteBatch.DrawString(_fpsMeter.SpriteFont, _fpsMeter.FpsString, new Vector2(0, 0), Color.AliceBlue);

            _gameObjects.ForEach(x => x.Draw(spriteBatch, gameTime));

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
