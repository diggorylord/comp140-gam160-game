using UnityEngine;
using System;
using System.IO.Ports;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
	//Public values for gravity and speed for turning and moving.

	public float speedMultiplier;
	public bool gotGravityPowerup = false;
	public float gravityValue = 10f;
	public float turnSpeed;
	public Image FuelBarFillImage;
	public float maxFuelAmount = 100f;
	public GameObject gravityPowerupImage;
	public string comPort = "COM5";

	private bool gravity = false;
	private Vector3 lookDirection;
	private Rigidbody carBody;
	private SerialPort serialPort;
	private float fuelUseModifier = 2f;
	private float speed = 10f;
	private int distance;

	// Use this for initialization
	void Start ()
	{
		carBody = GetComponent<Rigidbody> (); // Gets rigidbody component on this object.

		//Initialise the serial port
		serialPort = new SerialPort();
		//Using the following port
		serialPort.PortName = comPort;
		//Parity bits, this is used for error checking and must be agreed on by the device
		//and the host. In this case we don't use it!
		serialPort.Parity = Parity.None;
		//The Baud Rate is set to 9600, this is the default for mostn serial coms
		serialPort.BaudRate = 9600;
		//This is the length of bits communicated between the devices, the default is 8
		serialPort.DataBits = 8;
		//The stop bits in the data, per byte
		serialPort.StopBits = StopBits.One;

		//Open the port
		serialPort.Open();

	}
	
	// Update is called once per frame
	void Update ()
	{
		maxFuelAmount -= fuelUseModifier * Time.deltaTime;
		transform.position += transform.forward * speed * Time.deltaTime; // Moves the player continuously.
		lookDirection = new Vector3 (0f, -1f, 0f); // Sets the look direction for turning.
		FuelBarFillImage.fillAmount = maxFuelAmount / 100f;
		string data = serialPort.ReadLine();
		distance = int.Parse (data);
		Debug.Log (distance);

		if (distance >= 10 && distance < 15) 
		{
			speed = speed += 5f;; // Makes player go faster.
			fuelUseModifier = fuelUseModifier += 1f;
		}
		else 
		{
			speed = 10f;
			fuelUseModifier = 2f;
		}

		if (distance >= 20 && distance < 25) 
		{
			transform.RotateAround (transform.position, lookDirection, turnSpeed); // Turns player to the left.
		}

		if (distance >= 30 && distance < 35) 
		{
			transform.RotateAround (transform.position, -lookDirection, turnSpeed); // Turns player to the right.
		}
		// This if statement below is what allows gravity to be swapped.
		if (distance >= 40 && distance < 45) 
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

	//This called when the application quits (in editor and in standalone)
	void OnApplicationQuit()
	{
		//We must close the serial port
		serialPort.Close();
	}
}
