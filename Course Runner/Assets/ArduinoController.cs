using UnityEngine;
//Using statements for Serial Port
using System.IO.Ports;
using System.Collections;
//System is also needed
using System;

public class ArduinoController : MonoBehaviour {

	//Public string for changing the port, we could enumrate and the Ports
	//but this is fine for an example
	public string COMPort = "COM4";
	public GameObject cube;
	public float timeToStart;
	public float timeInbetweenFrames;
	public int speed;

	//Serial Port, this will hold the instance of the serial Port
	//This is the main way to communicate with the serial device
	SerialPort serialPort;


	// Use this for initialization
	void Start()
	{
		//Initialise the serial port
		serialPort = new SerialPort();
		//Using the following port
		serialPort.PortName = COMPort;
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

		//InvokeRepeating ("ArduinoTest", 0.1f, 1f);

	}

	// Update is called once per frame
	void Update()
	{
		string data = serialPort.ReadLine();
		int distance = int.Parse (data);

		if (distance >= 10 && distance < 30) 
		{
			cube.GetComponent<Rigidbody> ().AddForce (Vector3.up * speed);
		}
		Debug.Log (distance);
	}

	void ArduinoTest()
	{
		
	}

	//This called when the application quits (in editor and in standalone)
	void OnApplicationQuit()
	{
		//We must close the serial port
		serialPort.Close();
	}
}
