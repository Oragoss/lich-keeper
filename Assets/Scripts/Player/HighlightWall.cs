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
                        WallManager.wm.CheckToSeeIfWallIsMineable(tpos, wallLayer);
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
    }
}