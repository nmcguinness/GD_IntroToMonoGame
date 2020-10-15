using Microsoft.Xna.Framework;

namespace GDLibrary
{
    public class Camera3D : Actor3D
    {
        //name, active, Transform3D::Reset??
        private ProjectionParameters projectionParameters;

        public Matrix Projection
        {
            get
            {
                return this.projectionParameters.Projection;
            }
        }
        public Matrix View
        {
            get
            {
                return Matrix.CreateLookAt(this.Transform3D.Translation,
                    //to do...add Transform3D::Target
                    this.Transform3D.Translation + this.Transform3D.Look,
                    this.Transform3D.Up);
            }
        }

        public Camera3D(string id, Transform3D transform3D, 
                    ProjectionParameters projectionParameters)
            : base(id, transform3D)
        {
            this.projectionParameters = projectionParameters;
        }
    }
}
