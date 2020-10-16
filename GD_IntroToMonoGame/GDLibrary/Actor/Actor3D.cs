using System;
using System.Collections.Generic;

namespace GDLibrary
{
    public class Actor3D : Actor
    {
        private Transform3D transform3D;

        public Transform3D Transform3D
        {
            get
            {
                return this.transform3D;
            }
            set
            {
                this.transform3D = value;
            }
        }

        public Actor3D(string id, Transform3D transform3D) : base(id)
        {
            this.transform3D = transform3D;
        }

        public override bool Equals(object obj)
        {
            return obj is Actor3D d &&
                   EqualityComparer<Transform3D>.Default.Equals(transform3D, d.transform3D) &&
                   EqualityComparer<Transform3D>.Default.Equals(Transform3D, d.Transform3D);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(transform3D, Transform3D);
        }

        //to do...Clone
        public new object Clone()
        {
            return new Actor3D(this.ID, this.transform3D.Clone() as Transform3D);
        }
    }
}
