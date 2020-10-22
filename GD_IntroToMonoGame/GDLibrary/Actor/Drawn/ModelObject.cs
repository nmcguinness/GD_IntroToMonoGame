using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace GDLibrary
{
    /// <summary>
    /// Allows us to draw models objects. These are the FBX files we export from 3DS Max. 
    /// </summary>
    public class ModelObject : DrawnActor3D, ICloneable
    {
        #region Fields
        private Model model;
        private Matrix[] boneTransforms;
        #endregion

        #region Properties
        public Model Model
        {
            get
            {
                return this.model;
            }
            set
            {
                this.model = value;
            }
        }
        public Matrix[] BoneTransforms
        {
            get
            {
                return this.boneTransforms;
            }
            set
            {
                this.boneTransforms = value;
            }
        }
        #endregion

        public ModelObject(string id, ActorType actorType, StatusType statusType,
            Transform3D transform, EffectParameters effectParameters, Model model)
            : base(id, actorType, statusType, transform, effectParameters)
        {
            this.model = model;

            InitializeBoneTransforms();
        }

        /// <summary>
        /// 3DS Max models contain meshes(e.g.a table might have 5 meshes i.e.a top and 4 legs) and each mesh contains a bone.
        ///  
        /// A bone holds the transform that says "move this mesh to this position".Without 5 bones in a table all the meshes would collapse down to be centred on the origin.
        /// Our table, wouldnt look very much like a table!
        /// Take a look at the ObjectManager::DrawObject(GameTime gameTime, ModelObject modelObject) method and see if you can figure out what the line below is doing:
        ///   effect.World = modelObject.BoneTransforms[mesh.ParentBone.Index] * modelObject.GetWorldMatrix();
        /// </summary>
        private void InitializeBoneTransforms()
        {
            //load bone transforms and copy transfroms to transform array (transforms)
            if (this.model != null)
            {
                this.boneTransforms = new Matrix[this.model.Bones.Count];
                model.CopyAbsoluteBoneTransformsTo(this.boneTransforms);
            }
        }

        public override void Draw(GameTime gameTime, Camera3D camera, GraphicsDevice graphicsDevice)
        {
            this.EffectParameters.Effect.View = camera.View;
            this.EffectParameters.Effect.Projection = camera.Projection;
            this.EffectParameters.Effect.Alpha = this.EffectParameters.Alpha;
            this.EffectParameters.Effect.CurrentTechnique.Passes[0].Apply();

            //Not all models NEED a texture. Does a semi-transparent window need a texture?
            if (this.EffectParameters.Texture != null)
            {
                this.EffectParameters.Effect.TextureEnabled = true;
                this.EffectParameters.Effect.Texture = this.EffectParameters.Texture;
            }

            foreach (ModelMesh mesh in this.model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = this.EffectParameters.Effect;
                }
                this.EffectParameters.Effect.World = this.BoneTransforms[mesh.ParentBone.Index] * this.Transform3D.World;
                mesh.Draw();
            }

        }

        public new object Clone()
        {
            //to do...
            return null;
        }
    }
}