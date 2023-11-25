using System.Collections;
using UnityEngine;
using Assets.Scripts.BaseUtils;
using Assets.Scripts.UI;
using Assets.Scripts.Core;
using UnityEditor;

namespace Assets.Scripts.GamePlay2D
{
    public class LevelCharacterManager
    {
        public GameObject getCharacterPrefabByName(string PrefabName)
        {
            string from_str = "Assets/GamePlay/Prefabs/" + PrefabName + ".prefab";
            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(from_str);
            Debug.Log(go);
            return go;
        }

        

        public void Init()
        {
            LevelEvnetManager.Instance.AddListener(GamePlay2D_CMDType_Character.Character_Spawn, SpawnCharacter);
        }

        void SpawnCharacter(BaseEventArgs args)
        {
            Character_CMD_SpawnEventArgs spawn_character_args = (Character_CMD_SpawnEventArgs)args;

            GameObject.Instantiate(spawn_character_args.SpawnObject, LevelManager.Instance.LevelObjectsRoot);
        }
    }
}