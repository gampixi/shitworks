using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace ShItWorks
{
    public class Game : GameWindow
    {
        public static Game Current;

        private uint totalFrames = 0;
        public uint TotalFrames { get { return totalFrames; } }

        private double totalTime = 0.0f;
        public double TotalTime { get { return totalTime; } }

        public Game() : base(640, 480, new GraphicsMode(32, 24, 0, 2))
        {
            Title = "ShItWorks Game";
            Current = this;

            Load += OnLoad;
            UpdateFrame += OnUpdateFrame;
            RenderFrame += OnRenderFrame;
            Resize += OnResize;
        }

        private int programID;
        private int fragmentID;
        private int vertexID;

        private int in_vCol;
        private int in_vPos;
        private int uniform_mView;

        private int vbo_position;
        private int vbo_color;
        private int vbo_mView;
        private int ibo_elements;

        private Vector3[] vertData;
        private Vector3[] colData;
        private int[] indData;

        public List<Nodes.BaseNode> AllNodes = new List<Nodes.BaseNode>();
        public List<Rendering.RenderObject> Renderers = new List<Rendering.RenderObject>();

        private void AddTestCubes()
        {
            Nodes.DrawableNode cube1 = new Nodes.DrawableNode();
            cube1.Transformation.Position = Vector3.One * 2;
            cube1.Renderer = new Rendering.Volumes.Cube();
            AllNodes.Add(cube1);
            Nodes.DrawableNode cube2 = new Nodes.DrawableNode();
            cube2.Transformation.Rotation = Vector3.One * 0.3f;
            cube2.Renderer = new Rendering.Volumes.Cube();
            AllNodes.Add(cube2);
        }

        private void InitalizeProgram()
        {
            programID = GL.CreateProgram();

            LoadShader(@"Shaders\fragment.glsl", ShaderType.FragmentShader, programID, out fragmentID);
            LoadShader(@"Shaders\vertex.glsl", ShaderType.VertexShader, programID, out vertexID);

            GL.LinkProgram(programID);
            ConsoleLog.Message(GL.GetProgramInfoLog(programID));

            in_vCol = GL.GetAttribLocation(programID, "vColor");
            in_vPos = GL.GetAttribLocation(programID, "vPosition");
            uniform_mView = GL.GetUniformLocation(programID, "modelview");

            GL.GenBuffers(1, out ibo_elements);

            if(in_vCol == -1 || in_vPos == -1 || uniform_mView == -1)
            {
                ConsoleLog.Error("Error binding attributes!");
            }

            GL.GenBuffers(1, out vbo_position);
            GL.GenBuffers(1, out vbo_color);
            GL.GenBuffers(1, out vbo_mView);
        }

        private void LoadShader(string fileName, ShaderType type, int program, out int address)
        {
            ConsoleLog.Message($"Loading shader {type} {fileName}");
            address = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(fileName))
            {
                GL.ShaderSource(address, sr.ReadToEnd());
            }
            GL.CompileShader(address);
            GL.AttachShader(program, address);
            ConsoleLog.Message(GL.GetShaderInfoLog(address));
        }

        protected void OnLoad(object sender, EventArgs e)
        {
            InitalizeProgram();

            GL.ClearColor(Color.LightSkyBlue);
            GL.PointSize(5f);
            GL.Enable(EnableCap.DepthTest);

            AddTestCubes();
            UpdateRenderingData();
        }

        private void UpdateRenderingData()
        {
            List<Vector3> verts = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> cols = new List<Vector3>();

            int vertCount = 0;

            foreach(var r in Renderers)
            {
                verts.AddRange(r.GetVertices());
                inds.AddRange(r.GetIndices(vertCount));
                cols.AddRange(r.GetColors());
                vertCount += r.GetIndices().Length;
            }

            vertData = verts.ToArray();
            indData = inds.ToArray();
            colData = cols.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, vertData.Length * Vector3.SizeInBytes, vertData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(in_vPos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, colData.Length * Vector3.SizeInBytes, colData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(in_vCol, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (indData.Length * sizeof(int)), indData, BufferUsageHint.StaticDraw);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        protected void OnResize(object sender, EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }

        protected void OnUpdateFrame(object sender, FrameEventArgs e)
        {
            
        }

        protected void OnRenderFrame(object sender, FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            
            GL.EnableVertexAttribArray(in_vPos);
            GL.EnableVertexAttribArray(in_vCol);

            //Program must be used before anything is bound or uniforms set
            GL.UseProgram(programID);
            //Temporary stuff for doing camera stuff

            int currentIndex = 0;

            foreach(var r in Renderers)
            {
                Matrix4 view = Matrix4.CreatePerspectiveFieldOfView(1.4f, (float)Width / Height, 1.0f, 40.0f);
                Matrix4 mvpm = r.BaseNode.Transformation.ModelMatrix * view;
                GL.UniformMatrix4(uniform_mView, false, ref mvpm);
                GL.DrawElements(BeginMode.Triangles, r.GetIndices().Length, DrawElementsType.UnsignedInt, currentIndex * sizeof(uint));
                currentIndex += r.GetIndices().Length;
            }

            GL.DisableVertexAttribArray(in_vPos);
            GL.DisableVertexAttribArray(in_vCol);

            SwapBuffers();

            totalFrames++;
            totalTime += e.Time;
        }
    }
}
