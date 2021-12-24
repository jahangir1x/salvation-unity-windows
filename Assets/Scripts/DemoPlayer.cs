using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoPlayer : MonoBehaviour
{
    public float speed;
    public bool playerIsInElevator = false;

    private Animator playerAnimator;
    private Rigidbody2D rb;
    private Vector2 movePos = Vector3.zero;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        movePos.x = Input.GetAxisRaw("Horizontal");
        movePos.y = Input.GetAxisRaw("Vertical");
    }

    // public void ShowSpawnAnimation()
    // {
    //     // playerAnimator.Play(GameManager.PlayerSpawnAnimID); // show some spawn animation. currently it has a basic animation.
    // }

    private void FixedUpdate()
    {
        rb.velocity = movePos * speed;
    }
}
