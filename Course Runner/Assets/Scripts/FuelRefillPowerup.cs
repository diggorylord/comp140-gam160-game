using UnityEngine;
using System.Collections;

public class FuelRefillPowerup : MonoBehaviour
{

	public GameObject playerCar;

	// refills the fuel guage in the game by a set amount then removes the object from the scene. only when the player collides with it though.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			playerCar.GetComponent<CarMovement> ().maxFuelAmount += 40f;
			Destroy (gameObject);
		}
	}
}
