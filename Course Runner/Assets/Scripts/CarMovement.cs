using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
	//Public values for gravity and speed for turning and moving.
	public float speed = 10f;
	public float speedMultiplier;
	public bool gotGravityPowerup = false;
	public float gravityValue = 10f;
	public float turnSpeed;
	public float fuelUseModifier;
	public Image FuelBarFillImage;
	public float maxFuelAmount = 100f;
	public GameObject gravityPowerupImage;

	private bool gravity = false;
	private Vector3 lookDirection;
	private Rigidbody carBody;

	// Use this for initialization
	void Start ()
	{
		carBody = GetComponent<Rigidbody> (); // Gets rigidbody component on this object.
	}
	
	// Update is called once per frame
	void Update ()
	{
		maxFuelAmount -= fuelUseModifier * Time.deltaTime;
		transform.position += transform.forward * speed * Time.deltaTime; // Moves the player continuously.
		lookDirection = new Vector3 (0f, -1f, 0f); // Sets the look direction for turning.
		FuelBarFillImage.fillAmount = maxFuelAmount / 100f;

		// Movement controls below.
		if (Input.GetKeyDown (KeyCode.W)) 
		{
			speed = speed * speedMultiplier; // Makes player go faster.
			fuelUseModifier = fuelUseModifier * 5f;
		}
		if (Input.GetKeyUp(KeyCode.W)) 
		{
			speed = speed/speedMultiplier; // Slows player down.
			fuelUseModifier = fuelUseModifier / 5f;
		}

		if (Input.GetKey (KeyCode.A))
		{
			transform.RotateAround (transform.position, lookDirection, turnSpeed); // Turns player to the left.
		}

		if (Input.GetKey (KeyCode.D))
		{
			transform.RotateAround (transform.position, -lookDirection, turnSpeed); // Turns player to the right.
		}
		// This if statement below is what allows gravity to be swapped.
		if (Input.GetKeyDown (KeyCode.E))
		{
			if (gotGravityPowerup == true)
			{
				ToggleGravity ();
				transform.RotateAround (transform.position, transform.forward, 180f);
				gravityPowerupImage.SetActive (false);
				gotGravityPowerup = false;
			}
		}

		if (maxFuelAmount <= 0) 
		{
			string currentLevel = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (currentLevel);
		}
	}

	void ToggleGravity ()
	{
		gravity = !gravity; // Gravity bool toggle. Allows gravity bool to be set to true or false using the E key above.
	}

	void FixedUpdate ()
	{
		// This here sets the gravity to be pushing down on the player if gravity direction is true. Like normal gravity on earth.
		if (!gravity) 
		{
			carBody.AddForceAtPosition (Vector3.down * gravityValue, transform.position);
		}

		// This, however, sets the gravitydirection to false, meaning it pushes on the player from the bottom so that you can walk on the ceiling.
		// I wanted to include this fun mechanic to test what it would be like to change where youre looking from on the arena.
		if (gravity) 
		{
			carBody.AddForceAtPosition (Vector3.up * gravityValue, transform.position);
		}
	}
}
