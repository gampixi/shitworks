using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace ShItWorks
{
    public sealed class Game : GameWindow
    {
        public static Game Current;

        public Game() : base(640, 480)
        {
            Title = "ShItWorks Game";
            Current = this;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.LightSkyBlue);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            SwapBuffers();
            
        }
    }
}
