using GDLibrary;
using System;
using System.Collections.Generic;

namespace GDLibrary
{
    public class CameraManager
    {
        private List<Camera3D> list;
        private int activeCameraIndex = 0;

        /// <summary>
        /// Indexer for the camera manager
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// See <see href="www."/> for more info
        public Camera3D this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                this.list[index] = value;
            }
        }

        public Camera3D ActiveCamera
        {
            get
            {
                return this.list[this.activeCameraIndex];
            }
        }
        public int ActiveCameraIndex
        {
            get
            {
                return this.activeCameraIndex;
            }
            set
            {
                this.activeCameraIndex = value; //bug!!! [0, list.size()-1]
            }
        }


        public CameraManager()
        {
           this.list = new List<Camera3D>();
        }

        public void Add(Camera3D camera)
        {
            this.list.Add(camera);
        }

        public bool RemoveIf(Predicate<Camera3D> predicate)
        {
            int position = this.list.FindIndex(predicate);

            if (position != -1)
            {
                this.list.RemoveAt(position);
                return true;
            }

            return false;
        }
    }
}
