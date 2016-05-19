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
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.ContentManagement
{
    public class ContentManager : IContentManager
    {
        private Dictionary<Type, Dictionary<string, object>> contents = new Dictionary<Type, Dictionary<string, object>>();

        private Microsoft.Xna.Framework.Content.ContentManager _contentManager;

        public ContentManager(Microsoft.Xna.Framework.Content.ContentManager manager)
        {
            _contentManager = manager;
        }

        public void LoadContent<T>() where T : class
        {
            ContentAttribute attr = (ContentAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ContentAttribute));
            foreach (var content in attr.Contents)
            {
                if (!contents.ContainsKey(content.Value)) contents.Add(content.Value, new Dictionary<string, object>());
                if (!contents[content.Value].Any()) contents[content.Value] = new Dictionary<string, object>();
                if (!contents[content.Value].ContainsKey(content.Key))
                    contents[content.Value][content.Key] = LoadContent(content.Value, content.Key);
            }
        }

        private object LoadContent(Type type, string path)
        {
            if (type == typeof(Texture2D)) return _contentManager.Load<Texture2D>(path);
            if (type == typeof(SpriteFont)) return _contentManager.Load<SpriteFont>(path);

            throw new NotSupportedException("Cant load this type of content");
        }

        public T GetContent<T>(string name) where T : class
        {
            return (contents[typeof(T)][name] as T);
        }


        public void ClearContent()
        {
            contents = new Dictionary<Type, Dictionary<string, object>>();
            _contentManager.Unload();
        }
    }
}