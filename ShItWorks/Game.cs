using System;
using System.Drawing;
using System.IO;
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

        public Game() : base(640, 480)
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

        private Vector3[] vertData;
        private Vector3[] colData;
        private Matrix4[] mViewData;

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

            vertData = new Vector3[] {
                new Vector3(-0.8f, -0.8f, 0f),
                new Vector3(0.8f, -0.8f, 0f),
                new Vector3(0f, 0.8f, 0f)
            };

            colData = new Vector3[] {
                new Vector3(1f, 0f, 0f),
                new Vector3(0f, 1f, 0f),
                new Vector3(0f, 0f, 1f)
            };

            mViewData = new Matrix4[]
            {
                Matrix4.Identity
            };  

            GL.ClearColor(Color.LightSkyBlue);
            GL.PointSize(5f);
            GL.Enable(EnableCap.DepthTest);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, vertData.Length * Vector3.SizeInBytes, vertData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(in_vPos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, colData.Length * Vector3.SizeInBytes, colData, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(in_vCol, 3, VertexAttribPointerType.Float, false, 0, 0);

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
            GL.UniformMatrix4(uniform_mView, false, ref mViewData[0]);
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.DisableVertexAttribArray(in_vPos);
            GL.DisableVertexAttribArray(in_vCol);

            SwapBuffers();

            totalFrames++;
            totalTime += e.Time;
        }
    }
}
