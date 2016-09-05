using System;
using System.Collections.Generic;
using System.Linq;
using Core.Errors;
using Core.MonoGame.Attributes;
using Core.MonoGame.Content;
using Core.MonoGame.ContentManagement;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Ninject;

namespace SlimeRush.ContentManagement
{
    public class ContentLibary : IContentLibary
    {
        private Dictionary<Type, Dictionary<string, object>> _contents = new Dictionary<Type, Dictionary<string, object>>();

        private readonly HashSet<Type> _contentsLoaded = new HashSet<Type>();

        private ContentManager _contentManager;


        public ContentLibary()
        {
        }

        public void LoadContent<T>() where T : class
        {
            if (!_contentsLoaded.Contains(typeof(T)))
            {
                var attrs = typeof(T).GetCustomAttributes(typeof(ContentAttribute), true);
                foreach (ContentAttribute attr in attrs)
                {
                    foreach (var content in attr.Contents)
                    {
                        if (!_contents.ContainsKey(content.Value))
                            _contents.Add(content.Value, new Dictionary<string, object>());
                        if (!_contents[content.Value].Any())
                            _contents[content.Value] = new Dictionary<string, object>();
                        if (!_contents[content.Value].ContainsKey(content.Key))
                            _contents[content.Value][content.Key] = Load(content.Value, content.Key);
                    }
                }
                _contentsLoaded.Add(typeof(T));
            }
        }

        private object Load(Type type, string path)
        {
            try
            {
                object result;
                if (type == typeof(Texture2D)) result = _contentManager.Load<Texture2D>(path);
                else if (type == typeof(SpriteFont)) result = _contentManager.Load<SpriteFont>(path);
                else throw new GameException(GameErrorCode.InvalidAssetType);

                return result;
            }
            catch (Exception ex)
            {
                throw new GameException(GameErrorCode.CantLoadContent, path);
            }
        }

        public T GetContent<T>(string name) where T : class
        {
            if (!_contents.ContainsKey(typeof(T)) || !_contents[typeof(T)].ContainsKey(name))
            {
                throw new GameException(GameErrorCode.AssetNotFound, name);
            }

            return (_contents[typeof(T)][name] as T);
        }


        public void ClearContent()
        {
            _contents = new Dictionary<Type, Dictionary<string, object>>();
            _contentManager.Unload();
        }

        public void InjectGameContentManager(IGame game)
        {
            _contentManager = game.GameContentManager;
        }
    }
}