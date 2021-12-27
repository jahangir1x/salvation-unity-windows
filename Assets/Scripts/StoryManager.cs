using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    
    [SerializeField] Animator playerAnimator;
    [SerializeField] Animator shadowPlayerAnimator;
    AudioSource audioSource;

    bool movePlayer = false;
    public float moveSpeed = 10f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); 
    }
    private void Update()
    {
        if (movePlayer)
        {
            playerAnimator.gameObject.transform.position = new Vector2(playerAnimator.gameObject.transform.position.x + moveSpeed * Time.deltaTime,
                playerAnimator.gameObject.transform.position.y);

            shadowPlayerAnimator.gameObject.transform.position = new Vector2(shadowPlayerAnimator.gameObject.transform.position.x + moveSpeed * Time.deltaTime,
                shadowPlayerAnimator.gameObject.transform.position.y);
        }
    }
    public void GoToPortal()
    {
        playerAnimator.Play("walk");
        shadowPlayerAnimator.Play("walk");

        movePlayer = true;

    }

    public void ShakeScrren()
    {
        CinemachineShake.Instance.ShakeCamera(2f , 1f);
    }

    public void PlayAudio()
    {
        audioSource.Play();
    }


}
