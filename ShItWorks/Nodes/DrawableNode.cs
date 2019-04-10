using System;
using ShItWorks.Rendering;

namespace ShItWorks.Nodes
{
    public class DrawableNode : BaseNode, IDisposable
    {
        private RenderObject renderer;
        public RenderObject Renderer {
            get => renderer;
            set { renderer = value; if(renderer != null) renderer.BaseNode = this; }
        }

        public DrawableNode()
        {
            Renderer = null;
        }

        public DrawableNode(RenderObject render)
        {
            Renderer = render;
        }

        #region IDisposable Support
        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Renderer?.Dispose();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public new void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
