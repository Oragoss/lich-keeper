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

        private void Update()
        {
            Highlight();
            Dehighlight();
        }

        private void Highlight()
        {
            if (Input.GetMouseButton(0))
            {
                Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                Vector3Int tpos = wallLayer.WorldToCell(worldPoint);
                bool alreadyHighlighted = AddWallPosition(tpos);
                
                if(!alreadyHighlighted)
                {
                    //TODO: Move everything in here
                }
                //var roundedPoint = WallManager.wm.RoundUpWorldPoints(tpos);

                // Try to get a tile from cell position
                //var highlightLayerTile = highlightLayer.GetTile(tpos);
                Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
                newHighlightLayerTile.sprite = highlightSprite;

                var wallLayerTile = wallLayer.GetTile(tpos);

                if (wallLayerTile)
                {
                    highlightLayer.SetTile(tpos, newHighlightLayerTile);
                }
            }
        }

        private void Dehighlight()
        {
            //TODO: Check to see if a Vector 2 has already been stored.
        }

        private bool AddWallPosition(Vector3Int point)
        {
            return WallManager.wm.AddOrRemoveNewHighlightedWalPosition(point);
        }

        private void RemoveWallPosition(Vector2 point)
        {
            //TODO: Going to need to clean up how the vectors are stored so I can remove the correct ones.
        }
    }
}