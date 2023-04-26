using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Player
{
    public class Dig : MonoBehaviour
    {
        [SerializeField] Tilemap wallTileMap;
        [SerializeField] Tilemap highlightLayer;

        //[SerializeField] Sprite emptySquare;    //This replaces the highlight sprite so the player can see again.

        //[HideInInspector] public List<Vector3Int> highlightedWalls;
        public LayerMask wallLayer;

        private void Start()
        {
        
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                DigOutWall();
            }
            //if (!Physics2D.OverlapCircle(transform.position + horizontal, 0.2f, stopsMovementLayer))
        }
               

        public void DigOutWall()
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3Int tpos = wallTileMap.WorldToCell(worldPoint);
            //var wallLayerTile = wallTileMap.GetTile(tpos);

            RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left);
            RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down);
            ////TODO: This works!!! Need to loop through all the surrounding tiles first though
            //var hitArray = Physics2D.OverlapCircleAll(transform.position, 0.2f, wallLayer);
            //foreach(var hits in hitArray)
            //{
            //    hits.IsTouchingLayers(wallLayer);
            //}

            //TODO: Maybe try to use overlapcircle?
            if (upHit.collider != null)
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = upHit.point.x - 0.01f * upHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = upHit.point.y - 0.01f * upHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                                                                               //var hitTile = wallTileMap.GetTile(hitTilePos);
                if (tpos == hitTilePos)
                {
                    //Adjacent Walls
                    DetermineSurroundingWallSprites(hitTilePos);

                    //Direct Wall
                    wallTileMap.SetTile(wallTileMap.WorldToCell(hitTilePos), null);
                }
            }

            if (rightHit.collider != null)
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = rightHit.point.x - 0.01f * rightHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = rightHit.point.y - 0.01f * rightHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                                                                               //var hitTile = wallTileMap.GetTile(hitTilePos);
                if (tpos == hitTilePos)
                {
                    //Adjacent Walls
                    DetermineSurroundingWallSprites(hitTilePos);

                    //Direct Wall
                    wallTileMap.SetTile(wallTileMap.WorldToCell(hitTilePos), null);
                }
            }

            if (leftHit.collider != null)
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = leftHit.point.x - 0.01f * leftHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = leftHit.point.y - 0.01f * leftHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                                                                               //var hitTile = wallTileMap.GetTile(hitTilePos);
                if (tpos == hitTilePos)
                {
                    //Adjacent Walls
                    DetermineSurroundingWallSprites(hitTilePos);

                    //Direct Wall
                    wallTileMap.SetTile(wallTileMap.WorldToCell(hitTilePos), null);
                }
            }

            if (downHit.collider != null)
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = downHit.point.x - 0.01f * downHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = downHit.point.y - 0.01f * downHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                                                                               //var hitTile = wallTileMap.GetTile(hitTilePos);
                if (tpos == hitTilePos)
                {
                    //Adjacent Walls
                    DetermineSurroundingWallSprites(hitTilePos);

                    //Direct Wall
                    wallTileMap.SetTile(wallTileMap.WorldToCell(hitTilePos), null);
                }
            }
        }

        private void DetermineSurroundingWallSprites(Vector3Int tpos)
        {
            //TODO: Do Diagaonal Directions!!
            Vector3Int upperTpos = new Vector3Int(tpos.x, tpos.y + 1, tpos.z);
            Vector3Int upperLeftTpos = new Vector3Int(tpos.x - 1, tpos.y + 1, tpos.z);
            Vector3Int upperRightTpos = new Vector3Int(tpos.x + 1, tpos.y + 1, tpos.z);
            Vector3Int rightTpos = new Vector3Int(tpos.x + 1, tpos.y, tpos.z);
            Vector3Int leftTpos = new Vector3Int(tpos.x - 1, tpos.y, tpos.z);
            Vector3Int lowerTpos = new Vector3Int(tpos.x, tpos.y - 1, tpos.z);
            Vector3Int lowerLeftTpos = new Vector3Int(tpos.x - 1, tpos.y - 1, tpos.z);
            Vector3Int lowerRightTpos = new Vector3Int(tpos.x + 1, tpos.y - 1, tpos.z);

            var upperLeftTile = wallTileMap.GetTile(upperLeftTpos);
            var upperRightTile = wallTileMap.GetTile(upperRightTpos);
            var upperTile = wallTileMap.GetTile(upperTpos);
            var rightTile = wallTileMap.GetTile(rightTpos);
            var leftTile = wallTileMap.GetTile(leftTpos);
            var lowerTile = wallTileMap.GetTile(lowerTpos);
            var lowerLeftTile = wallTileMap.GetTile(lowerLeftTpos);
            var lowerRightTile = wallTileMap.GetTile(lowerRightTpos);

            if (upperTile)
            {
                if (!upperRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.UpperRight].Sprite;
                    wallTileMap.SetTile(upperTpos, newTile);
                }
                if (upperRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Upper].Sprite;
                    wallTileMap.SetTile(upperTpos, newTile);
                }
                if (upperRightTile && rightTile && !leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Upper].Sprite;
                    wallTileMap.SetTile(upperTpos, newTile);

                    Tile newUpperRIghtTile = ScriptableObject.CreateInstance<Tile>();
                    newUpperRIghtTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.RightUpperRight].Sprite;
                    wallTileMap.SetTile(upperRightTpos, newUpperRIghtTile);
                }
                if (upperLeftTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.UpperLeft].Sprite;
                    wallTileMap.SetTile(upperLeftTpos, newTile);
                }
                if (!upperLeftTile && !leftTile && rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LeftUpperLeft].Sprite;
                    wallTileMap.SetTile(upperTpos, newTile);
                }
                if (leftTile && rightTile && !lowerTile)
                {
                    Tile newUpperTile = ScriptableObject.CreateInstance<Tile>();
                    newUpperTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Upper].Sprite;
                    wallTileMap.SetTile(upperTpos, newUpperTile);
                }
            }
            if (rightTile)
            {
                if (!leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Right].Sprite;
                    wallTileMap.SetTile(rightTpos, newTile);
                }
                if (!lowerRightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LeftUpperLeft].Sprite;
                    wallTileMap.SetTile(rightTpos, newTile);
                }
                if (lowerRightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Right].Sprite;
                    wallTileMap.SetTile(rightTpos, newTile);
                }
                if (upperRightTile && !lowerTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.RightUpperRight].Sprite;
                    wallTileMap.SetTile(upperRightTpos, newTile);
                }
                if (!upperRightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LeftLowerRight].Sprite;
                    wallTileMap.SetTile(rightTpos, newTile);
                }
                if (lowerRightTile && !upperTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.RightLowerRight].Sprite;
                    wallTileMap.SetTile(lowerRightTpos, newTile);
                }
            }
            if (leftTile)
            {
                if (!rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Left].Sprite;
                    wallTileMap.SetTile(leftTpos, newTile);
                }
                if (!lowerLeftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.UpperRight].Sprite;
                    wallTileMap.SetTile(leftTpos, newTile);
                }
                if (lowerLeftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Left].Sprite;
                    wallTileMap.SetTile(leftTpos, newTile);
                }
                if (upperLeftTile && !lowerTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.UpperLeft].Sprite;
                    wallTileMap.SetTile(upperLeftTpos, newTile);
                }
                if (!upperLeftTile && !upperTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LowerRight].Sprite;
                    wallTileMap.SetTile(leftTpos, newTile);
                }
                if (lowerLeftTile && !upperTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LowerLeft].Sprite;
                    wallTileMap.SetTile(lowerLeftTpos, newTile);
                }
            }
            if (lowerTile)
            {
                if (!lowerRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LowerRight].Sprite;
                    wallTileMap.SetTile(lowerTpos, newTile);
                }
                if (lowerRightTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Lower].Sprite;
                    wallTileMap.SetTile(lowerTpos, newTile);
                }
                if (lowerRightTile && !leftTile && rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Lower].Sprite;
                    wallTileMap.SetTile(lowerTpos, newTile);

                    Tile newRightLowerRight = ScriptableObject.CreateInstance<Tile>();
                    newRightLowerRight.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.RightLowerRight].Sprite;
                    wallTileMap.SetTile(lowerRightTpos, newRightLowerRight);
                }
                if (lowerLeftTile && !rightTile && leftTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LowerLeft].Sprite;
                    wallTileMap.SetTile(lowerLeftTpos, newTile);
                }
                if (!lowerLeftTile && !leftTile && rightTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.LeftLowerRight].Sprite;
                    wallTileMap.SetTile(lowerTpos, newTile);
                }
                if (lowerRightTile && lowerLeftTile && !upperTile)
                {
                    Tile newTile = ScriptableObject.CreateInstance<Tile>();
                    newTile.sprite = WallManager.wm.wallSprites[(int)WallManager.WallSprites.Lower].Sprite;
                    wallTileMap.SetTile(lowerTpos, newTile);
                }
            }
        }
    }
}
