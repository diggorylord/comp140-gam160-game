using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
	public string sceneToLoad;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
