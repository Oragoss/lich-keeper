using Assets.Scripts.Manager;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Player
{
    /**
     * This class will be used to detect and highlight the wall tiles the player has selected.
     */
    public class HighlightWall : MonoBehaviour
    {
        [SerializeField] Tilemap groundLayer;
        [SerializeField] Tilemap wallLayer;
        [SerializeField] Tilemap highlightLayer;

        [SerializeField] Sprite highlightSprite;
        [SerializeField] Sprite deHighlightSprite;

        private bool alreadyHighlighted = true;
        private void Update()
        {
            ToHighlightOrNotToHighlight();

            if (!alreadyHighlighted)
                Highlight();
            else
                Dehighlight();
        }

        private void ToHighlightOrNotToHighlight()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int tpos = wallLayer.WorldToCell(worldPoint);
                alreadyHighlighted = WallManager.wm.CheckIfWallIsAlreadyHighlighted(tpos);
            }
        }

        private void Highlight()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int tpos = wallLayer.WorldToCell(worldPoint);
                var isHighlighted = WallManager.wm.CheckIfWallIsAlreadyHighlighted(tpos);

                if(!isHighlighted)
                {
                    Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
                    newHighlightLayerTile.sprite = highlightSprite;

                    var wallLayerTile = wallLayer.GetTile(tpos);

                    if (wallLayerTile)
                    {
                        highlightLayer.SetTile(tpos, newHighlightLayerTile);
                        WallManager.wm.AddWallToHighlightedWalls(tpos);

                        CheckToSeeIfWallIsMineable(tpos);
                    }
                }
            }
        }

        private void Dehighlight()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int tpos = wallLayer.WorldToCell(worldPoint);
                var isHighlighted = WallManager.wm.CheckIfWallIsAlreadyHighlighted(tpos);

                if (isHighlighted)
                {
                    Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
                    newHighlightLayerTile.sprite = deHighlightSprite;

                    var wallLayerTile = wallLayer.GetTile(tpos);

                    if (wallLayerTile)
                    {
                        highlightLayer.SetTile(tpos, newHighlightLayerTile);
                        WallManager.wm.RemoveHighlightedWallPosition(tpos);
                        WallManager.wm.RemoveMineableWalls(tpos);
                    }
                }
            }
        }
    
        private void CheckToSeeIfWallIsMineable(Vector3Int tpos)
        {
            Vector3Int upperTpos = new Vector3Int(tpos.x, tpos.y + 1, tpos.z);
            Vector3Int upperLeftTpos = new Vector3Int(tpos.x - 1, tpos.y + 1, tpos.z);
            Vector3Int upperRightTpos = new Vector3Int(tpos.x + 1, tpos.y + 1, tpos.z);
            Vector3Int rightTpos = new Vector3Int(tpos.x + 1, tpos.y, tpos.z);
            Vector3Int leftTpos = new Vector3Int(tpos.x - 1, tpos.y, tpos.z);
            Vector3Int lowerTpos = new Vector3Int(tpos.x, tpos.y - 1, tpos.z);
            Vector3Int lowerLeftTpos = new Vector3Int(tpos.x - 1, tpos.y - 1, tpos.z);
            Vector3Int lowerRightTpos = new Vector3Int(tpos.x + 1, tpos.y - 1, tpos.z);

            var upperLeftTile = wallLayer.GetTile(upperLeftTpos);
            var upperRightTile = wallLayer.GetTile(upperRightTpos);
            var upperTile = wallLayer.GetTile(upperTpos);
            var rightTile = wallLayer.GetTile(rightTpos);
            var leftTile = wallLayer.GetTile(leftTpos);
            var lowerTile = wallLayer.GetTile(lowerTpos);
            var lowerLeftTile = wallLayer.GetTile(lowerLeftTpos);
            var lowerRightTile = wallLayer.GetTile(lowerRightTpos);

            if(!upperLeftTile || !upperRightTile || !upperTile || !rightTile || !leftTile || !lowerTile || !lowerLeftTile || !lowerRightTile)
            {
                WallManager.wm.AddMineableWalls(tpos);
            }
        }
    }
}