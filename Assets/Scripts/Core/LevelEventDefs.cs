using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.GamePlay2D;

namespace Assets.Scripts.Core
{
    public enum ObjectOperateEvent
    {
        SpawnItem,
    }

    public enum GamePlay2D_CMD_Building
    {
        CreateBuilding
    }

    public enum GamePlay2D_HOOK_Building
    {

    }

    public enum GamePlay2D_CMDType_Character
    {
        Character_Spawn,
        Character_AddTool,
    }

    public enum CharacterSpwnType
    {
        AsHero
    }

    public class SpawnItemEventArgs : BaseEventArgs
    {
        public string ObjectName;
        public SpawnItemEventArgs(ObjectOperateEvent _t , GameObject _sender ,  string objectName) : base(_t , _sender)
        {
            ObjectName = objectName;
        }
    }

    public class Character_CMD_SpawnEventArgs : BaseEventArgs
    {
        public Vector3 pos;
        public CharacterSpwnType spwnType;
        public GameObject SpawnObject;

        public Character_CMD_SpawnEventArgs(GamePlay2D_CMDType_Character _t, GameObject _sender , Vector3 pos, CharacterSpwnType spwnType, GameObject spawnObject) : base(_t , _sender)
        {
            this.pos = pos;
            this.spwnType = spwnType;
            SpawnObject = spawnObject;
        }
    }

    public class Character_CMD_AddTool : BaseEventArgs
    {
        public int toolID;
        public CharacterBase toCharacter;

        public Character_CMD_AddTool(GamePlay2D_CMDType_Character _t, GameObject _sender ,  int toolID, CharacterBase toCharacter) : base(_t , _sender)
        {
            this.toolID = toolID;
            this.toCharacter = toCharacter;
        }
    }
}
