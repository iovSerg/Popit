using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public static Action EventNextLevel;
	public static Action<float> EventGameOver;
	public static int SceneIndex = 0;

    [SerializeField] private GameObject game_UI;
    [SerializeField] private GameObject menu_UI;


	[SerializeField] private TextMeshProUGUI level;
	[SerializeField] private TextMeshProUGUI levelHighScore;
	[SerializeField] private TextMeshProUGUI unlimHighScore;

	private int _level = 1;
    private bool _isVibration = false;

	//Player score
	private float _bestUnlimTime;
	private float _bestLevelTime;

	private void Start()
	{
		if(SceneIndex == 1)
		{
			level.text = "Level : " + _level.ToString();
			EventNextLevel += OnNextLevel;
		}
		else
		{
			level.text = "Time : ";
			EventGameOver += OnGameoVer;
		}
		LoadScore();
	}

	private void OnGameoVer(float time)
	{
		
	}

	private void LoadScore()
	{
		//Unlim
		if (PlayerPrefs.GetFloat("_bestUnlimTime") <= _bestUnlimTime)
			PlayerPrefs.SetFloat("_bestUnlimTime", _bestUnlimTime);

		_bestUnlimTime = PlayerPrefs.GetFloat("_bestUnlimTime");

		unlimHighScore.text = _bestUnlimTime.ToString() + " sec";


		//Level
		if (PlayerPrefs.GetFloat("_bestLevelTime") <= _bestLevelTime)
			PlayerPrefs.SetFloat("_bestLevelTime", _bestLevelTime);

		_bestLevelTime = PlayerPrefs.GetFloat("_bestLevelTime");

		levelHighScore.text = _bestLevelTime.ToString() + " sec";
	}

	private void OnNextLevel()
	{
			_level++;
			level.text = "Level : " + _level.ToString();
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

	public void SelecetLevel(int scene)
	{
		SceneIndex = scene;
		SceneManager.LoadScene(scene);
	}

	private void OnDestroy()
	{
		EventNextLevel -= OnNextLevel;
	}

}
