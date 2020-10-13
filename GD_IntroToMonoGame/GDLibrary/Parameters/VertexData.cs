using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary
{
    /// <summary>
    /// Holds vertex array, primitive type and primitive count for drawn primitive
    /// </summary>
    public class VertexData<T> : ICloneable where T : struct, IVertexType
    {
        #region Fields
        private T[] vertices;
        private PrimitiveType primitiveType;
        private int primitiveCount;
        #endregion

        #region Properties

        #endregion

        public VertexData(T[] vertices, 
            PrimitiveType primitiveType, int primitiveCount)
        {
            this.vertices = vertices;
            this.primitiveType = primitiveType;
            this.primitiveCount = primitiveCount;
        }

        public object Clone()
        {
            return this;
        }

        public void Draw(BasicEffect effect, 
            Matrix world, Matrix view, Matrix projection,
            GraphicsDevice graphicsDevice)
        {
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.CurrentTechnique.Passes[0].Apply();
            graphicsDevice.DrawUserPrimitives<T>(
                this.primitiveType,
                this.vertices, 0, this.primitiveCount);
        }


    }
}




