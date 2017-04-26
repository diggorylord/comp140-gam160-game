using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			string currentScene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (currentScene);
		}
	}
}
