using System;
using ShItWorks.Rendering;

namespace ShItWorks.Nodes
{
    public class DrawableNode : BaseNode
    {
        private RenderObject renderer;
        public RenderObject Renderer { get => renderer; set { renderer = value; renderer.BaseNode = this; } }

        public DrawableNode()
        {
            Renderer = new RenderObject();
        }

        public DrawableNode(RenderObject render)
        {
            Renderer = render;
        }
    }
}
