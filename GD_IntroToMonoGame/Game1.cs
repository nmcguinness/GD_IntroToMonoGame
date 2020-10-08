using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GD_IntroToMonoGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector3 cameraPosition;
        private Matrix view;
        private Matrix projection;
        private VertexPositionColor[] vertices;
        private BasicEffect effect;

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
            this.cameraPosition = new Vector3(0, 0, 20);
            //camera needs a view matrix
            this.view = Matrix.CreateLookAt(cameraPosition,
                Vector3.Zero, new Vector3(0, 1, 0));

            this.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 4.0f / 3, 1, 10000);

        }

        //play around with changing the values inside this method
        private void InitVertices()
        {
            this.vertices = new VertexPositionColor[4];
            vertices[0] = new VertexPositionColor(new Vector3(0, 1, 0),
                            Color.Red);
            vertices[1] = new VertexPositionColor(new Vector3(1, 0, 0),
                                  Color.Green);
            vertices[2] = new VertexPositionColor(new Vector3(-1, 0, 0),
                      Color.Blue);
            vertices[3] = new VertexPositionColor(new Vector3(0, 1, 0),
                      Color.Red);
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

            //System.Diagnostics.Debug.WriteLine(
            //    gameTime.ElapsedGameTime.TotalMilliseconds);

            //System.Diagnostics.Debug.WriteLine(
            //   "\t\t" + gameTime.TotalGameTime.TotalSeconds);

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //update stuff...
            float moveSpeed = 0.1f;
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                this.cameraPosition -= new Vector3(0, 0, moveSpeed);
            else if (Keyboard.GetState().IsKeyDown(Keys.S))
                this.cameraPosition += new Vector3(0, 0, moveSpeed);
            
            //A/D movement to the camera?

            this.view = Matrix.CreateLookAt(cameraPosition,
                    Vector3.Zero, new Vector3(0, 1, 0));

            this.projection = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.PiOver4, 4.0f / 3, 1, 10000);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //System.Diagnostics.Debug.WriteLine("Draw...");

            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.effect.View = this.view;
            this.effect.Projection = this.projection;
            this.effect.CurrentTechnique.Passes[0].Apply();

            this._graphics.GraphicsDevice
                .DrawUserPrimitives<VertexPositionColor>(
                PrimitiveType.LineStrip, //I wonder what this means!
                this.vertices, 0, 3);


            base.Draw(gameTime);
        }
    }
}