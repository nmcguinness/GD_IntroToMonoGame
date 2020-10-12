using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDLibrary
{
    public class ObjectManager<T> where T : struct, IVertexType
    {
        /*
        private List<VertexData<T>> drawnList;

        public ObjectManager(int initialDrawCount)
        {
            this.drawnList = new List<VertexData<T>>(initialDrawCount);
        }

        public void Add(VertexData<T> vertexData)
        {
            this.drawnList.Add(vertexData);
        }

        public void Draw()
        {
            foreach(VertexData<T> vertexData in this.drawnList){
                vertexData.Draw(this.effect, this.world,
                    this.view, this.projection,
                    this.GraphicsDevice);
            }
        }
        */


    }
}
