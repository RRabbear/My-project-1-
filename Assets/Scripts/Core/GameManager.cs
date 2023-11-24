using Assets.Scripts.BaseUtils;
using Assets.Scripts.BuildingSystem;
using Assets.Scripts.Inputs;
using UnityEngine;

namespace Assets.Scripts.Core
{
    //和整个游戏相关的东西从这里走，只允许GameManager处理Level逻辑
    public class GameManager : MonoSingleton<GameManager>
    {
        [field: SerializeField]
        public BuildItem Item { get; private set; }

        [field: SerializeField]
        public BuildingLayer BuildingLayer { get; private set; }

        [SerializeField]
        private InputActionsHandler _inputActionsHandler;


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
            if(BuildingLayer != null && _inputActionsHandler.IsMouseLeftButtonPressed)
            {
                var worldCoords = Camera.main.ScreenToWorldPoint(_inputActionsHandler.MousePostion);
                BuildingLayer.Build(worldCoords, Item);
            }
        }
    }
}