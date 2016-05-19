using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Core.MonoGame.Utils
{
    public interface IFpsMeter
    {
        SpriteFont SpriteFont { get; }

        void Update(GameTime time);

        string FpsString { get; }

        int Fps { get; }
    }
}