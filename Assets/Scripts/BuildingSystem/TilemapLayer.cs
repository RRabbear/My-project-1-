using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.BuildingSystem
{
    [RequireComponent(typeof(Tilemap))]
    public class TilemapLayer : MonoBehaviour
    {
        protected Tilemap m_tilemap { get; private set; }

        private void Awake()
        {
            m_tilemap = GetComponent<Tilemap>();
        }
    }
}