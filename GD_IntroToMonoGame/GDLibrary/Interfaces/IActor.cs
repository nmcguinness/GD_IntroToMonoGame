using Microsoft.Xna.Framework;
using System;

namespace GD_IntroToMonoGame.GDLibrary.Interfaces
{
    public interface IActor : ICloneable
    {
        //add methods over time...
        void Update(GameTime gameTime);

        //camera, trigger volume
        //void Draw(GameTime gameTime);

        float GetAlpha();
    }
}
