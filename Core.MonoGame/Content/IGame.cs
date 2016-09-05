using System;
using Microsoft.Xna.Framework.Content;

namespace Core.MonoGame.Content
{
    public interface IGame
    {
        ContentManager GameContentManager { get; }

        T GetService<T>() where T : class;

        void Run();
    }
}