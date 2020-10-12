using GDLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GD_IntroToMonoGame
{
    public class PrimitiveObject<T> where T : struct, IVertexType
    {
        private VertexData<T> vertexData;
        private Matrix world;
        private Texture2D texture;

    }
}
