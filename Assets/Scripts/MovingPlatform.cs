using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Vector3 positionToMove;
    [SerializeField] private float speed = 10f;

    private Vector3 pos1;
    private Vector3 pos2;
    private bool isMoving1to2 = true;

    private void Start()
    {
        pos1 = transform.position;                  // the platform would move from pos1 to pos2 and vice versa.
        pos2 = transform.position + positionToMove; // positionToMove is just an offset from player.
    }

    private void OnDrawGizmosSelected() // draw gizmo to see in the scene where the platform would move
    {
        Gizmos.color = Color.red;
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(pos2, new Vector3(3, 0.4f, 1)); // draw in pos2 when the game is running and pos2 is set.
        }
        else
        {
            Gizmos.DrawCube(transform.position + positionToMove, new Vector3(3, 0.4f, 1)); // draw from player position offset when the game is not running and pos2 is not set.
        }
    }

    private void FixedUpdate()
    {
        if (isMoving1to2)   // check whether platform is moving from pos1 to pos2 or pos2 to pos1
        {
            transform.position = Vector3.MoveTowards(transform.position, pos2, speed * Time.deltaTime);
            if (transform.position == pos2) // if the platform reached pos2 the platform needs to move towards pos1
            {
                isMoving1to2 = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pos1, speed * Time.deltaTime);
            if (transform.position == pos1)
            {
                isMoving1to2 = true;
            }
        }
    }

}
