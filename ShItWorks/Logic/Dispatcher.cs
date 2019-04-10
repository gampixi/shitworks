using System;
using System.Collections.Generic;

namespace ShItWorks.Logic
{
    public static class Dispatcher
    {
        private static List<IInitLogic> initDispatch = new List<IInitLogic>();
        private static List<ILoopLogic> loopDispatch = new List<ILoopLogic>();

        public static void AddInitDispatcher(IInitLogic d)
        {
            initDispatch.Add(d);
        }

        public static void RemoveInitDispatcher(IInitLogic d)
        {
            initDispatch.Remove(d);
        }

        public static void AddLoopDispatcher(ILoopLogic d)
        {
            loopDispatch.Add(d);
        }

        public static void RemoveLoopDispatcher(ILoopLogic d)
        {
            loopDispatch.Remove(d);
        }

        public static void HandleInitDispatch()
        {
            foreach(var d in initDispatch)
            {
                d.OnInit();
            }

            initDispatch.Clear();
        }

        public static void HandleLoopDispatch()
        {
            foreach(var d in loopDispatch)
            {
                d.OnLoop();
            }
        }

        public static void ClearAllDispatchers()
        {
            initDispatch.Clear();
            loopDispatch.Clear();
        }
    }
}
