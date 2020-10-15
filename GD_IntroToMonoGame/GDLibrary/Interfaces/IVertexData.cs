using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary
{
    /// <summary>
    /// All vertex data objects will implement this interface
    /// </summary>
    public interface IVertexData
    {
        void Draw(GameTime gameTime, Effect effect);
    }
}
