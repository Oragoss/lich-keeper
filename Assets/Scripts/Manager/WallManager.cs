using System.Collections.Generic;
using UnityEngine;

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