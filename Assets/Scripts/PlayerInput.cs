// This script handles inputs for the player. It serves two main purposes: 1) wrap up
// inputs so swapping between mobile and standalone is simpler and 2) keeping inputs
// from Update() in sync with FixedUpdate()

using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

//We first ensure this script runs before all other player scripts to prevent laggy
//inputs
[DefaultExecutionOrder(-100)]
public class PlayerInput : MonoBehaviour
{
	[HideInInspector] public float horizontal;		//Float that stores horizontal input
	[HideInInspector] public bool jumpHeld;			//Bool that stores jump pressed
	[HideInInspector] public bool jumpPressed;		//Bool that stores jump held

	bool readyToClear;                              //Bool used to keep input in sync

	[Header("Shoot")]
	public Transform FirePos;
	public GameObject Bullet;
	public float shootSpeed;

	NormalPlayerMovement playerMovement;

	public float fireDelay = 0.5f;
	public bool fire = false;

	public bool canMove = true;

	private void Start()
	{

		playerMovement = GetComponent<NormalPlayerMovement>();
	}
	void Update()
	{
/*		if (Input.GetKeyDown(KeyCode.R))
			PlayerPrefs.DeleteAll();*/
		ClearInput();

		//If the Game Manager says the game is over, exit
		if (GameManagerRocky.IsGameOver())
			return;

		//Process keyboard, mouse, gamepad (etc) inputs
		if(canMove)
			ProcessInputs();

		//Clamp the horizontal input to be between -1 and 1
		horizontal = Mathf.Clamp(horizontal, -1f, 1f);

	}

	void FixedUpdate()
	{
		//In FixedUpdate() we set a flag that lets inputs to be cleared out during the 
		//next Update(). This ensures that all code gets to use the current inputs
		readyToClear = true;
	}

	public IEnumerator Fire()
	{
		fire = true;

		audio_Manager.instance.Play("bullet");
		
		GameObject bullet = Instantiate(Bullet, FirePos.position, Quaternion.identity);
		Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
		rb.AddForce(Vector2.right * playerMovement.direction * shootSpeed, ForceMode2D.Impulse);
		yield return new WaitForSeconds(fireDelay);
		fire = false;
	}

	void ClearInput()
	{
		//If we're not ready to clear input, exit
		if (!readyToClear)
			return;

		//Reset all inputs
		horizontal		= 0f;
		jumpPressed		= false;
		jumpHeld		= false;

		readyToClear	= false;
	}

	void ProcessInputs()
	{
		if (Input.GetKey(KeyCode.RightControl))
		{
			if (!fire)
			{
				StartCoroutine(Fire());
			}

		}

		//Accumulate horizontal axis input
		horizontal += Input.GetAxis("Horizontal");

		//Accumulate button inputs
		jumpPressed		= jumpPressed || Input.GetButtonDown("Jump");
		jumpHeld		= jumpHeld || Input.GetButton("Jump");
		//Fire = Fire || Input.GetButtonDown("Fire1");


	}

}
