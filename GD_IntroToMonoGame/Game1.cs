﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GDLibrary
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector3 cameraPosition;
        private Vector3 cameraTarget;
        private Matrix view;
        private Matrix projection;
        private BasicEffect effect;
        private VertexPositionColorTexture[] vertices;
        private Texture2D texture;
        private VertexData<VertexPositionColorTexture> vertexData;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            System.Diagnostics.Debug.WriteLine("Initialize...");

            InitCamera();
            InitVertices();
            InitTextures();
            InitPrimitives();

            InitEffect();
            InitGraphicsSettings();

            base.Initialize();
        }

        private void InitTextures()
        {
            this.texture = Content.Load<Texture2D>("Assets/Textures/Props/Crates/crate1");
        }

        private void InitPrimitives()
        {
            this.vertexData = new VertexData<VertexPositionColorTexture>(
                this.vertices, PrimitiveType.TriangleStrip, 2);
        }

        private void InitGraphicsSettings()
        {
            RasterizerState rs = new RasterizerState();
            rs.CullMode = CullMode.None;
            this._graphics.GraphicsDevice.RasterizerState = rs;
        }

        //play around with changing the values inside this method
        private void InitCamera()
        {
            this.cameraPosition = new Vector3(0, 0, 5);
            this.cameraTarget = new Vector3(0, 0, 0);
            //camera needs a view matrix
            this.view = Matrix.CreateLookAt(cameraPosition,
                cameraTarget, new Vector3(0, 1, 0));

            this.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 4.0f / 3, 1, 10000);

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
                Color.Yellow, new Vector2(1, 1));
        }

        private void InitEffect()
        {
            this.effect = new BasicEffect(this._graphics.GraphicsDevice);
            this.effect.VertexColorEnabled = true; //otherwise we wont see RGB
        }

        protected override void LoadContent()
        {
            System.Diagnostics.Debug.WriteLine("LoadContent...");
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update stuff...
            float moveSpeed = 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                this.cameraPosition -= new Vector3(0, 0, moveSpeed);
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                this.cameraPosition += new Vector3(0, 0, moveSpeed);

            //A/D movement to the camera?
            float strafeSpeed = 0.05f;
            if (Keyboard.GetState().IsKeyDown(Keys.A)){
                this.cameraPosition -= new Vector3(strafeSpeed, 0, 0);
                this.cameraTarget -= new Vector3(strafeSpeed, 0, 0);
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.D)){
                this.cameraPosition += new Vector3(strafeSpeed, 0, 0);
                this.cameraTarget += new Vector3(strafeSpeed, 0, 0);
            }

            this.view = Matrix.CreateLookAt(cameraPosition,
                    cameraTarget, new Vector3(0, 1, 0));

            this.projection = ProjectionParameters.StandardDeepSixteenTen.Projection;

                //Matrix.CreatePerspectiveFieldOfView(
                //MathHelper.ToRadians(45), 4.0f / 3, 1, 1000);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.vertexData.Draw(this.effect,
                Matrix.Identity, this.view,
                ProjectionParameters.StandardDeepSixteenTen.Projection,
                this._graphics.GraphicsDevice);
         

            base.Draw(gameTime);
        }
    }
}