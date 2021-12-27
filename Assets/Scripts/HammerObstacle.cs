using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerObstacle : MonoBehaviour
{
    public bool canDealDamage = true;
    [SerializeField] private Transform hammerSmasherTransform;
    [SerializeField] private Vector3 positionToMove;
    [SerializeField] private float totalDurationToSmash = 1f;
    [SerializeField] private float waitBeforeFirstSmash = 0f;
    [SerializeField] private float waitAfterSmash = 2f;
    [SerializeField] private float waitAfterPull = 3f;
    [SerializeField] private bool isMovingToSmashPosition = true;

    private bool isWaitedFirstTime = false;
    // private CameraShake cameraShake;
    private Vector3 pullPosition;
    private Vector3 smashPosition;

    private void Start()
    {
        // cameraShake = GetComponent<CameraShake>();
        pullPosition = hammerSmasherTransform.position;                  // the platform would move from pos1 to pos2 and vice versa.
        StartCoroutine(StartSmashingHammerRoutine());
    }

    private void OnDrawGizmosSelected() // draw gizmo to see in the scene where the platform would move
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(smashPosition - Vector3.up * 3f, new Vector3(3, 0.4f, 1)); // draw in pos2 when the game is running and pos2 is set.
        if (Application.isPlaying)
        {
            Gizmos.DrawCube(smashPosition - Vector3.up * 3f, new Vector3(3, 0.4f, 1)); // draw in pos2 when the game is running and pos2 is set.
        }
        else
        {
            Gizmos.DrawCube(hammerSmasherTransform.position + positionToMove - Vector3.up * 3f, new Vector3(3, 0.4f, 1)); // draw from player position offset when the game is not running and pos2 is not set.
        }
    }

    IEnumerator StartSmashingHammerRoutine()
    {
        while (true)
        {
            smashPosition = pullPosition + positionToMove; // positionToMove is just an offset from player.
            if (isMovingToSmashPosition)
            {
                if (isWaitedFirstTime == false)
                {
                    yield return new WaitForSeconds(waitBeforeFirstSmash);
                    isWaitedFirstTime = true;
                }
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
        // hammerSmasherTransform.position = Vector3.MoveTowards(hammerSmasherTransform.position, pullPosition, totalDurationToSmash * Time.deltaTime);
        // if (hammerSmasherTransform.position == pullPosition)
        // {
        //     yield return new WaitForSeconds(waitAfterPull);
        //     canDealDamage = true;                  // hammer can only damage when it's on the way to smash
        //     isMovingToSmashPosition = true;
        // }
        // else
        // {
        //     yield return null;
        // }

        float time = 0f;
        Vector2 startPosition = hammerSmasherTransform.position;

        while (time < totalDurationToSmash)
        {
            hammerSmasherTransform.position = Vector2.Lerp(startPosition, pullPosition, time / totalDurationToSmash);
            time += Time.deltaTime;
            yield return null;
        }
        // canDealDamage = true;
        isMovingToSmashPosition = true;
        hammerSmasherTransform.position = pullPosition;
        yield return new WaitForSeconds(waitAfterPull);

    }

    IEnumerator SmashHammerRoutine()
    {
        // hammerSmasherTransform.position = Vector3.MoveTowards(hammerSmasherTransform.position, smashPosition, totalDurationToSmash * Time.deltaTime);
        // if (hammerSmasherTransform.position == smashPosition) // if the platform reached pos2 the platform needs to move towards pos1
        // {
        //     cameraShake.Shake();
        //     canDealDamage = false;                  // hammer can not damage when it's steady
        //     yield return new WaitForSeconds(waitAfterSmash);
        //     isMovingToSmashPosition = false;
        // }
        // else
        // {
        //     yield return null;
        // }

        float time = 0f;
        Vector2 startPosition = hammerSmasherTransform.position;

        while (time < totalDurationToSmash)
        {
            hammerSmasherTransform.position = Vector2.Lerp(startPosition, smashPosition, time / totalDurationToSmash);
            time += Time.deltaTime;
            yield return null;
        }
        // cameraShake.Shake();
        // canDealDamage = false;
        isMovingToSmashPosition = false;
        hammerSmasherTransform.position = smashPosition;
        yield return new WaitForSeconds(waitAfterPull);

    }
}
