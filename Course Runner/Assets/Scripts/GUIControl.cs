using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GUIControl : MonoBehaviour
{


	public void StartGame (int sceneIndex)
	{

		SceneManager.LoadScene (sceneIndex);

	}
		

	public void QuitGame ()
	{

		#if UNITY_EDITOR

		UnityEditor.EditorApplication.isPlaying = false;

		#else

		Application.Quit();

		#endif

	}

}
