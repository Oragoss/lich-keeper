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

        [SerializeField] Sprite floorSprite;
        [SerializeField] Sprite[] wallSprites;

        [HideInInspector]
        public List<Vector2> worldPoints;

        private void Awake()
        {
            //TODO: Make a way to update this or have this called when highlighting new wall sections
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
                foreach (var worldPoint in worldPoints)
                {
                    Vector3Int tpos = wallLayer.WorldToCell(worldPoint);
                    var wallLayerTile = wallLayer.GetTile(tpos);
                    if (wallLayerTile)
                    {
                        hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
                        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
                        wallLayer.SetTile(wallLayer.WorldToCell(hitPosition), null);
                    }
                }
            }
        }
    }
}