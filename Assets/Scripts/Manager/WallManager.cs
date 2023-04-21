using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager
{
    public class WallManager : MonoBehaviour
    {
        public static WallManager wm;

        [SerializeField] List<Vector3Int> highlightedWallPositions;

        private void Reset()
        {
        }

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

        internal bool CheckIfWallIsAlreadyHighlighted(Vector3Int point)
        {
            Vector3Int newPoint = highlightedWallPositions.Find(x => x == point);
            
            if(newPoint == Vector3Int.zero)
            {
                AddWallToHighlightedWalls(point);
                return false;
            }

            RemoveHighlightedWallPosition(point);
            return true;
        }

        internal void RemoveHighlightedWallPosition(Vector3Int point)
        {
            highlightedWallPositions.Remove(point);
        }

        private void AddWallToHighlightedWalls(Vector3Int point)
        {
            highlightedWallPositions.Add(point);
        }
    }
}