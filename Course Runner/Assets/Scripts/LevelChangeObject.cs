using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelChangeObject : MonoBehaviour
{
	void OnTriggerEnter()
	{
		SceneManager.LoadScene ("EndScene"); // Changes level when the pickup is collected.
		Destroy(gameObject);
	}
}
