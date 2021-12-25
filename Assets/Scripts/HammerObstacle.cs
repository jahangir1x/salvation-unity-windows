using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerObstacle : MonoBehaviour
{
    public bool canDealDamage = false;
    [SerializeField] private Transform hammerSmasherTransform;
    [SerializeField] private Vector3 positionToMove;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float waitAfterSmash = 2f;
    [SerializeField] private float waitAfterPull = 3f;
    [SerializeField] private bool isMovingToSmashPosition = true;

    private CameraShake cameraShake;
    private Vector3 pullPosition;
    private Vector3 smashPosition;

    private void Start()
    {
        cameraShake = GetComponent<CameraShake>();
        pullPosition = hammerSmasherTransform.position;                  // the platform would move from pos1 to pos2 and vice versa.
        StartCoroutine(StartSmashingHammerRoutine());
    }

    private void OnDrawGizmosSelected() // draw gizmo to see in the scene where the platform would move
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(smashPosition, new Vector3(3, 0.4f, 1)); // draw in pos2 when the game is running and pos2 is set.
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(smashPosition, new Vector3(3, 0.4f, 1)); // draw in pos2 when the game is running and pos2 is set.
        }
        else
        {
            Gizmos.DrawCube(hammerSmasherTransform.position + positionToMove, new Vector3(3, 0.4f, 1)); // draw from player position offset when the game is not running and pos2 is not set.
        }
    }

    IEnumerator StartSmashingHammerRoutine()
    {
        while (true)
        {
            smashPosition = pullPosition + positionToMove; // positionToMove is just an offset from player.
            if (isMovingToSmashPosition)
            {
                yield return SmashHammerRoutine();
            }
            else
            {
                yield return PullHammerRoutine();
            }
        }
    }

    IEnumerator PullHammerRoutine()
    {
        hammerSmasherTransform.position = Vector3.MoveTowards(hammerSmasherTransform.position, pullPosition, speed * Time.deltaTime);
        if (hammerSmasherTransform.position == pullPosition)
        {
            yield return new WaitForSeconds(waitAfterPull);
            canDealDamage = true;                  // hammer can only damage when it's on the way to smash
            isMovingToSmashPosition = true;
        }
        else
        {
            yield return null;
        }
    }

    IEnumerator SmashHammerRoutine()
    {
        hammerSmasherTransform.position = Vector3.MoveTowards(hammerSmasherTransform.position, smashPosition, speed * Time.deltaTime);
        if (hammerSmasherTransform.position == smashPosition) // if the platform reached pos2 the platform needs to move towards pos1
        {
            cameraShake.Shake();
            canDealDamage = false;                  // hammer can not damage when it's steady
            yield return new WaitForSeconds(waitAfterSmash);
            isMovingToSmashPosition = false;
        }
        else
        {
            yield return null;
        }
    }
}
