using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    //https://www.youtube.com/watch?v=mbzXIOKZurA
    public class PlayerControl : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public Transform movePoint;

        public LayerMask stopsMovementLayer;

        private void Start()
        {
            movePoint.parent = null;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, movePoint.position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, movePoint.position) <= 0.05f)
            {
                if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
                {
                    var horizontal = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                    if (!Physics2D.OverlapCircle(movePoint.position + horizontal, 0.2f, stopsMovementLayer))
                    {
                        movePoint.position += horizontal;
                    }
                }
                if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
                {
                    var vertical = new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                    if (!Physics2D.OverlapCircle(movePoint.position + vertical, 0.2f, stopsMovementLayer))
                    {
                        movePoint.position += vertical; 
                    }
                }
            }
        }
    }
}