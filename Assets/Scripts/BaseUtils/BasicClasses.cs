using System.Collections;
using System;
using UnityEngine;

namespace Assets.Scripts.BaseUtils
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public static T Instance { get; private set; }

        protected void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


    public class Singleton<T> where T : class
    {
        private static T _Instance;
        private static readonly object padlock = new object();
        public static T Instance
        {
            get
            {
                if (null == _Instance)
                {
                    lock (padlock)
                    {
                        if (null == _Instance)
                        {
                            _Instance = Activator.CreateInstance(typeof(T), true) as T;
                        }
                    }
                }
                return _Instance;
            }
        }

    }
}