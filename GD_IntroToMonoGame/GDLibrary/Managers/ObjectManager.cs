using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace GDLibrary
{
    public class ObjectManager<Actor> where T : struct, IVertexType
    {
        
        private List<Actor> drawnList;
        /*
        public ObjectManager(int initialDrawCount)
        {
            this.drawnList = new List<VertexData<T>>(initialDrawCount);
        }

        public void Add(VertexData<T> vertexData)
        {
            this.drawnList.Add(vertexData);
        }

        public void Draw(Matrix view, Matrix projection, 
                            GraphicsDevice graphicsDevice)
        {
            foreach(VertexData<T> vertexData in this.drawnList){
                vertexData.Draw(effect, this.world,
                    view, projection,
                    graphicsDevice);
            }
        }
        */


    }
}
