using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChangeObject : MonoBehaviour
{
	public string sceneToLoad;

	void OnTriggerEnter()
	{
		SceneManager.LoadScene(sceneToLoad); // Changes level when the pickup is collected.
		Destroy(gameObject);
	}
}
