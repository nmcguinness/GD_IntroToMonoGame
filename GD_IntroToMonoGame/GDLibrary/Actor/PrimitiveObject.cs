using GDLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GDLibrary
{
    public class PrimitiveObject : DrawnActor3D
    {
        private IVertexData vertexData;

        public IVertexData IVertexData
        {
            get
            {
                return this.vertexData;
            }
        }

        public PrimitiveObject(string id, Transform3D transform3D, 
            EffectParameters effectParameters, IVertexData vertexData) 
                        : base(id, transform3D, effectParameters)
        {
            this.vertexData = vertexData;
        }

        public override void Draw(GameTime gameTime, Camera3D camera, GraphicsDevice graphicsDevice)
        {
            this.EffectParameters.Effect.World = this.Transform3D.World;
            this.EffectParameters.Effect.View = camera.View;
            this.EffectParameters.Effect.Projection = camera.Projection;
            this.EffectParameters.Effect.Texture = this.EffectParameters.Texture;
            this.EffectParameters.Effect.DiffuseColor = this.EffectParameters.DiffuseColor.ToVector3();
            this.EffectParameters.Effect.Alpha = this.EffectParameters.Alpha;

            this.EffectParameters.Effect.CurrentTechnique.Passes[0].Apply();
            this.IVertexData.Draw(gameTime, this.EffectParameters.Effect, graphicsDevice);
        }



    }
}
