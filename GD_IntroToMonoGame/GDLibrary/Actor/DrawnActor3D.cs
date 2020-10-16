using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

//triangle = PrimitiveObject(vertex data) => DrawnActor3D(EffectParameters)
namespace GDLibrary
{
    public class DrawnActor3D : Actor3D
    {
        private EffectParameters effectParameters;

        public EffectParameters EffectParameters
        {
            get
            {
                return this.effectParameters;
            }
        }

        public DrawnActor3D(string id, Transform3D transform3D,
            EffectParameters effectParameters) : base(id, transform3D)
        {
            this.effectParameters = effectParameters;
        }

        public virtual void Draw(GameTime gameTime, Camera3D camera, GraphicsDevice graphicsDevice)
        {
            //nothing happens
        }

        public new object Clone()
        {
            return new DrawnActor3D(this.ID, this.Transform3D.Clone() as Transform3D,
                this.effectParameters.Clone() as EffectParameters);
        }
    }
}
