using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class NPCMovement : MonoBehaviour
    {
        [SerializeField] float horizontalSpeed = 1.7f;
        [SerializeField] float verticalSpeed = 0;
        Rigidbody2D rb2d;

        // Use this for initialization
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var velocity = new Vector2(horizontalSpeed, verticalSpeed);
            rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
        }
    }
}