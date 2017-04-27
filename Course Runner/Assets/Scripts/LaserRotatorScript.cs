using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaserRotatorScript : MonoBehaviour
{
	public Vector3 spinDirection;
	public float speed;

	//resets the scene if the player collides with the laser.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			string currentScene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (currentScene);
		}
	}

	void Update ()
	{
		transform.RotateAround (transform.position, spinDirection, speed * Time.deltaTime); //Spins the lasers around in a direction decided in the inspector.
	}
}
