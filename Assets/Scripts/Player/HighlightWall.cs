using Assets.Scripts.Manager;
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

        Vector2 worldPoint;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //TODO: Implement this
                //WallManager.wm.AddNewHighlightedWalPosition(worldPoint);

                Vector3Int tpos = wallLayer.WorldToCell(worldPoint);
                
                // Try to get a tile from cell position
                //var highlightLayerTile = highlightLayer.GetTile(tpos);
                Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
                newHighlightLayerTile.sprite = highlightSprite;

                var wallLayerTile = wallLayer.GetTile(tpos);
                
                GameObject go = wallLayer.GetComponent<Tilemap>().GetInstantiatedObject(tpos);

                Tile newWallLayerTile = ScriptableObject.CreateInstance<Tile>();
                //newWallLayerTile.gameObject.tag = "HighlightedTile";

                if (wallLayerTile)
                {
                    highlightLayer.SetTile(tpos, newHighlightLayerTile);
                    //wallLayer.SetTile(tpos, newWallLayerTile);
                }
            }
        }
    }
}