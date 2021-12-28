using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstacle : MonoBehaviour
{
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private float cameraAngularSpeed = 0.7f;
    [SerializeField] private float cameraIntruderChaseSpeed = 3f;
    [SerializeField] private float bulletSpeed = 3f;
    [SerializeField] private float shootOffset = 10f;
    [SerializeField] private float gunRotationSpeed = 10f;

    private Animator cameraAnimator;
    private float shootInterval;
    private float timeElapsedSinceLastShooting = 0f;
    private float shootingAngle;
    private bool shouldRotateClockwise = false;
    private bool shootingAngleSet = false;


    private void Start()
    {
        cameraAnimator = transform.GetComponent<Animator>();
        shootInterval = UnityEngine.Random.Range(0.2f, 3f);
    }

    private void Update()
    {

        fieldOfView.SetOrigin(rayOrigin.position);
        fieldOfView.SetAimDirection((rayOrigin.right * (-1)));
        if (fieldOfView.foundPlayer)
        {
            cameraAnimator.SetBool(GameManagerRocky.PlayerDetectedAnimBoolID, true);

            ChasePlayer();
            ShootThePlayer();
        }
        else
        {
            cameraAnimator.SetBool(GameManagerRocky.PlayerDetectedAnimBoolID, false);
            RotateCamera();
        }
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.transform.CompareTag("Player"))
    //    {

    //        transform.GetComponent<Animator>().Play("Default");
    //        isPlayerVisible = false;
    //    }
    //}

    private void RotateCamera()
    {
        var finalRotation = Quaternion.identity;
        if (!shouldRotateClockwise)
        {
            finalRotation = Quaternion.Euler(0, 0, 180f);
        }
        else
        {
            finalRotation = Quaternion.Euler(0, 0, 0f);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, finalRotation, cameraAngularSpeed * Time.deltaTime);
        if (transform.rotation.eulerAngles.z >= 170f)
        {
            shouldRotateClockwise = true;
        }
        else if (transform.rotation.eulerAngles.z <= 10f)
        {
            shouldRotateClockwise = false;
        }
    }

    private void ChasePlayer()
    {
        var direction = GameManagerRocky.PlayerTransform.position - rayOrigin.transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 180f;

        if (angle > 10f && angle < 170f)
        {
            transform.rotation = Quaternion.Slerp(
                                                                    transform.rotation,
                                                                    Quaternion.Euler(0, 0, angle),
                                                                    cameraIntruderChaseSpeed * Time.deltaTime);

        }
    }

    private void ShootThePlayer()
    {
        timeElapsedSinceLastShooting += Time.deltaTime;

        if (!shootingAngleSet)
        {
            var dir = GameManagerRocky.PlayerTransform.position - gun.transform.position;
            shootingAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f + UnityEngine.Random.Range(-shootOffset, shootOffset);
            // Debug.Log("shooting angle: " + shootingAngle);
            shootingAngleSet = true;
        }
        else
        {
            gun.transform.rotation = Quaternion.Slerp(gun.transform.rotation,
                                                  Quaternion.Euler(0, 0, shootingAngle),
                                                  Time.deltaTime * gunRotationSpeed);
        }


        if (shootInterval < timeElapsedSinceLastShooting)
        {
            shootingAngleSet = false;
            // var bullet = Instantiate(bulletPrefab, bulletOrigin.transform.position, Quaternion.identity);
            var bullet = ObjectPooler.Instance.SpawnFromPool(Constants.CAMERA_BULLET, bulletOrigin.transform.position, Quaternion.identity);
            var direction = bulletOrigin.transform.right * (-1);
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.eulerAngles = Vector3.forward * angle;
            bullet.GetComponent<Rigidbody2D>().AddForce((direction)
                                                       * bulletSpeed, ForceMode2D.Impulse);

            timeElapsedSinceLastShooting = 0f;
            shootInterval = UnityEngine.Random.Range(0.2f, 2f);
        }




    }




}
