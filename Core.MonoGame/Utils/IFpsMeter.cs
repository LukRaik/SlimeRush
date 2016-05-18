using Microsoft.Xna.Framework;

namespace Core.MonoGame.Utils
{
    public interface IFpsMeter
    {
        void Update(GameTime time);

        string FpsString { get; }

        int Fps { get; }
    }
}