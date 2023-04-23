﻿using Assets.Scripts.Manager;
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
        //[SerializeField] Sprite[] wallSprites;

        [HideInInspector] public List<Vector3Int> worldPoints;

        private void Start()
        {
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

                Vector3Int tpos = wallLayer.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
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

            if (upperTile)
            {
                if (!upperRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.UpperRight].Sprite;
                    wallLayer.SetTile(upperTpos, newTile);
                }
                if (upperRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Upper].Sprite;
                    wallLayer.SetTile(upperTpos, newTile);
                }
                if (upperRightTile && rightTile && !leftTile) {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Upper].Sprite;
                    wallLayer.SetTile(upperTpos, newTile);

                    Tile newUpperRIghtTile = ScriptableObject.CreateInstance<Tile>();
                    newUpperRIghtTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.RightUpperRight].Sprite;
                    wallLayer.SetTile(upperRightTpos, newUpperRIghtTile);
                }
                if (upperLeftTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.UpperLeft].Sprite;
                    wallLayer.SetTile(upperLeftTpos, newTile);
                }
                if (!upperLeftTile && !leftTile && rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LeftUpperLeft].Sprite;
                    wallLayer.SetTile(upperTpos, newTile);
                }
                
            }
            if (rightTile)
            {
                if (!leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Right].Sprite;
                    wallLayer.SetTile(rightTpos, newTile);
                }
            }
            if (leftTile)
            {
                if (!rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Left].Sprite;
                    wallLayer.SetTile(leftTpos, newTile);
                }
            }
            if (lowerTile)
            {
                if (!lowerRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LowerRight].Sprite;
                    wallLayer.SetTile(lowerTpos, newTile);
                }
                if (lowerRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Lower].Sprite;
                    wallLayer.SetTile(lowerTpos, newTile);
                }
                if (lowerRightTile && !leftTile && rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Lower].Sprite;
                    wallLayer.SetTile(lowerTpos, newTile);

                    Tile newRightLowerRight = ScriptableObject.CreateInstance<Tile>();
                    newRightLowerRight.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.RightLowerRight].Sprite;
                    wallLayer.SetTile(lowerRightTpos, newRightLowerRight);
                }
                if (lowerLeftTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LowerLeft].Sprite;
                    wallLayer.SetTile(lowerLeftTpos, newTile);
                }
                if (!lowerLeftTile && !leftTile && rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LeftLowerRight].Sprite;
                    wallLayer.SetTile(lowerTpos, newTile);
                }
            }
        }
    }
}