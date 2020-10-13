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
        private VertexData<VertexPositionColorTexture> vertexData;
        private Camera3D camera3D;


        private float moveSpeed = 5;
        private float strafeSpeed = 5;
        private float worldScale = 5000;
    

        public Main()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Window.Title = "My Amazing Game";

            InitCameras3D();
            InitVertices();
            InitTextures();
            InitPrimitives();

            InitEffect();
            InitGraphicsSettings(1024, 768);

            base.Initialize();
        }

        private void InitCameras3D()
        {
            Transform3D transform3D = new Transform3D(new Vector3(0, 25, 50),
                Vector3.Zero, Vector3.Zero, 
                            new Vector3(0, 0, -1), Vector3.UnitY);

            this.camera3D = new Camera3D("simple 1st person", transform3D,
                ProjectionParameters.StandardDeepSixteenTen);
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

        private void InitPrimitives()
        {
            this.vertexData = new VertexData<VertexPositionColorTexture>(
                this.vertices, PrimitiveType.TriangleStrip, 2);
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

        //play around with changing the values inside this method
        private void InitVertices()
        {
            this.vertices
                = new VertexPositionColorTexture[4];

            float halfLength = 0.5f;
            //TL
            vertices[0] = new VertexPositionColorTexture(
                new Vector3(-halfLength, halfLength, 0),
                new Color(255,255,255,255), new Vector2(0, 0));

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

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

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

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //draw vertexdata with back texture and back world matrix
            //step 2 - set texture
            this.effect.Texture = this.backSky;
            //step 3 - scale and translation
            this.vertexData.Draw(this.effect,
                Matrix.Identity 
                * Matrix.CreateScale(new Vector3(worldScale, worldScale, 20))
                * Matrix.CreateTranslation(0, 0, -worldScale/2.0f),
                this.camera3D, this._graphics.GraphicsDevice);

            //draw vertexdata with left texture and left world matrix
            this.effect.Texture = this.leftSky;
            this.vertexData.Draw(this.effect,
                Matrix.Identity
                * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
                * Matrix.CreateRotationY(MathHelper.ToRadians(90))
                * Matrix.CreateTranslation(-worldScale / 2.0f, 0, 0),
                 this.camera3D, this._graphics.GraphicsDevice);


            //draw vertexdata with right texture and right world matrix
            this.effect.Texture = this.rightSky;
            this.vertexData.Draw(this.effect,
                Matrix.Identity
                * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
                * Matrix.CreateRotationY(MathHelper.ToRadians(-90))
                * Matrix.CreateTranslation(worldScale / 2.0f, 0, 0),
                 this.camera3D, this._graphics.GraphicsDevice);

            //draw vertexdata with top texture and top world matrix
            this.effect.Texture = this.topSky;
            this.vertexData.Draw(this.effect,
                Matrix.Identity
                * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
                * Matrix.CreateRotationX(MathHelper.ToRadians(90))
                * Matrix.CreateRotationY(MathHelper.ToRadians(-90))
                * Matrix.CreateTranslation(0, worldScale / 2.0f, 0),
                 this.camera3D, this._graphics.GraphicsDevice);

            //draw vertexdata with front texture and front world matrix
            //step 2 - set texture
            this.effect.Texture = this.frontSky;
            //step 3 - scale and translation
            this.vertexData.Draw(this.effect,
                Matrix.Identity
                * Matrix.CreateScale(new Vector3(worldScale, worldScale, 20))
                * Matrix.CreateRotationY(MathHelper.ToRadians(180))
                * Matrix.CreateTranslation(0, 0, worldScale / 2.0f),
                 this.camera3D, this._graphics.GraphicsDevice);

            //draw vertexdata with grass texture and grass world matrix
            this.effect.Texture = this.grass;
            this.vertexData.Draw(this.effect,
                Matrix.Identity
                * Matrix.CreateScale(new Vector3(worldScale, worldScale, 1))
                * Matrix.CreateRotationX(MathHelper.ToRadians(90))
                * Matrix.CreateTranslation(0, 0, 0),
                 this.camera3D, this._graphics.GraphicsDevice);

            base.Draw(gameTime);
        }
    }
}