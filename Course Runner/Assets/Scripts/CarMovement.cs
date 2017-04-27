using UnityEngine;
using System;
using System.IO.Ports;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovement : MonoBehaviour
{
	//Public values for gravity and speed for turning and moving. includes stuff like the serialport string to work with the controller


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
		maxFuelAmount -= fuelUseModifier * Time.deltaTime; // This reduces the fuel amount during each second.
		transform.position += transform.forward * speed * Time.deltaTime; // Moves the player continuously.
		lookDirection = new Vector3 (0f, -1f, 0f); // Sets the look direction for turning.
		FuelBarFillImage.fillAmount = maxFuelAmount / 100f; // this is what reduces it visually on screen.
		string data = serialPort.ReadLine(); // This reads the serialport and puts the data it reads into a string.
		distance = int.Parse (data); // this converts that string of data into an integer so that it can be used within the code as a value.
		Debug.Log (distance); // this is just to check the distance read, so that I can debug any lag.

		if (distance >= 10 && distance < 15) //checks if the distance read is within thee values of 10 to 15. if so it increases the speed. if not it returns the speed to the original value.
		{
			speed = speed += 5f;; // Makes player go faster.
			fuelUseModifier = fuelUseModifier += 1f;
		}
		else 
		{
			speed = 10f;
			fuelUseModifier = 2f;
		}

		/* This part checks if the distance is within 20-25cm away from the sensor. if so it turns left.
		 * If the value is between 30-35cm away then it turns the player right */
		if (distance >= 20 && distance < 25) 
		{
			transform.RotateAround (transform.position, lookDirection, turnSpeed); // Turns player to the left.
		}

		if (distance >= 30 && distance < 35) 
		{
			transform.RotateAround (transform.position, -lookDirection, turnSpeed); // Turns player to the right.
		}

		// This if statement below is what allows gravity to be swapped.
		// It checks the distance value and if it is inbetween the values of 40-45 it swaps the gravity, provided you have the pickup.
		if (distance >= 40 && distance < 45) 
		{
			if (gotGravityPowerup == true)
			{
				ToggleGravity ();
				transform.RotateAround (transform.position, transform.forward, 180f); // flips the player upside down.
				gravityPowerupImage.SetActive (false); // removes the powerup from the player.
				gotGravityPowerup = false;
			}
		}
		 //resets the scene if they run of fuel.
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
