﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GD_IntroToMonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector3 cameraPosition;
        private Vector3 cameraTarget;
        private Matrix view;
        private Matrix projection;
        private VertexPositionColor[] vertices;
        private BasicEffect effect;
        private float rotation;

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
            InitEffect();

            base.Initialize();
        }

        //play around with changing the values inside this method
        private void InitCamera()
        {
            this.cameraPosition = new Vector3(0, 0, 10);
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
            //rectangle?
            this.vertices = new VertexPositionColor[4];

            //T
            vertices[0] = new VertexPositionColor(new Vector3(0, 2, 0),
                            Color.Red);

            //BR
            vertices[1] = new VertexPositionColor(new Vector3(1, 0, 0),
                                  Color.Green);
            //BL
            vertices[2] = new VertexPositionColor(new Vector3(-1, 0, 0),
                      Color.Blue);

            //B
            vertices[3] = new VertexPositionColor(new Vector3(0, -2, 0),
                      Color.Yellow);
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

            this.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(45), 4.0f / 3, 1, 1000);

            this.rotation += MathHelper.ToRadians(1f);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //System.Diagnostics.Debug.WriteLine("Draw...");

            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.effect.World = Matrix.CreateRotationX(this.rotation*2)
                * Matrix.CreateRotationZ(this.rotation);

            this.effect.View = this.view; //position
            this.effect.Projection = this.projection; //how you see world
            this.effect.CurrentTechnique.Passes[0].Apply();

            this._graphics.GraphicsDevice
                .DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.TriangleStrip, //I wonder what this means!
                this.vertices, 0, 2);


            base.Draw(gameTime);
        }
    }
}