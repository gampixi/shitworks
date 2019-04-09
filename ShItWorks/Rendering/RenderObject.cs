using System;
using System.Drawing;
using OpenTK;
using ShItWorks.Nodes;

namespace ShItWorks.Rendering
{
    public class RenderObject : IDisposable
    {
        public BaseNode BaseNode;

        private Vector3[] vertices;
        private int[] indices;
        private Vector3[] colors;

        public RenderObject()
        {
            Game.Current.OnGatherRenderers += Game.Current.GatherRenderer;
        }

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

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion


    }
}
