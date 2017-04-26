using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartLevelUI : MonoBehaviour
{
	
	public GameObject LevelXCanvas;
	public GameObject LevelXText;
	public GameObject LevelXStartText;

	private float setInactiveTimer = 4f;
	private float textSwitchingTimer = 2f;

	void Start ()
	{
		
	}

	void Update ()
	{
		setInactiveTimer -= Time.deltaTime;
		textSwitchingTimer -= Time.deltaTime;

		if (setInactiveTimer <= 0) 
		{
			LevelXCanvas.SetActive (false);
		} 

		else if (textSwitchingTimer <= 0) 
		{
			LevelXText.SetActive (false);
			LevelXStartText.SetActive (true);
		}
	}
}
