using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.BuildingSystem
{
    public class BuildingLayer : TilemapLayer
    {
        private Dictionary<Vector3Int, bool> _gridStatus = new Dictionary<Vector3Int, bool>();
        private GameObject _previewObject;

        public void OnEnable()
        {
            _previewObject = new GameObject();
        }

        public void OnDisable()
        {
            if( _previewObject != null )
                Destroy( _previewObject );
        }

        public void Build(Vector3 worldCoords, BuildItem item)
        {
            if (item.Data.Tile != null && m_tilemap != null)
            {
                var tileCoords = WorldCoords2TileCoords(worldCoords);
                m_tilemap.SetTile(tileCoords, item.Data.Tile);
                _gridStatus[tileCoords] = true;
            }
        }

        public Vector3Int WorldCoords2TileCoords(Vector3 worldCoords)
        {
            return m_tilemap.WorldToCell(worldCoords);
        }

        public Vector3 TileCoords2WorldCoords(Vector3Int tileCoords)
        {
            return m_tilemap.CellToWorld(tileCoords);
        }

        public bool IsEmpty(Vector3 worldCoords)
        {
            var tileCoords = WorldCoords2TileCoords(worldCoords);
            if (_gridStatus.ContainsKey(tileCoords)
                || m_tilemap.GetTile(tileCoords) != null )
                return false;
            else
                return true;
        }

        public void ShowPreview(Vector3 worldCoords, BuildItem item)
        {
            var showPosition = TileCoords2WorldCoords(WorldCoords2TileCoords(worldCoords)) + m_tilemap.cellSize / 2;
            Sprite showSprite = item.Data.PreviewSprite;

            if(_previewObject.GetComponent<SpriteRenderer>() == null)
                _previewObject.AddComponent<SpriteRenderer>();

            _previewObject.GetComponent<SpriteRenderer>().enabled = true;
            _previewObject.GetComponent<SpriteRenderer>().sortingOrder = m_tilemap.GetComponent<Renderer>().sortingOrder + 1;
            _previewObject.transform.position = showPosition;
            _previewObject.GetComponent<SpriteRenderer>().sprite = showSprite;

            if (IsEmpty(worldCoords))
                _previewObject.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);
            else
                _previewObject.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 0.5f);
        }

        public void ClearPreview()
        {
            _previewObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}