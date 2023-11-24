using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem
{
    public class BuildingLayer : TilemapLayer
    {
        public void Build(Vector3 worldCoords, BuildItem item)
        {
            if (item.Tile != null && m_tilemap != null)
            {
                var tileCoords = m_tilemap.WorldToCell(worldCoords);
                m_tilemap.SetTile(tileCoords, item.Tile);
            }
        }
    }
}