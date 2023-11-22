using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public enum ObjectOperateEvent
    {
        SpawnItem,
    }
    public class SpawnItemEventArgs : BaseEventArgs
    {
        public string ObjectName;
        public SpawnItemEventArgs(ObjectOperateEvent _t , GameObject _sender ,  string objectName) : base(_t , _sender)
        {
            ObjectName = objectName;
        }
    }
}
