﻿using System;
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
            RegisterRenderer();
        }

        private bool registered = false;
        protected void RegisterRenderer(RenderObject r = null)
        {
            if (r == null) r = this;
            if (registered)
            {
                ConsoleLog.Warning($"Attempt to register already registered RenderObject {r.ToString()}");
                return;
            }
            Game.Current.RegisterRenderer(r);
            registered = true;
            ConsoleLog.Message($"{r.ToString()} constructed and added to Renderers list");
        }

        protected void DeregisterRenderer(RenderObject r = null)
        {
            if (r == null) r = this;
            if (!registered)
            {
                ConsoleLog.Warning($"Attempt to deregister unregistered RenderObject {r.ToString()}");
                return;
            }
            Game.Current.DeregisterRenderer(r);
            registered = false;
            ConsoleLog.Message($"{r.ToString()} removed from Renderers list");
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
            int[] temp = indices;

            if (offset != 0)
            {
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] += offset;
                }
            }

            return temp;
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
                    DeregisterRenderer();
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
