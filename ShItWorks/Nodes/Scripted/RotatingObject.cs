using System;
using OpenTK;
using ShItWorks.Logic;

namespace ShItWorks.Nodes.Scripted
{
    public class RotatingObject : DrawableNode, ILoopLogic, IInitLogic
    {
        public Vector3 RotateSpeed = new Vector3(30, -10, 5);
        public float MinRotateSpeedMult = 0.2f;
        public float MaxRotateSpeedMult = 4f;
        private float rotateMultiplier = 1f;

        private Vector3 startingPosition = Vector3.Zero;
        private Vector3 bobDirection = Vector3.Zero;
        public float BobFrequency = 5f;
        public float BobAmplitude = 4f;

        public RotatingObject()
        {
            Dispatcher.AddInitDispatcher(this);
            Dispatcher.AddLoopDispatcher(this);
        }

        public void OnInit()
        {
            startingPosition = Transformation.Position;
            rotateMultiplier = RNG.Range(MinRotateSpeedMult, MaxRotateSpeedMult);
            bobDirection = RotateSpeed.Normalized();
        }

        public void OnLoop()
        {
            Vector3 pos = startingPosition;
            pos += bobDirection * BobAmplitude * (float)Math.Sin(BobFrequency * Game.Current.TotalTime);
            Transformation.Position = pos;
            Transformation.Rotate(RotateSpeed * Game.Current.DeltaTime * rotateMultiplier);
        }

        protected override void Dispose(bool disposing)
        {
            Dispatcher.RemoveInitDispatcher(this);
            Dispatcher.RemoveLoopDispatcher(this);
            base.Dispose(disposing);
        }
    }
}
