using System;
using OpenTK;
using ShItWorks.Logic;

namespace ShItWorks.Nodes.Scripted
{
    public class RotatingObject : DrawableNode, ILoopLogic, IInitLogic
    {
        public Vector3 RotateSpeed = new Vector3(30, -10, 5);
        public float MinRotateSpeedMult = 1f;
        public float MaxRotateSpeedMult = 10f;
        private float rotateMultiplier = 1f;

        private Vector3 startingPosition = Vector3.Zero;
        public float BobFrequency = 10f;
        public float BobAmplitude = 2f;

        public RotatingObject()
        {
            Dispatcher.AddInitDispatcher(this);
            Dispatcher.AddLoopDispatcher(this);
        }

        public void OnInit()
        {
            startingPosition = Transformation.Position;
            rotateMultiplier = RNG.Range(MinRotateSpeedMult, MaxRotateSpeedMult);
        }

        public void OnLoop()
        {
            throw new NotImplementedException();
        }
    }
}
