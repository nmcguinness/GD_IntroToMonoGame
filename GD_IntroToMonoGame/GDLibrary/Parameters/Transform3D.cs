using Microsoft.Xna.Framework;

namespace GDLibrary
{
    /// <summary>
    /// Holds data related to actor (e.g. player, pickup, decorator, architecture, camera) position
    /// </summary>
    public class Transform3D
    {
        private Vector3 translation, rotationInDegrees, scale;
        private Vector3 look, up; //right = look x up

        public Vector3 Look
        {
            get
            {
                look.Normalize(); //less-cpu intensive than Vector3.Normalize()
                return look;
            }
            set
            {
                this.look = value;
            }
        }
        public Vector3 Up
        {
            get
            {
                up.Normalize(); //less-cpu intensive than Vector3.Normalize()
                return up;
            }
            set
            {
                this.up = value;
            }
        }
        public Vector3 Right
        {
            get
            {
              //  Vector3 right = Vector3.Cross(this.look, this.up);
              //  right.Normalize();
              //  return right;

                return Vector3.Normalize(Vector3.Cross(this.look, this.up));
            }
        }

        public Vector3 Translation {
            get
            {
                return this.translation;
            }
            set
            {
                this.translation = value;
            } 
        }
        public Vector3 RotationInDegrees { get; set; }
        public Vector3 Scale { get; set; }

        //to do...add a constructor suitable for Camera3D (i.e. no rotation or scale)
        public Transform3D(Vector3 translation, Vector3 rotationInDegrees, 
            Vector3 scale, Vector3 look, Vector3 up)
        {
            this.Translation = translation;
            this.RotationInDegrees = rotationInDegrees;
            this.Scale = scale;
            this.Look = look;
            this.Up = up;
        }

        public void TranslateBy(Vector3 delta)
        {
            this.translation += delta;
        }

        public void RotateAroundUpBy(float rotationInDegrees)
        {
            //update the look
            //update the up?
        }

        //to do...Clone etc

    }
}
