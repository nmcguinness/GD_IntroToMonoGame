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
    }
}
