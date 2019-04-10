using System;
using ShItWorks.Rendering;

namespace ShItWorks.Nodes
{
    public class DrawableNode : BaseNode
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
    }
}
