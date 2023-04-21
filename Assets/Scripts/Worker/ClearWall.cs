using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Worker
{
    public class ClearWall : MonoBehaviour
    {
        [SerializeField] Tilemap wallLayer;
        [SerializeField] Tilemap highlightLayer;

        [SerializeField] Sprite emptySquare;    //This replaces the highlight sprite so the player can see again
        [SerializeField] Sprite upperWall;

        [HideInInspector]
        public List<Vector3Int> worldPoints;

        private void Start()
        {
            //TODO: Make a way to update this or have this called when highlighting new wall sections
            worldPoints = WallManager.wm.GetHighlightedWalls();
        }

        private void FixedUpdate()
        {
            worldPoints = WallManager.wm.GetHighlightedWalls();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            DigOutWall(collision);
        }

        private void DigOutWall(Collision2D collision)
        {
            //TODO: Detect which direction the worker is moving then select the correct wall sprites

            Vector3 hitPosition = Vector3.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x - 0.01f * hit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

                Vector3Int tpos = wallLayer.WorldToCell(hitPosition);
                Vector3Int highlightedPoint = worldPoints.Find(x => x == tpos);
                var wallLayerTile = wallLayer.GetTile(highlightedPoint);

                if (wallLayerTile)
                {
                    //Adjacent Walls
                    DetermineSurroundingWallSprites(highlightedPoint);

                    //Direct Wall
                    wallLayer.SetTile(wallLayer.WorldToCell(highlightedPoint), null);
                                        
                    //Highlight layer
                    Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
                    newHighlightLayerTile.sprite = emptySquare;
                    highlightLayer.SetTile(highlightLayer.WorldToCell(hitPosition), newHighlightLayerTile);
                }
            }
        }
    
        private void DetermineSurroundingWallSprites(Vector3Int tpos)
        {
            //TODO: Gather information about the 8 surrounding tiles to determine which sprite should be used.
            Vector3Int upperTpos = new Vector3Int(tpos.x, tpos.y + 1, tpos.z);
            Tile newTile = ScriptableObject.CreateInstance<Tile>();
            newTile.sprite = upperWall;
            var otherTile = wallLayer.GetTile(new Vector3Int(tpos.x, tpos.y + 1, tpos.z));
            if(otherTile)
                wallLayer.SetTile(upperTpos, newTile);
        }
    }
}