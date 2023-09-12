using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject game_UI;
    [SerializeField] private GameObject menu_UI;

    private bool _isVibration = false;
	private void Start()
	{
		
	}
	#region Menu UI
	public void PressButtonMenu()
	{

		Time.timeScale = 0f;
		game_UI.SetActive(false);
		menu_UI.SetActive(true);
	}

	public void PressButtonResume()
	{
		Time.timeScale = 1f;
		menu_UI.SetActive(false);
		game_UI.SetActive(true);
	}
	public void PressTriggerVibration()
	{
		_isVibration = !_isVibration;
	}
	public void PressButtonRestart()
	{

	}
	public void PressButtonQuit()
	{
		Application.Quit();
	}
	#endregion

}
