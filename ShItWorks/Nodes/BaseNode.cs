using System;
using OpenTK;

namespace ShItWorks.Nodes
{
    public class BaseNode : IDisposable
    {
        public Transformation Transformation = new Transformation();

        public BaseNode()
        {
            RegisterNode();
        }

        protected void RegisterNode()
        {
            Game.Current.RegisterNode(this);
        }

        protected void DeregisterNode()
        {
            Game.Current.DeregisterNode(this);
        }

        #region IDisposable Support
        protected bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DeregisterNode();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
