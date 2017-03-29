using UnityEngine;
using System.Collections;

public class GravityPowerup : MonoBehaviour
{
	// Variables for the gameobject.
	public GameObject car;
	CarMovement carScript;
	public GameObject gravityPowerupImage;

	void Start ()
	{
		carScript = car.GetComponent<CarMovement> (); // Grabs script on player.
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player") 
		{
			carScript.gotGravityPowerup = true; // Sets the gotgravitypowerup bool to be true. This allows the use of the powerup so gravity cant be swapped at any time.
			gravityPowerupImage.SetActive (true);
			Destroy (gameObject); // This just destroys the powerup on impact.
		}
	}
}
