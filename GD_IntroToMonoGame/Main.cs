using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace GDLibrary
{
    public class Main : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BasicEffect effect;
        private VertexPositionColorTexture[] vertices;
        private Texture2D backSky, leftSky, rightSky, frontSky, topSky, grass;
        private PrimitiveObject archetypalTexturedQuad;

        private float moveSpeed = 10;
        private float strafeSpeed = 5;
        private float worldScale = 2000;

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        #region Initialization
        protected override void Initialize()
        {
            //IVertexData v = new VertexData<VertexPositionColor>(verts, PrimitiveType.LineList, 1);
            //IVertexData clone = v.Clone() as IVertexData;
            //clone.SetPrimitiveType(PrimitiveType.TriangleList);


            // TODO: Add your initialization logic here
            Window.Title = "My Amazing Game";

            this.cameraManager = new CameraManager();

            InitCameras3D();
            InitManagers();
            InitVertices();
            InitTextures();
            InitEffect();
            InitPrimitives();

            InitGraphicsSettings(1024, 768);

            base.Initialize();
        }

        private void InitManagers()
        {
           // this.cameraManager = new CameraManager();


            this.objectManager = new ObjectManager(this, 6, 10, this.cameraManager);
            Components.Add(this.objectManager);

            //keyboard

            //mouse
        }

        private void InitCameras3D()
        {
            #region Camera 1
            Transform3D transform3D = new Transform3D(new Vector3(0, 50, 10),
                /*Vector3.Zero, Vector3.Zero,*/ new Vector3(0, 0, -1), Vector3.UnitY);

            this.cameraManager.Add(new Camera3D("simple 1st person", ActorType.Camera3D, StatusType.Update, transform3D,
                ProjectionParameters.StandardDeepSixteenTen));
            #endregion

            #region Camera 2

            #endregion


            #region Camera 3

            #endregion


            this.cameraManager.ActiveCameraIndex = 0; //0, 1, 2

        }

        private void InitPrimitives()
        {
            InitWireframeHelpers();
            InitTexturedQuad();
        }

        private void InitWireframeHelpers()
        {
            //to do...add wireframe origin
        }

        private void InitTextures()
        {
            //step 1 - texture
            this.backSky
                = Content.Load<Texture2D>("Assets/Textures/Skybox/back");
            this.leftSky
               = Content.Load<Texture2D>("Assets/Textures/Skybox/left");
            this.rightSky
              = Content.Load<Texture2D>("Assets/Textures/Skybox/right");
            this.frontSky
              = Content.Load<Texture2D>("Assets/Textures/Skybox/front");
            this.topSky
              = Content.Load<Texture2D>("Assets/Textures/Skybox/sky");

            this.grass
              = Content.Load<Texture2D>("Assets/Textures/Foliage/Ground/grass1");
        }

        private void InitVertices()
        {
            this.vertices
                = new VertexPositionColorTexture[4];

            float halfLength = 0.5f;
            //TL
            vertices[0] = new VertexPositionColorTexture(
                new Vector3(-halfLength, halfLength, 0),
                new Color(255, 255, 255, 255), new Vector2(0, 0));

            //BL
            vertices[1] = new VertexPositionColorTexture(
                new Vector3(-halfLength, -halfLength, 0),
                Color.White, new Vector2(0, 1));

            //TR
            vertices[2] = new VertexPositionColorTexture(
                new Vector3(halfLength, halfLength, 0),
                Color.White, new Vector2(1, 0));

            //BR
            vertices[3] = new VertexPositionColorTexture(
                new Vector3(halfLength, -halfLength, 0),
                Color.White, new Vector2(1, 1));
        }

        private void InitTexturedQuad()
        {
            Transform3D transform3D = new Transform3D(Vector3.Zero, Vector3.Zero,
               Vector3.One, Vector3.UnitZ, Vector3.UnitY);

            EffectParameters effectParameters = new EffectParameters(this.effect,
                this.grass, /*bug*/ Color.White, 1);

            IVertexData vertexData = new VertexData<VertexPositionColorTexture>(
                this.vertices, PrimitiveType.TriangleStrip, 2);

            this.archetypalTexturedQuad = new PrimitiveObject("original texture quad",
                ActorType.Decorator, 
                StatusType.Update | StatusType.Drawn,
                transform3D, effectParameters, vertexData);

            //back
            primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
          //  primitiveObject.StatusType = StatusType.Off; //Experiment of the effect of StatusType
            primitiveObject.ID = "sky back";
            primitiveObject.EffectParameters.Texture = this.backSky;
            primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 1);
            primitiveObject.Transform3D.Translation = new Vector3(0, 0, -worldScale / 2.0f);
            this.objectManager.Add(primitiveObject);

            //left
            primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
            primitiveObject.ID = "left back";
            primitiveObject.EffectParameters.Texture = this.leftSky;
            primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 1);
            primitiveObject.Transform3D.RotationInDegrees = new Vector3(0, 90, 0);
            primitiveObject.Transform3D.Translation = new Vector3(-worldScale / 2.0f, 0, 0);
            this.objectManager.Add(primitiveObject);

            //right
            primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
            primitiveObject.ID = "sky right";
            primitiveObject.EffectParameters.Texture = this.rightSky;
            primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 20);
            primitiveObject.Transform3D.RotationInDegrees = new Vector3(0, -90, 0);
            primitiveObject.Transform3D.Translation = new Vector3(worldScale / 2.0f, 0, 0);
            this.objectManager.Add(primitiveObject);

             
            //top
            primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
            primitiveObject.ID = "sky top";
            primitiveObject.EffectParameters.Texture = this.topSky;
            primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 1);
            primitiveObject.Transform3D.RotationInDegrees = new Vector3(90, -90, 0);
            primitiveObject.Transform3D.Translation = new Vector3(0 ,worldScale / 2.0f, 0);
            this.objectManager.Add(primitiveObject);

            //to do...front


            //grass
            primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
            primitiveObject.ID = "grass";
            primitiveObject.EffectParameters.Texture = this.grass;
            primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 1);
            primitiveObject.Transform3D.RotationInDegrees = new Vector3(90, 90, 0);
            this.objectManager.Add(primitiveObject);
        }

        private void InitGraphicsSettings(int width, int height)
        {
            //set resolution
            this._graphics.PreferredBackBufferWidth = width;
            this._graphics.PreferredBackBufferHeight = height;

            //dont forget to apply resolution changes otherwise we wont see the new WxH
            this._graphics.ApplyChanges();

            //set cull mode to show front and back faces - inefficient but we will change later
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            this._graphics.GraphicsDevice.RasterizerState = rs;

            //we use a sampler state to set the texture address mode to solve the aliasing problem between skybox planes
            SamplerState samplerState = new SamplerState();
            samplerState.AddressU = TextureAddressMode.Clamp;
            samplerState.AddressV = TextureAddressMode.Clamp;
            this._graphics.GraphicsDevice.SamplerStates[0] = samplerState;
        }

        private void InitEffect()
        {
            this.effect = new BasicEffect(this._graphics.GraphicsDevice);
            this.effect.VertexColorEnabled = true; //otherwise we wont see RGB
            this.effect.TextureEnabled = true;
        } 

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        #endregion

        #region Update & Draw
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.C))
            {
                this.cameraManager.ActiveCameraIndex++;
            }

                /*
                if (Keyboard.GetState().IsKeyDown(Keys.W))
                {
                    this.camera3D.Transform3D.TranslateBy(
                        moveSpeed * this.camera3D.Transform3D.Look);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.S))
                {
                    this.camera3D.Transform3D.TranslateBy(
                         -1 * moveSpeed * this.camera3D.Transform3D.Look);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    this.camera3D.Transform3D.TranslateBy(
                        -1 * strafeSpeed * this.camera3D.Transform3D.Right);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    this.camera3D.Transform3D.TranslateBy(
                         strafeSpeed * this.camera3D.Transform3D.Right);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.NumPad4))
                {
                    this.camera3D.Transform3D.RotateAroundUpBy(-1);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    this.camera3D.Transform3D.RotateAroundUpBy(1);
                }
                */

                base.Update(gameTime);
        }

        PrimitiveObject primitiveObject = null;
        private CameraManager cameraManager;
        private ObjectManager objectManager;

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //     this.archetypalTexturedQuad.Transform3D.Scale = new Vector3(10, 4, 1);
            //     this.archetypalTexturedQuad.Draw(gameTime, this.camera3D, this._graphics.GraphicsDevice);

            ////back
            //primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
            //primitiveObject.EffectParameters.Texture = this.backSky;
            //primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 1);
            //primitiveObject.Transform3D.Translation = new Vector3(0, 0, -worldScale / 2.0f);
            //primitiveObject.Draw(gameTime, this.camera3D, this._graphics.GraphicsDevice);

            ////left
            //primitiveObject = this.archetypalTexturedQuad.Clone() as PrimitiveObject;
            //primitiveObject.EffectParameters.Texture = this.leftSky;
            //primitiveObject.Transform3D.Scale = new Vector3(worldScale, worldScale, 1);
            //primitiveObject.Transform3D.RotationInDegrees = new Vector3(0, 90, 0);
            //primitiveObject.Transform3D.Translation = new Vector3(-worldScale / 2.0f, 0, 0);
            //primitiveObject.Draw(gameTime, this.camera3D, this._graphics.GraphicsDevice);




            ////draw vertexdata with back texture and back world matrix
            ////step 2 - set texture
            //this.effect.Texture = this.backSky;
            ////step 3 - scale and translation
            //this.vertexData.Draw(this.effect,
            //    Matrix.Identity 
            //    * Matrix.CreateScale(new Vector3(worldScale, worldScale, 20))
            //    * Matrix.CreateTranslation(0, 0, -worldScale/2.0f),
            //    this.camera3D, this._graphics.GraphicsDevice);

            ////draw vertexdata with left texture and left world matrix
            //this.effect.Texture = this.leftSky;
            //this.vertexData.Draw(this.effect,
            //    Matrix.Identity
            //    * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
            //    * Matrix.CreateRotationY(MathHelper.ToRadians(90))
            //    * Matrix.CreateTranslation(-worldScale / 2.0f, 0, 0),
            //     this.camera3D, this._graphics.GraphicsDevice);


            ////draw vertexdata with right texture and right world matrix
            //this.effect.Texture = this.rightSky;
            //this.vertexData.Draw(this.effect,
            //    Matrix.Identity
            //    * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
            //    * Matrix.CreateRotationY(MathHelper.ToRadians(-90))
            //    * Matrix.CreateTranslation(worldScale / 2.0f, 0, 0),
            //     this.camera3D, this._graphics.GraphicsDevice);

            ////draw vertexdata with top texture and top world matrix
            //this.effect.Texture = this.topSky;
            //this.vertexData.Draw(this.effect,
            //    Matrix.Identity
            //    * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
            //    * Matrix.CreateRotationX(MathHelper.ToRadians(90))
            //    * Matrix.CreateRotationY(MathHelper.ToRadians(-90))
            //    * Matrix.CreateTranslation(0, worldScale / 2.0f, 0),
            //     this.camera3D, this._graphics.GraphicsDevice);

            ////draw vertexdata with front texture and front world matrix
            ////step 2 - set texture
            //this.effect.Texture = this.frontSky;
            ////step 3 - scale and translation
            //this.vertexData.Draw(this.effect,
            //    Matrix.Identity
            //    * Matrix.CreateScale(new Vector3(worldScale, worldScale, 20))
            //    * Matrix.CreateRotationY(MathHelper.ToRadians(180))
            //    * Matrix.CreateTranslation(0, 0, worldScale / 2.0f),
            //     this.camera3D, this._graphics.GraphicsDevice);

            ////draw vertexdata with grass texture and grass world matrix
            //this.effect.Texture = this.grass;
            //this.vertexData.Draw(this.effect,
            //    Matrix.Identity
            //    * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
            //    * Matrix.CreateRotationX(MathHelper.ToRadians(90))
            //    * Matrix.CreateTranslation(0, 0, 0),
            //     this.camera3D, this._graphics.GraphicsDevice);

            base.Draw(gameTime);
        }

        #endregion
    }
}