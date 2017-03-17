using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace HolographicSmokeTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Matrix worldMatrix;
        Matrix[] viewMatrix = new Matrix[2];
        Matrix[] projectionMatrix = new Matrix[2];

        BasicEffect basicEffect;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            worldMatrix = Matrix.Identity;
            basicEffect = new BasicEffect(GraphicsDevice);
            basicEffect.World = worldMatrix;
            basicEffect.VertexColorEnabled = true;

            CreateCubeVertexBuffer();
            CreateCubeIndexBuffer();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            var pose = graphics.HolographicCameraPose;
            var transform = graphics.HolographicStereoTransform;

            if(pose != null && transform.Left != null && transform.Right != null)
            {
                projectionMatrix[0] = ToXna(pose.ProjectionTransform.Left);
                projectionMatrix[1] = ToXna(pose.ProjectionTransform.Right);

                viewMatrix[0] = ToXna(transform.Left);
                viewMatrix[1] = ToXna(transform.Right);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Transparent);

            for (int i = 1; i < 2; i++)
            {
                worldMatrix = Matrix.CreateScale(1, 0.5f, 1);
                basicEffect.World = worldMatrix;

                //var viewMatrix = Matrix.CreateLookAt(new Vector3(0, 0, 0), new Vector3(0, 0, 1), Vector3.Up);
                //var projectionMatrix = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver2, GraphicsDevice.Viewport.AspectRatio, 0.1f, 10);
                //basicEffect.View = viewMatrix;
                //basicEffect.Projection = projectionMatrix;
                //GraphicsDevice.SetVertexBuffer(vertices);
                //GraphicsDevice.Indices = indices;

                basicEffect.View = viewMatrix[i];
                basicEffect.Projection = projectionMatrix[i];

                GraphicsDevice.SetVertexBuffer(vertices);
                GraphicsDevice.Indices = indices;

                Viewport vp = new Viewport();
                vp.X = i == 0 ? 0 : (int)graphics.HolographicCameraPose.Viewport.Width / 2;
                vp.Y = i == 0 ? 0 : (int)graphics.HolographicCameraPose.Viewport.Height / 2;
                vp.Width = (int)graphics.HolographicCameraPose.Viewport.Width / 2;
                vp.Height = (int)graphics.HolographicCameraPose.Viewport.Height / 2;

                graphics.GraphicsDevice.Viewport = vp;

                //RasterizerState rasterizerState1 = new RasterizerState ();
                //rasterizerState1.CullMode = CullMode.None;
                //graphics.GraphicsDevice.RasterizerState = rasterizerState1;

                foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
                {
                    pass.Apply();

                    GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, number_of_indices / 3);

                }
            }

            Color[] data = new Color[100 * 100];
            Texture2D rectTexture = new Texture2D(GraphicsDevice, 100, 100);

            for (int i = 0; i < data.Length; ++i)
                data[i] = Color.White;

            rectTexture.SetData(data);
            var position = new Vector2(0, 0);

            spriteBatch.Begin();
            spriteBatch.Draw(rectTexture, position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static Matrix ToXna(System.Numerics.Matrix4x4 matrix)
        {
            var result = Matrix.Identity;

            result.M11 = matrix.M11;
            result.M12 = matrix.M12;
            result.M13 = matrix.M13;
            result.M14 = matrix.M14;

            result.M21 = matrix.M21;
            result.M22 = matrix.M22;
            result.M23 = matrix.M23;
            result.M24 = matrix.M24;

            result.M31 = matrix.M31;
            result.M32 = matrix.M32;
            result.M33 = matrix.M33;
            result.M34 = matrix.M34;

            result.M41 = matrix.M41;
            result.M42 = matrix.M42;
            result.M43 = matrix.M43;
            result.M44 = matrix.M44;

            return result;
        }



        const int number_of_vertices = 8;
        const int number_of_indices = 36;
        VertexBuffer vertices;

        void CreateCubeVertexBuffer()
        {
            VertexPositionColor[] cubeVertices = new VertexPositionColor[number_of_vertices];

            cubeVertices[0].Position = new Vector3(-1, -1, 2);
            cubeVertices[1].Position = new Vector3(-1, -1, 3);
            cubeVertices[2].Position = new Vector3(1, -1, 3);
            cubeVertices[3].Position = new Vector3(1, -1, 2);
            cubeVertices[4].Position = new Vector3(-1, 1, 2);
            cubeVertices[5].Position = new Vector3(-1, 1, 3);
            cubeVertices[6].Position = new Vector3(1, 1, 3);
            cubeVertices[7].Position = new Vector3(1, 1, 2);

            cubeVertices[0].Color = Color.Black;
            cubeVertices[1].Color = Color.Red;
            cubeVertices[2].Color = Color.Yellow;
            cubeVertices[3].Color = Color.Green;
            cubeVertices[4].Color = Color.Blue;
            cubeVertices[5].Color = Color.Magenta;
            cubeVertices[6].Color = Color.White;
            cubeVertices[7].Color = Color.Cyan;

            vertices = new VertexBuffer(GraphicsDevice, VertexPositionColor.VertexDeclaration, number_of_vertices, BufferUsage.WriteOnly);
            vertices.SetData<VertexPositionColor>(cubeVertices);
        }

        IndexBuffer indices;

        void CreateCubeIndexBuffer()
        {
            UInt16[] cubeIndices = new UInt16[number_of_indices];

            //bottom face
            cubeIndices[0] = 0;
            cubeIndices[1] = 2;
            cubeIndices[2] = 3;
            cubeIndices[3] = 0;
            cubeIndices[4] = 1;
            cubeIndices[5] = 2;

            //top face
            cubeIndices[6] = 4;
            cubeIndices[7] = 6;
            cubeIndices[8] = 5;
            cubeIndices[9] = 4;
            cubeIndices[10] = 7;
            cubeIndices[11] = 6;

            //front face
            cubeIndices[12] = 5;
            cubeIndices[13] = 2;
            cubeIndices[14] = 1;
            cubeIndices[15] = 5;
            cubeIndices[16] = 6;
            cubeIndices[17] = 2;

            //back face
            cubeIndices[18] = 0;
            cubeIndices[19] = 7;
            cubeIndices[20] = 4;
            cubeIndices[21] = 0;
            cubeIndices[22] = 3;
            cubeIndices[23] = 7;

            //left face
            cubeIndices[24] = 0;
            cubeIndices[25] = 4;
            cubeIndices[26] = 1;
            cubeIndices[27] = 1;
            cubeIndices[28] = 4;
            cubeIndices[29] = 5;

            //right face
            cubeIndices[30] = 2;
            cubeIndices[31] = 6;
            cubeIndices[32] = 3;
            cubeIndices[33] = 3;
            cubeIndices[34] = 6;
            cubeIndices[35] = 7;

            indices = new IndexBuffer(GraphicsDevice, IndexElementSize.SixteenBits, number_of_indices, BufferUsage.WriteOnly);
            indices.SetData<UInt16>(cubeIndices);

        }
    }
}
