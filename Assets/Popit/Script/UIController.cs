using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
	public static Action EventNextLevel;
	public static Action EventGameOver;

	// ? Level : Unlim
	public static bool GameMode = true;

	[Header("UI Component")]
    [SerializeField] private GameObject game_UI;
    [SerializeField] private GameObject menu_UI;
	[SerializeField] private GameObject gameOver_UI;
	[Space(1)]
	[Header("Game UI Component")]
	[SerializeField] private TextMeshProUGUI level;
	[Space(1)]
	[Header("Main Menu UI Component")]
	[SerializeField] private TextMeshProUGUI levelBestTime;
	[SerializeField] private TextMeshProUGUI unlimBestTime;
	[Space(1)]
	[Header("Game Over UI Component")]
	[SerializeField] private TextMeshProUGUI bestTime;
	[SerializeField] private TextMeshProUGUI currentTime;

	private int _level = 1;
    private bool _isVibration = false;

	//Player score
	private float _bestUnlimTime = 0f;
	private float _bestLevelTime = 0f;

	private float _timeGame;

	private void Start()
	{
		EventNextLevel += OnNextLevel;
		EventGameOver += OnGameOver;

		if (GameMode)
			level.text = "Level : " + _level.ToString();
		else
			level.text = "Time : ";

		LoadScore();
	}
	private void Update()
	{
		_timeGame += Time.deltaTime;
		if(!GameMode) level.text = "Time : " + TimeSpan.FromMinutes(_timeGame).ToString().Substring(0, 5);
	}
	private void OnGameOver()
	{
		Time.timeScale = 0f;
		game_UI.SetActive(false);
		gameOver_UI.SetActive(true);

		if (GameMode)
		{
			bestTime.text = _bestLevelTime.ToString() + " sec";
			if (_timeGame < _bestLevelTime) PlayerPrefs.SetFloat("_bestLevelTime", _bestLevelTime);
		}
		else
		{
			bestTime.text = _bestUnlimTime.ToString() + " sec";
			if (_timeGame > _bestUnlimTime) PlayerPrefs.SetFloat("_bestUnlimTime", _bestUnlimTime);
		}

		
		currentTime.text = TimeSpan.FromMinutes(_timeGame).ToString().Substring(0, 5);
	}

	private void LoadScore()
	{
		//Unlim
		if (PlayerPrefs.GetFloat("_bestUnlimTime") <= _bestUnlimTime)
			PlayerPrefs.SetFloat("_bestUnlimTime", _bestUnlimTime);

		_bestUnlimTime = PlayerPrefs.GetFloat("_bestUnlimTime");

		unlimBestTime.text = _bestUnlimTime.ToString() + " sec";


		//Level
		if (PlayerPrefs.GetFloat("_bestLevelTime") <= _bestLevelTime)
			PlayerPrefs.SetFloat("_bestLevelTime", _bestLevelTime);

		_bestLevelTime = PlayerPrefs.GetFloat("_bestLevelTime");

		levelBestTime.text = _bestLevelTime.ToString() + " sec";
	}

	private void OnNextLevel()
	{
		_level++;
		level.text = "Level : " + _level.ToString();
	}

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
	public void PressButtonMainMenu()
	{
		SceneManager.LoadScene(0);
	}
	public void PressButtonQuit()
	{
		Application.Quit();
	}

	public void SelecetLevel(int scene)
	{
		GameMode = scene == 1 ? true : false;
		SceneManager.LoadScene(1);
	}

	private void OnDestroy()
	{
		EventNextLevel -= OnNextLevel;
		EventGameOver -= OnGameOver;
	}

}
