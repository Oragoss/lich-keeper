﻿using System;
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
            //highlightedWallPositions = new List<Vector2> {
            //    new Vector2(0.27f, 2.52f),
            //    new Vector2(-0.04f, 2.47f),
            //    new Vector2(-0.52f, 3.34f),
            //    new Vector2(-0.37f, 4.39f),
            //    new Vector2(-0.45f, 5.46f),
            //    new Vector2(-0.47f, 6.25f),
            //    new Vector2(0.55f, 3.60f),
            //    new Vector2(0.58f, 4.57f),
            //    new Vector2(0.58f, 5.36f),
            //    new Vector2(-0.52f, 3.34f),
            //    new Vector2(0.42f, 6.43f),
            //};
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

        internal bool AddOrRemoveNewHighlightedWalPosition(Vector3Int newPoint)
        {
            //TODO: check to see if a point has already been added and remove it
            highlightedWallPositions.Add(newPoint);
            print(highlightedWallPositions);
            return false;
        }
    }
}