using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{

	[Header("Time visibility sphere")]
    [SerializeField] private float _timeApperance = 0.5f;
	[SerializeField] private float _timeDecrease = 0.1f;
	[Space(2)]
	[Header("Audio click sphere")]
	[SerializeField] private AudioClip _clipClick;
	[Space(2)]
	[Header("Unlim Parametrs")]
	[SerializeField] private float _timeActiveSphere = 2f;


    private SphereController[] spheres;
	private AudioSource audioSource;

	private float _toNextLevel = 3;
    private int _quantitySphere = 1;
    private bool _isActiveCourutine = false;

	private bool _isLevel;
	private float _unlimTimeFlag = 0f;

	void Start()
    {
		if(Time.timeScale == 0f) Time.timeScale = 1f;

		_isLevel = UIController.GameMode;
		audioSource = GetComponent<AudioSource>();
        spheres = GetComponentsInChildren<SphereController>();
        foreach (SphereController sphere in spheres)
        {
            sphere.SetState(false);
        }
		if(_isLevel) ActiveSphere();
		else UnlimActivateSphere();
	}
    void Update()
    {
		_unlimTimeFlag += Time.deltaTime;
		if(_toNextLevel == 0)
		{
			NextLevel();
		}
		if(_isLevel)
		{
			CheckActiveSphere();
		}
		else
		{
			UnlimCheckActiveSphere();
		}
    }


	#region Level Method
	private void NextLevel()
	{
		UIController.EventNextLevel?.Invoke();
		_toNextLevel = 3;
		_quantitySphere++;

		if (_quantitySphere == spheres.Length)
		{
			UIController.EventGameOver?.Invoke();
		}

		if (_timeApperance > _timeDecrease)
			_timeApperance -= _timeDecrease;
	}

	private void ActiveSphere()
	{
		for(int i = 0; i < _quantitySphere; i++)
        {
            int index = Random.Range(0,spheres.Length);
			while (true)
			{
				if (!spheres[index].isActive)
				{
					spheres[index].SetState(true);
					break;
				}
				index = Random.Range(0, spheres.Length);
			}
        }
	}

	private void CheckActiveSphere()
	{
		bool clear = true;
		if(!_isActiveCourutine)
        {
			foreach (var sphere in spheres)
			{
				if (sphere.isActive)
				{
					clear = false;
					break;
				}
				else clear = true;
			}
			if (clear)
			{
				_isActiveCourutine = true;
				StartCoroutine(WaitActiveSphere());
			}
		}
	
	}

	private IEnumerator WaitActiveSphere()
	{
		yield return new WaitForSeconds(_timeApperance);
        ActiveSphere();
		_toNextLevel--;
		_isActiveCourutine = false;
	}
	#endregion

	#region Unlim Method
	public void UnlimCheckActiveSphere()
	{
		float activeSphere = 0;
		foreach(var sphere in spheres)
		{
			if (!sphere.isActive)
				activeSphere++;
		}

		if (activeSphere == spheres.Length) _timeActiveSphere -= _timeDecrease;
	}
	public void UnlimActivateSphere()
	{
		int index = Random.Range(0, spheres.Length);
		while (true)
		{
			if (!spheres[index].isActive)
			{
				spheres[index].SetState(true);
				break;
			}
			index = Random.Range(0, spheres.Length);

		}
	}

	#endregion
	public void PlayClick(SphereController sphere)
	{
		if (!audioSource.isPlaying) audioSource.PlayOneShot(_clipClick);
		if (!_isLevel)
		{
			sphere.ActiveSphere(_timeActiveSphere);
			UnlimActivateSphere();
		}
	}
}
