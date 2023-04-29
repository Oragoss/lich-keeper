using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] float speed = 50f;
        [SerializeField] float decisionDelay = 15f;
        [SerializeField] LayerMask enemyLayer;

        int horizontalMovement = 0;
        int verticallMovement = 0;

        Vector2 movement;
        Rigidbody2D rb;
        
        enum MoveDirection
        {
            Up = 1,
            Right,
            Left,
            Down
        }

        enum Behavior
        {
            Wander,
            Idle,
            Aggression,
            Interacting
        }

        int behavior;

        int lastDirection = -1;
        List<int> directions = new List<int>();

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            InvokeRepeating("Wander", 0, decisionDelay);
        }

        private void FixedUpdate()
        {

            //Aggro();
            ////Idle();
            //InteractWithDungeonItem();
            movement = new Vector2(horizontalMovement * speed, verticallMovement * speed);

            movement *= Time.deltaTime;
            rb.velocity = movement;
        }

        private void InteractWithDungeonItem()
        {
            
        }

        private void Idle()
        {
            print("Idling,,,");
            horizontalMovement = 0;
            verticallMovement = 0;
        }

        private void Aggro()
        {
            
        }

        //TODO: Oncollision stop this coroutine and start it up again
        //TODO: OnAggro stop this coroutine and start the agrro one
        private void Wander()
        {
            RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, 1f, ~enemyLayer);
            Debug.DrawLine(transform.position, Vector2.up, Color.red);
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, 1f, ~enemyLayer);
            Debug.DrawLine(transform.position, Vector2.right, Color.red);
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, 1f, ~enemyLayer);
            Debug.DrawLine(transform.position, Vector2.left, Color.red);
            RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down, 1f, ~enemyLayer);
            Debug.DrawLine(transform.position, Vector2.down, Color.red);

            #region DetectWalls
            if (upHit.collider && upHit.collider.gameObject.CompareTag("WallTile"))
            {
                var direction = directions.Find(x => x == (int)MoveDirection.Up);
                if (direction == 0)
                    directions.Remove(direction);
            }
            else
            {
                directions.Add((int)MoveDirection.Up);
            }

            if (rightHit.collider && rightHit.collider.gameObject.CompareTag("WallTile"))
            {
                var direction = directions.Find(x => x == (int)MoveDirection.Right);
                if (direction == 0)
                    directions.Remove(direction);
            }
            else
            {
                directions.Add((int)MoveDirection.Right);
            }

            if (leftHit.collider && leftHit.collider.gameObject.CompareTag("WallTile"))
            {
                var direction = directions.Find(x => x == (int)MoveDirection.Left);
                if (direction == 0)
                    directions.Remove(direction);
            }
            else
            {
                directions.Add((int)MoveDirection.Left);
            }

            if (downHit.collider && downHit.collider.gameObject.CompareTag("WallTile"))
            {
                var direction = directions.Find(x => x == (int)MoveDirection.Down);
                if (direction == 0)
                    directions.Remove(direction);
            }
            else
            {
                directions.Add((int)MoveDirection.Down);
            }
            #endregion

            //If the enemy chooses a direction it can't move in, it chooses the Idle action instead
            //We want to choose a random direction from the directions list. This way we can always choose a good direction to wander

            System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
            int lastDirectionTravelled;
            if (lastDirection >= 0)
            {
                //TODO: DOES THIS WORK?!?!?!? It should remove based on value not index!!
                lastDirectionTravelled = directions.Find(x => x == lastDirection);
                directions.Remove(lastDirectionTravelled);
            }

            int pickedDirection = directions[rand.Next(directions.Count)];
            print("Picked direction:" + " " + pickedDirection);

            switch (pickedDirection)
            {
                case (int)MoveDirection.Up:
                    horizontalMovement = 0;
                    verticallMovement = 1;                    
                    lastDirection = (int)MoveDirection.Down; //Store the last direction enemy came from
                    break;
                case (int)MoveDirection.Right:
                    horizontalMovement = 1;
                    verticallMovement = 0;
                    lastDirection = (int)MoveDirection.Left; //Store the last direction enemy came from
                    break;
                case (int)MoveDirection.Left:
                    horizontalMovement = -1;
                    verticallMovement = 0;
                    lastDirection = (int)MoveDirection.Right; //Store the last direction enemy came from
                    break;
                case (int)MoveDirection.Down:
                    horizontalMovement = 0;
                    verticallMovement = -1;
                    lastDirection = (int)MoveDirection.Up; //Store the last direction enemy came from
                    break;
                default:
                    Idle();
                    break;
            }

            //yield return new WaitForSeconds(decisionDelay);
        }
    }
}