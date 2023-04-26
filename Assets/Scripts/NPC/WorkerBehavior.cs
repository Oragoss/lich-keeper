using Assets.Scripts.Manager;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.NPC
{
    public class WorkerBehavior : MonoBehaviour
    {
        [SerializeField] Tilemap wallLayer;
        [SerializeField] Tilemap highlightLayer;
        [SerializeField] float speed = 1f;
        
        Rigidbody2D rb2d;
        List<Vector3Int> mineableWalls;

        private PathFinder pathFinder;

        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
            mineableWalls = WallManager.wm.GetMineableWalls();
        }

        void FixedUpdate()
        {
            var velocity = speed * Time.fixedDeltaTime;
            var offset = 0.5f;
            mineableWalls = WallManager.wm.GetMineableWalls();

            //TODO: Find the closest mineable wall: transform.position - whatever
            if (mineableWalls.Count > 0)
            {
                Vector3Int movePoint = mineableWalls[0];
                var endNode = wallLayer.CellToWorld(movePoint);
                var workerPoint = wallLayer.WorldToCell(transform.position);
                var startNode = wallLayer.WorldToCell(workerPoint);
                //rb2d.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(endNode.x + offset, endNode.y + offset), velocity));

                //TODO: Generate the overlay tiles like in the tutorial and then everything else might fall into place
                //var path = pathFinder.FindPath(startNode, endNode);
            }
            else
            {
                Idle();
            }
        }

        private void Idle()
        {
            //TODO: Add idling behavior
        }
    }
}