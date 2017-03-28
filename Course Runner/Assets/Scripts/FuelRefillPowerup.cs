using UnityEngine;
using System.Collections;

public class FuelRefillPowerup : MonoBehaviour
{

	public GameObject playerCar;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			playerCar.GetComponent<CarMovement> ().maxFuelAmount += 40f;
			Destroy (gameObject);
		}
	}
}
