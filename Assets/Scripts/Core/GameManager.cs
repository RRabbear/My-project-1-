using System.Collections;
using UnityEngine;
using Assets.Scripts.BaseUtils;
using BuildingSystem;
using Assets.Scripts.UI;

namespace Assets.Scripts.Core
{
    //和整个游戏相关的东西从这里走，只允许GameManager处理Level逻辑
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField]
        public BuildItem Item { get; private set; }

        [field: SerializeField]
        public BuildingLayer BuildingLayer { get; private set; }

        private void Awake()
        {
            base.Awake();
            UI.UIManager.Instance.InitUIManager();
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(BuildingLayer != null && Input.GetMouseButtonDown(0))
            {
                BuildingLayer.Build(Camera.main.ScreenToWorldPoint(Input.mousePosition), Item);
                Debug.Log("---build---");
            }
        }
    }
}