using System;
using OpenTK;

namespace ShItWorks.Nodes
{
    public class Transformation
    {
        private Vector3 position = Vector3.Zero;
        private Vector3 rotation = Vector3.Zero;
        private Vector3 scale = Vector3.One;

        private bool matrixDirty = true;
        private Matrix4 modelMatrix = Matrix4.Identity;

        public Vector3 Position { get => position; set { position = value; matrixDirty = true; } }
        public Vector3 Rotation { get => rotation; set { rotation = value; matrixDirty = true; } }
        public Vector3 Scale { get => scale; set { scale = value; matrixDirty = true; } }
        public Matrix4 ModelMatrix { get { if (matrixDirty) { UpdateModelMatrix(); } return modelMatrix; } set => modelMatrix = value; }

        public void UpdateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScale(Scale)
                * Matrix4.CreateRotationX(Rotation.X)
                * Matrix4.CreateRotationY(Rotation.Y)
                * Matrix4.CreateRotationZ(Rotation.Z)
                * Matrix4.CreateTranslation(Position);
            matrixDirty = false;
        }

        /// <summary>
        /// Translates in global coordinates
        /// </summary>
        /// <param name="delta">Amount to translate by</param>
        public void Translate(Vector3 delta)
        {
            Position += delta;
        }

        /// <summary>
        /// Rotates in global coordinates
        /// </summary>
        /// <param name="eulerDelta">Amount in degrees to rotate by</param>
        public void Rotate(Vector3 eulerDelta)
        {
            Rotation += eulerDelta;
        }
    }
}
