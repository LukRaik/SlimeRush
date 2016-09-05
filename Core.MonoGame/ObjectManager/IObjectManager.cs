using System;
using System.Collections.Generic;
using Core.MonoGame.Interfaces;

namespace Core.MonoGame.ObjectManager
{
    public interface IObjectManager : IUpdateable
    {
        void RemoveObject(int key);

        int CreateObject(GameObject.GameObject obj);

        T GetObject<T>(int id) where T : class;

        void RunForAll(Action<GameObject.GameObject> action);

        void RunForAll<T>(Action<T> action) where T : class;

        void Reset();
    }
}