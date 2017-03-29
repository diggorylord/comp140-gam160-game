using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LaserRotatorScript : MonoBehaviour
{
	public Vector3 spinDirection;
	public float speed;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			string currentScene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (currentScene);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.RotateAround (transform.position, spinDirection, speed * Time.deltaTime);
	}
}
