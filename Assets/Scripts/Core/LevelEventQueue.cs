﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.BaseUtils;

namespace Assets.Scripts.Core
{
    public class LevelEventQueue : Singleton<LevelEventQueue>
    {
        Queue<BaseEventArgs> eventQueue = new Queue<BaseEventArgs>();
        public void EnqueueEvent(BaseEventArgs args)
        {
            if(null == eventQueue)
            {
                eventQueue = new Queue<BaseEventArgs>();
            }
            eventQueue.Enqueue(args);
        }
        public void EventQueueTick()
        {
            if(null==eventQueue) return;
            while (eventQueue.Count > 0)
            {
                LevelEvnetManager.Instance.EventDispatch(eventQueue.Dequeue()); 
            }
        }

        public void InitEventQueue()
        {

        }
    }
}
