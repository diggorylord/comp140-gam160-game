using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
	
	public Image fuelFillImage;
	public float maxFuelAmount = 100f;
	public float fuelUseAmount = 2f;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		maxFuelAmount -= fuelUseAmount * Time.deltaTime;
		fuelFillImage.fillAmount = maxFuelAmount / 100f;
	}
}
