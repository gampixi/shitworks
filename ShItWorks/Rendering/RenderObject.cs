using System;
using System.Drawing;
using OpenTK;
using ShItWorks.Nodes;

namespace ShItWorks.Rendering
{
    public class RenderObject
    {
        private BaseNode baseNode;

        private Vector3[] vertices;
        private int[] indices;
        private Vector3[] colors;

        public void SetVertices(Vector3[] newVertices)
        {
            vertices = newVertices;
        }

        public Vector3[] GetVertices()
        {
            return vertices;
        }

        public void SetIndices(int[] newIndices)
        {
            indices = newIndices;
        }

        public int[] GetIndices(int offset = 0)
        {
            return indices;
        }

        public void SetColors(Vector3[] newColors)
        {
            colors = newColors;
        }

        public Vector3[] GetColors()
        {
            return colors;
        }

    }
}
