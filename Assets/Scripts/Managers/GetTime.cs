using UnityEngine;
using System;
using UnityEngine.UI;

public class GetTime : MonoBehaviour
{
	[SerializeField] Text datetimeText;

	void FixedUpdate()
	{
		DateTime currentDateTime = WorldTimeAPI.Instance.GetCurrentDateTime();

		datetimeText.text = currentDateTime.ToString();
	}
}
