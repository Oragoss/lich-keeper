using Assets.Scripts.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.NPC
{
    public class ClearWall : MonoBehaviour
    {
        [SerializeField] float animationDelayTime = 2;
        [SerializeField] Tilemap wallTileMap;
        [SerializeField] Tilemap highlightLayer;

        [SerializeField] Sprite emptySquare;    //This replaces the highlight sprite so the player can see again.

        [HideInInspector] public List<Vector3Int> highlightedWalls;
        [HideInInspector] public List<Vector3Int> mineableWalls;

        private void Start()
        {
            highlightedWalls = WallManager.wm.GetHighlightedWalls();
            mineableWalls = WallManager.wm.GetMineableWalls();
        }

        private void FixedUpdate()
        {
            highlightedWalls = WallManager.wm.GetHighlightedWalls();
            mineableWalls = WallManager.wm.GetMineableWalls();

            DigOutWall();
        }

       
        public void DigOutWall()
        {
            RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, 0.5f);
            Debug.DrawLine(transform.position, Vector2.up, Color.red);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 0.5f);
            Debug.DrawLine(transform.position, Vector2.right, Color.red);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 0.5f);
            Debug.DrawLine(transform.position, Vector2.left, Color.red);
            RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
            Debug.DrawLine(transform.position, Vector2.down, Color.red);

            if (upHit.collider && upHit.collider.gameObject.CompareTag("WallTile"))
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = upHit.point.x - 0.01f * upHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = upHit.point.y - 0.01f * upHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                var wallPos = mineableWalls.Find(x => x == hitTilePos);
                if (wallPos != Vector3Int.zero)
                {
                    StartCoroutine(Dig(wallPos));
                }
            }

            else if (rightHit.collider && rightHit.collider.gameObject.CompareTag("WallTile"))
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = rightHit.point.x - 0.01f * rightHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = rightHit.point.y - 0.01f * rightHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                var wallPos = mineableWalls.Find(x => x == hitTilePos);
                if (wallPos != Vector3Int.zero)
                {
                    StartCoroutine(Dig(wallPos));
                }
            }

            else if (leftHit.collider && leftHit.collider.gameObject.CompareTag("WallTile"))
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = leftHit.point.x - 0.01f * leftHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = leftHit.point.y - 0.01f * leftHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                var wallPos = mineableWalls.Find(x => x == hitTilePos);
                if (wallPos != Vector3Int.zero)
                {
                    StartCoroutine(Dig(wallPos));
                }
            }

            else if (downHit.collider && downHit.collider.gameObject.CompareTag("WallTile"))
            {
                Vector3 hitPosition = Vector3.zero;
                hitPosition.x = downHit.point.x - 0.01f * downHit.normal.x; //Doing this is absolutely necessary
                hitPosition.y = downHit.point.y - 0.01f * downHit.normal.y;
                Vector3Int hitTilePos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
                var wallPos = mineableWalls.Find(x => x == hitTilePos);
                if (wallPos != Vector3Int.zero)
                {
                    StartCoroutine(Dig(wallPos));
                }
            }
        }

        //private void DigOutWall(Collision2D collision)
        //{
        //    Vector3 hitPosition = Vector3.zero;
        //    //TODO: Don't do this on collisions, shoot a raycast in one of 4 directions and then if the tile position it hits is the same as the mineable tile, mine it out
        //    foreach (ContactPoint2D hit in collision.contacts)
        //    {
        //        hitPosition.x = hit.point.x - 0.01f * hit.normal.x; //Doing this is absolutely necessary
        //        hitPosition.y = hit.point.y - 0.01f * hit.normal.y;

        //        Vector3Int tpos = wallTileMap.WorldToCell(hitPosition);  //Double check to make sure it can grab any stored position from the list
        //        Vector3Int highlightedPoint = highlightedWalls.Find(x => x == tpos);
        //        var wallLayerTile = wallTileMap.GetTile(highlightedPoint);

        //        if (wallLayerTile)
        //        {
        //            //Adjacent Walls
        //            DetermineSurroundingWallSprites(highlightedPoint);

        //            //Direct Wall
        //            wallTileMap.SetTile(wallTileMap.WorldToCell(highlightedPoint), null);

        //            //Highlight layer
        //            Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
        //            newHighlightLayerTile.sprite = emptySquare;
        //            highlightLayer.SetTile(highlightLayer.WorldToCell(hitPosition), newHighlightLayerTile);

        //            //Mineable Wall
        //            //TODO: When changing directions it will bump into a wall and add too many mineable walls
        //            WallManager.wm.RemoveMineableWalls(tpos);
        //            WallManager.wm.RemoveHighlightedWallPosition(tpos);
        //            foreach (var newTpos in highlightedWalls)
        //                WallManager.wm.CheckToSeeIfWallIsMineable(newTpos, wallTileMap);
        //        }
        //    }
        //}

        private IEnumerator Dig(Vector3Int tpos)
        {
            GetComponent<WorkerBehavior>().isDigging = true;
            //TODO: Add animation
            yield return new WaitForSeconds(animationDelayTime);

            //Adjacent Walls
            DetermineSurroundingWallSprites(tpos);
                        
            //Direct Wall
            wallTileMap.SetTile(wallTileMap.WorldToCell(tpos), null);

            //Highlight layer
            Tile newHighlightLayerTile = ScriptableObject.CreateInstance<Tile>();
            newHighlightLayerTile.sprite = emptySquare;
            highlightLayer.SetTile(highlightLayer.WorldToCell(tpos), newHighlightLayerTile);

            //Mineable Wall
            WallManager.wm.RemoveMineableWalls(tpos);
            WallManager.wm.RemoveHighlightedWallPosition(tpos);
            foreach (var newTpos in highlightedWalls)
                WallManager.wm.CheckToSeeIfWallIsMineable(newTpos, wallTileMap);

            GetComponent<WorkerBehavior>().isDigging = false;
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