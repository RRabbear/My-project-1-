using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.BuildingSystem
{
    [System.Serializable]
    public class BuildItemData
    {
        public string Name;
        public Tile Tile;
        public Sprite PreviewSprite;
    }

    [CreateAssetMenu(menuName = "Build/BuildItem", fileName = "BuildItem")]
    public class BuildItem : ScriptableObject
    {
        public BuildItemData Data;
    }
}