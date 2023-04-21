using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class NPCMovement : MonoBehaviour
    {
        Rigidbody2D rb2d;

        // Use this for initialization
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            var velocity = new Vector2(-1.7f, 0f);
            rb2d.MovePosition(rb2d.position + velocity * Time.fixedDeltaTime);
        }
    }
}