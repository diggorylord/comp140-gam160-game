using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelReset : MonoBehaviour
{
	public string sceneToLoad;

	// used to reset the level when player colides with a hazard.
	//uses a string set in the inspector as to what scene is loaded.
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			SceneManager.LoadScene (sceneToLoad);
		}
	}
}
