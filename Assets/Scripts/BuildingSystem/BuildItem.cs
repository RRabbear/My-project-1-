using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.BuildingSystem
{
    [CreateAssetMenu(menuName = "Build/BuildItem", fileName = "BuildItem")]
    public class BuildItem : ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Tile Tile { get; private set; }
    }
}