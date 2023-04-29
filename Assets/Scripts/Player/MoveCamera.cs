using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    //TODO: Create boundaries so the player can't go infinitely in one direction
    public class MoveCamera : MonoBehaviour
    {
        [SerializeField] float movementSpeed = 10;

        private void Update()
        {
            //get the Input from Horizontal axis
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            //get the Input from Vertical axis
            float verticalInput = Input.GetAxisRaw("Vertical");

            //update the position
            transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, 0);
        }
    }
}