using System.Collections;
using UnityEngine;
using Assets.Scripts.BaseUtils;
using Assets.Scripts.UI;
using Assets.Scripts.GamePlay2D;
using Assets.Scripts.Core;

namespace Assets.Scripts.Core
{
    public enum LevelType
    {
        Game2DLevel = 1
    }
    
    
    //一个关卡对应一个LevelManager，允许跨场景使用。但是玩家切换关卡时，必须重新初始化LevelManager。如果需要跨大关卡传参，那么往更上层的GameManager缓存里放。
    public class LevelManager : MonoSingleton<LevelManager>
    {
        //角色控制器不使用单例，不确定是否是好主意，先这样写试试
        public LevelCharacterManager characterManager;

        public Transform LevelObjectsRoot;
        private void Awake()
        {
            base.Awake();
            InitLevelEventManager();
            InitLevelEventQueue();

            characterManager = new LevelCharacterManager();
            characterManager.Init();

            LevelObjectsRoot = GameObject.Find("LevelObjects").transform;

        }
        /// <summary>
        /// 用这个函数来启动关卡
        /// </summary>
        /// <param name="levelID"></param>
        /// <param name="subLevelID"></param>
        public void LoadLevelConfigFromTable(int levelID , int subLevelID)
        {
            AllLevelSettings levelSetting =  GameTableConfig.Instance.allLevelSettings.FindFirstLine(t => ( t.LevleID == levelID && t.SubLevelID == subLevelID )  );

            RoleSetting newLevelHeroData = GameTableConfig.Instance.roleConifg.FindFirstLine(t => t.RoleID == levelSetting.LevelHeroRoleID);

            //创建主角            
            LevelEventQueue.Instance.EnqueueEvent(
                new Character_CMD_SpawnEventArgs(
                    GamePlay2D_CMDType_Character.Character_Spawn, 
                    this.gameObject,
                    Vector3.zero,
                    CharacterSpwnType.AsHero,
                    characterManager.getCharacterPrefabByName(newLevelHeroData.RolePrefab)
                    )
                );

        }

        

        private GameObject getHeroGameObject()
        {
            return null;
        }

        public T GetHeroInGameInstaceBase<T>() where T:CharacterBase
        {
            return null;
        }


        // Use this for initialization
        void Start()
        {
            

            //为主角添加一个Type1工具
            LevelEventQueue.Instance.EnqueueEvent(new Character_CMD_AddTool(GamePlay2D_CMDType_Character.Character_AddTool, null, 1, GetHeroInGameInstaceBase<CharacterBase>()));
            
            //创建模式栏UI
            //UIManager.Instance.CreateUIByName<BaseUI>("UI_BuildingList" , UILayers.UILayers_Default);
        }

        //事件系统通过回调，严格控制游戏的整体运行流程。但由于事件的触发分布在各个GameObject中，所以无法严格保证先后顺序
        private void InitLevelEventManager()
        {
            LevelEvnetManager.Instance.AddListener(ObjectOperateEvent.SpawnItem, aa );

            LevelEvnetManager.Instance.EventDispatch(new SpawnItemEventArgs(ObjectOperateEvent.SpawnItem, null, "123"));

            LevelEvnetManager.Instance.DelListener(ObjectOperateEvent.SpawnItem, aa );

            LevelEvnetManager.Instance.EventDispatch(new SpawnItemEventArgs(ObjectOperateEvent.SpawnItem, null, "123456"));

        }

        public void aa(BaseEventArgs s)
        {
            SpawnItemEventArgs zz  = (SpawnItemEventArgs)s; 
            
            Debug.Log(zz.ObjectName);
        }

        //事件队列单线程地处理所有事件（允许插队机制)，用来处理必须严格有先后顺序的功能
        private void InitLevelEventQueue()
        {
            LevelEventQueue.Instance.InitEventQueue();
        }
        

        // Update is called once per frame
        void Update()
        {
            //每帧清空事件队列
            LevelEventQueue.Instance.EventQueueTick();
        }
    }
}