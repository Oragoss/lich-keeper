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

            //Find the closest mineable wall
            if (mineableWalls.Count > 0)
            {
                Vector3Int movePoint = mineableWalls[0];
                var movePosition = wallLayer.CellToWorld(movePoint);
                rb2d.MovePosition(Vector2.MoveTowards(transform.position, new Vector2(movePosition.x + offset, movePosition.y + offset), velocity));
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