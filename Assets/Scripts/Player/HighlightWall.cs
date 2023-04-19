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
        [SerializeField] Tilemap highlightLayer;

        [SerializeField] Sprite testSprite;

        Vector2 worldPoint;
        RaycastHit2D hit;

        private void Start()
        {

        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                var tpos = groundLayer.WorldToCell(worldPoint);
                
                // Try to get a tile from cell position
                var tile = highlightLayer.GetTile(tpos);
                Tile newTile = ScriptableObject.CreateInstance<Tile>();
                newTile.sprite = testSprite;

                if (tile)
                {
                    print(tile.name);
                    highlightLayer.SetTile(tpos, newTile);
                }
            }
        }
    }
}