using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Manager
{
    public class WallManager : MonoBehaviour
    {
        public static WallManager wm;

        [SerializeField] List<Vector3Int> highlightedWallPositions;
        [SerializeField] List<Vector3Int> mineableWalls;
        public WallTypes[] wallSprites;

        [HideInInspector] public enum WallSprites { Upper, UpperLeft, UpperRight, Left, Right, Lower, LowerLeft, LowerRight, LeftUpperLeft, LeftLowerRight, RightUpperRight, RightLowerRight };

        private void Awake()
        {
            if (wm == null)
            {
                DontDestroyOnLoad(gameObject);
                wm = this;
            }
            else if (wm != this)
            {
                Destroy(gameObject);
            }
        }

        internal List<Vector3Int> GetHighlightedWalls()
        {
            return highlightedWallPositions;
        }

        internal List<Vector3Int> GetMineableWalls()
        {
            return mineableWalls;
        }

        internal bool CheckIfWallIsAlreadyHighlighted(Vector3Int point)
        {
            Vector3Int newPoint = highlightedWallPositions.Find(x => x == point);
            
            if(newPoint == Vector3Int.zero)
            {
                return false;
            }
            return true;
        }

        internal void CheckToSeeIfWallIsMineable(Vector3Int tpos, Tilemap tilemap)
        {
            Vector3Int upperTpos = new Vector3Int(tpos.x, tpos.y + 1, tpos.z);
            Vector3Int upperLeftTpos = new Vector3Int(tpos.x - 1, tpos.y + 1, tpos.z);
            Vector3Int upperRightTpos = new Vector3Int(tpos.x + 1, tpos.y + 1, tpos.z);
            Vector3Int rightTpos = new Vector3Int(tpos.x + 1, tpos.y, tpos.z);
            Vector3Int leftTpos = new Vector3Int(tpos.x - 1, tpos.y, tpos.z);
            Vector3Int lowerTpos = new Vector3Int(tpos.x, tpos.y - 1, tpos.z);
            Vector3Int lowerLeftTpos = new Vector3Int(tpos.x - 1, tpos.y - 1, tpos.z);
            Vector3Int lowerRightTpos = new Vector3Int(tpos.x + 1, tpos.y - 1, tpos.z);

            var upperLeftTile = tilemap.GetTile(upperLeftTpos);
            var upperRightTile = tilemap.GetTile(upperRightTpos);
            var upperTile = tilemap.GetTile(upperTpos);
            var rightTile = tilemap.GetTile(rightTpos);
            var leftTile = tilemap.GetTile(leftTpos);
            var lowerTile = tilemap.GetTile(lowerTpos);
            var lowerLeftTile = tilemap.GetTile(lowerLeftTpos);
            var lowerRightTile = tilemap.GetTile(lowerRightTpos);

            if (!upperLeftTile || !upperRightTile || !upperTile || !rightTile || !leftTile || !lowerTile || !lowerLeftTile || !lowerRightTile)
            {
                AddMineableWalls(tpos);
            }
        }

        internal void RemoveHighlightedWallPosition(Vector3Int point)
        {
            highlightedWallPositions.Remove(point);
        }

        internal void AddWallToHighlightedWalls(Vector3Int point)
        {
            highlightedWallPositions.Add(point);
        }

        internal void RemoveMineableWalls(Vector3Int point)
        {
            mineableWalls.Remove(point);
        }

        internal void AddMineableWalls(Vector3Int point)
        {
            mineableWalls.Add(point);
        }
    }
}