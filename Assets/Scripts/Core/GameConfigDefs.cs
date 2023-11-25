using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.GamePlay2D;
using Assets.Scripts.BaseUtils;

namespace Assets.Scripts.Core
{
    public class RoleSetting : TableDatabase
    {
        public int RoleID;
        public int RoleType;
        public string RolePrefab;
        public float MoveSpeed;
    }

    public class AllLevelSettings : TableDatabase
    {
        public int LevleID, SubLevelID, LevelType, LevelHeroRoleID;
    }

    public class GameTableConfig : Singleton<GameTableConfig>
    {
        public ConfigTable<RoleSetting> roleConifg = new ConfigTable<RoleSetting>("Configs/RoleConfig.csv");
        public ConfigTable<AllLevelSettings> allLevelSettings = new ConfigTable<AllLevelSettings>("Configs/AllLevelSettings.csv");

        public void CallBlank() { }
    }


}