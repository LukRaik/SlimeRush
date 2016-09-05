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
using Core.Errors;
using Microsoft.Xna.Framework;

namespace Core.MonoGame.ObjectManager
{
    public class ObjectManager : IObjectManager
    {
        public static int IdCounter = 1;

        private readonly Dictionary<int, GameObject.GameObject> _objects;

        private readonly Queue<Action<Dictionary<int, GameObject.GameObject>>> _actions = new Queue<Action<Dictionary<int, GameObject.GameObject>>>();

        public ObjectManager()
        {
            _objects = new Dictionary<int, GameObject.GameObject>();
        }

        public void RemoveObject(int key)
        {
            _actions.Enqueue(x => x.Remove(key));
        }

        public int CreateObject(GameObject.GameObject obj)
        {
            var id = IdCounter++;

            _actions.Enqueue(x => x.Add(id, obj));

            return id;
        }

        public T GetObject<T>(int id) where T : class
        {
            if (!_objects.ContainsKey(id))
            {
                throw new GameException(GameErrorCode.ObjectNotFound);
            }

            var result = _objects[id] as T;

            if (result == null)
            {
                throw new GameException(GameErrorCode.ObjectIsOtherType);
            }

            return result;
        }

        public void RunForAll(Action<GameObject.GameObject> action)
        {
            foreach (var pair in _objects)
            {
                action.Invoke(pair.Value);
            }
        }

        public void RunForAll<T>(Action<T> action) where T : class
        {
            foreach (var pair in _objects)
            {
                T typed = pair.Value as T;

                if (typed != null) action.Invoke(typed);
            }
        }

        public void Reset()
        {
            _objects.Clear();
            _actions.Clear();
            IdCounter = 1;
        }

        public void Update(GameTime gameTime)
        {
            while (_actions.Any())
            {
                var action = _actions.Dequeue();

                action.Invoke(_objects);
            }
        }
    }
}