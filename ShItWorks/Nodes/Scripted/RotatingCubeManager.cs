using System;
using System.Collections.Generic;
using ShItWorks.Nodes;
using ShItWorks.Input;
using ShItWorks.Logic;

namespace ShItWorks.Nodes.Scripted
{
    class RotatingCubeManager : BaseNode, ILoopLogic
    {
        public List<BaseNode> allCubes = new List<BaseNode>();

        public RotatingCubeManager()
        {
            Dispatcher.AddLoopDispatcher(this);
        }

        public void CreateNewCube()
        {
            RotatingObject newCube = new RotatingObject();
            newCube.Renderer = new Rendering.Volumes.Cube();
            newCube.RotateSpeed = new OpenTK.Vector3(RNG.Range(-10, 10), RNG.Range(-10, 10), RNG.Range(-10, 10));
            newCube.BobFrequency = RNG.Range(1, 4);
            newCube.Transformation.Position = new OpenTK.Vector3(0, 0, -3);
            allCubes.Add(newCube);
        }

        public void RemoveCube()
        {
            if (allCubes.Count <= 0) return;
            allCubes[0].Dispose();
            allCubes.RemoveAt(0);
        }

        public void OnLoop()
        {
            if (Keyboard.KeyPressed(OpenTK.Input.Key.Number1)) CreateNewCube();
            if (Keyboard.KeyPressed(OpenTK.Input.Key.Number2)) RemoveCube();
        }

        protected override void Dispose(bool disposing)
        {
            Dispatcher.RemoveLoopDispatcher(this);
            base.Dispose(disposing);
        }
    }
}
