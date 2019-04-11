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

        public void OnLoop()
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            Dispatcher.RemoveLoopDispatcher(this);
            base.Dispose(disposing);
        }
    }
}
