using UnityEngine;
using System.Collections;

public class TutorialTextChanger : MonoBehaviour
{
	public GameObject tutorial1;
	public GameObject tutorial2;
	public GameObject tutorial3;
	public GameObject tutorial4;

	private float tutorialTimer = 5f;
	private int tutorialCounter;

	void Update ()
	{
		tutorialTimer -= Time.deltaTime;
		if (tutorialTimer <= 0) 
		{
			tutorialCounter ++;
			TutorialChanger ();
			tutorialTimer = 5f;
		}
	}

	void TutorialChanger()
	{
		if (tutorialCounter == 1) 
		{
			tutorial1.SetActive (false);
			tutorial2.SetActive (true);
			tutorial3.SetActive (false);
			tutorial4.SetActive (false);
		}

		else if (tutorialCounter == 2) 
		{
			tutorial1.SetActive (false);
			tutorial2.SetActive (false);
			tutorial3.SetActive (true);
			tutorial4.SetActive (false);
		}

		else if (tutorialCounter == 3)
		{
			tutorial1.SetActive (false);
			tutorial2.SetActive (false);
			tutorial3.SetActive (false);
			tutorial4.SetActive (true);
		}

		else if (tutorialCounter == 4) 
		{
			tutorial1.SetActive (false);
			tutorial2.SetActive (false);
			tutorial3.SetActive (false);
			tutorial4.SetActive (false);
		}
	}
}
