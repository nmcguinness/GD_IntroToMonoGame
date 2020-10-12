using System;
using Microsoft.Xna.Framework;

namespace GDLibrary
{
    /// <summary>
    /// Encapsulates the projection matrix specific parameters for the camera class
    /// </summary>
    public class ProjectionParameters 
    {
        #region Statics
        /// <summary>
        /// Creates a deep-copy instance of a ProjectionParameters object to be used by the camera
        /// </summary>
        /// <value>
        /// StandardDeepFourThree property gets an instance of a ProjectionParameters object (4.0f / 3, 0.1f, 2500)
        /// </value>
        public static ProjectionParameters StandardDeepFourThree
        {
            get
            {
                return new ProjectionParameters(MathHelper.PiOver2, 4.0f / 3, 1, 2500);
            }
        }

        /// <summary>
        /// Creates a deep-copy instance of a ProjectionParameters object to be used by the camera
        /// </summary>
        /// <value>
        /// StandardDeepSixteenTen property gets an instance of a ProjectionParameters object (16.0f / 10, 1, 2500)
        /// </value>
        public static ProjectionParameters StandardDeepSixteenTen
        {
            get
            {
                return new ProjectionParameters(MathHelper.PiOver2, 16.0f / 10, 0.1f, 2500);
            }
        }

        /// <summary>
        /// Creates a deep-copy instance of a ProjectionParameters object to be used by the camera
        /// </summary>
        /// <value>
        /// StandardMediumFourThree property gets an instance of a ProjectionParameters object (4.0f / 3, 1, 1000)
        /// </value>
        public static ProjectionParameters StandardMediumFourThree
        {
            get
            {
                return new ProjectionParameters(MathHelper.PiOver2, 4.0f / 3, 1, 1000);
            }
        }

        /// <summary>
        /// Creates a deep-copy instance of a ProjectionParameters object to be used by the camera
        /// </summary>
        /// <value>
        /// StandardMediumSixteenTen property gets an instance of a ProjectionParameters object (16.0f / 10, 0.1f, 1000)
        /// </value>
        public static ProjectionParameters StandardMediumSixteenTen
        {
            get
            {
                return new ProjectionParameters(MathHelper.PiOver2, 16.0f / 10, 0.1f, 1000);
            }
        }

        /// <summary>
        /// Creates a deep-copy instance of a ProjectionParameters object to be used by the camera
        /// </summary>
        /// <value>
        /// StandardShallowFourThree property gets an instance of a ProjectionParameters object (4.0f / 3, 0.1f, 500)
        /// </value>
        public static ProjectionParameters StandardShallowFourThree
        {
            get
            {
                return new ProjectionParameters(MathHelper.PiOver2, 4.0f / 3, 0.1f, 500);
            }
        }

        /// <summary>
        /// Creates a deep-copy instance of a ProjectionParameters object to be used by the camera
        /// </summary>
        /// <value>
        /// StandardShallowSixteenTen property gets an instance of a ProjectionParameters object (16.0f / 10, 0.1f, 500)
        /// </value>
        public static ProjectionParameters StandardShallowSixteenTen
        {
            get
            {
                return new ProjectionParameters(MathHelper.PiOver2, 16.0f / 10, 0.1f, 500);
            }
        }
        #endregion

        #region Fields
        //used by perspective projections
        private float fieldOfView, aspectRatio, nearClipPlane, farClipPlane;

        //used by both
        private Matrix projection;
        #endregion

        #region Properties
        /// <summary>
        /// Represents the Field-of-View (FOV) property for a Camera3D instance
        /// </summary>
        /// <value>
        /// FOV gets/sets the value of the FOV field
        /// </value>
        public float FOV
        {
            get
            {
                return this.fieldOfView;
            }
            set
            {
                this.fieldOfView = value;
            }
        }
        /// <summary>
        /// Represents the aspect ratio property for a Camera3D instance
        /// </summary>
        /// <value>
        /// AspectRatio gets/sets the value of the aspectRatio field
        /// </value>
        public float AspectRatio
        {
            get
            {
                return this.aspectRatio;
            }
            set
            {
                this.aspectRatio = value;
            }
        }
        /// <summary>
        /// Represents the nearClipPlane property for a Camera3D instance
        /// </summary>
        /// <value>
        /// NearClipPlane gets/sets the value of the nearClipPlane field
        /// </value>
        public float NearClipPlane
        {
            get
            {
                return this.nearClipPlane;
            }
            set
            {
                this.nearClipPlane = value;
            }
        }
        /// <summary>
        /// Represents the farClipPlane property for a Camera3D instance
        /// </summary>
        /// <value>
        /// FarClipPlane gets/sets the value of the farClipPlane field
        /// </value>
        public float FarClipPlane
        {
            get
            {
                return this.farClipPlane;
            }
            set
            {
                this.farClipPlane = value; //validation
            }
        }

        /// <summary>
        /// Generates the Projection matrix for a Camera3D instance using a specific ProjectionParameter object
        /// </summary>
        /// <value>
        /// Returns the latest projection matrix based on any changes to the contributing fields (e.g. aspect ratio)
        /// </value>
        public Matrix Projection
        {
            get
            {
                this.projection = Matrix.CreatePerspectiveFieldOfView(
                       this.fieldOfView, this.aspectRatio,
                       this.nearClipPlane, this.farClipPlane);
                return this.projection;
            }
        }
        #endregion

        #region Constructors & Core

        /// <summary>
        /// Constructor for the ProjectionParameters object
        /// </summary>
        /// <param name="fieldOfView">Floating-point value representing...</param>
        /// <param name="aspectRatio">Floating-point value representing...</param>
        /// <param name="nearClipPlane">Floating-point value representing...</param>
        /// <param name="farClipPlane">Floating-point value representing...</param>
        /// See <see cref="Math.Random()"/> Math.Random for more info
        public ProjectionParameters(float fieldOfView, float aspectRatio,
            float nearClipPlane, float farClipPlane)
        {
            this.FOV = fieldOfView;
            this.AspectRatio = aspectRatio;
            this.NearClipPlane = nearClipPlane;
            this.FarClipPlane = farClipPlane;
        }

        //to do - Clone, Equals, GetHashCode
        #endregion

    }
}
