using System;
using System.Collections.Generic;

namespace ShItWorks.Logic
{
    public static class Dispatcher
    {
        private static List<IInitLogic> initBuffer = new List<IInitLogic>();
        //initRemoveBuffer removes items from initBuffer
        private static List<IInitLogic> initRemoveBuffer = new List<IInitLogic>();
        private static List<IInitLogic> initDispatch = new List<IInitLogic>();

        private static List<ILoopLogic> loopBuffer = new List<ILoopLogic>();
        //loopRemoveBuffer removes items from both loopBuffer and loopDispatch simultaneously
        private static List<ILoopLogic> loopRemoveBuffer = new List<ILoopLogic>();
        private static List<ILoopLogic> loopDispatch = new List<ILoopLogic>();

        public static void AddInitDispatcher(IInitLogic d)
        {
            initBuffer.Add(d);
            ConsoleLog.Message("Init dispatcher registered.");
        }

        public static void RemoveInitDispatcher(IInitLogic d)
        {
            try
            {
                initRemoveBuffer.Add(d);
            }
            catch
            {
                ConsoleLog.Message("Attempt to remove already fired init dispatcher");
            }
        }

        public static void AddLoopDispatcher(ILoopLogic d)
        {
            loopBuffer.Add(d);
            ConsoleLog.Message("Loop dispatcher registered.");
        }

        public static void RemoveLoopDispatcher(ILoopLogic d)
        {
            try
            {
                loopRemoveBuffer.Add(d);
            }
            catch
            {
                ConsoleLog.Warning("Attempt to remove nonexistant loop dispatcher");
            }
}

        public static void HandleInitDispatch()
        {
            foreach(var d in initRemoveBuffer)
            {
                initBuffer.Remove(d);
            }
            initRemoveBuffer.Clear();

            foreach(var d in initBuffer)
            {
                initDispatch.Add(d);
            }
            initBuffer.Clear();

            foreach(var d in initDispatch)
            {
                d.OnInit();
            }

            initDispatch.Clear();
        }

        public static void HandleLoopDispatch()
        {
            foreach(var d in loopRemoveBuffer)
            {
                loopBuffer.Remove(d);
                loopDispatch.Remove(d);
            }

            foreach(var d in loopBuffer)
            {
                loopDispatch.Add(d);
            }

            foreach(var d in loopDispatch)
            {
                d.OnLoop();
            }
        }

        public static void ClearAllDispatchers()
        {
            initDispatch.Clear();
            loopDispatch.Clear();
            initBuffer.Clear();
            loopBuffer.Clear();
        }
    }
}
