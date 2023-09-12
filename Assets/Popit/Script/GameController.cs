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

    private SphereController[] spheres;
	private AudioSource audioSource;

	private float _toNextLevel = 3;
    private int _quantitySphere = 1;
    private bool _isActiveCourutine = false;

	void Start()
    {
		audioSource = GetComponent<AudioSource>();
        spheres = GetComponentsInChildren<SphereController>();
        foreach (SphereController sphere in spheres)
        {
            sphere.SetState(false);
        }
        ActiveSphere();
    }
    void Update()
    {
		if(_toNextLevel == 0) NextLevel();
		CheckActiveSphere();
    }

	private void NextLevel()
	{
		UIController.EventNextLevel?.Invoke();
		_toNextLevel = 3;
		_quantitySphere++;
		if(_quantitySphere ==  spheres.Length)
		{
			
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
	public void PlayClick()
	{
		if (!audioSource.isPlaying) audioSource.PlayOneShot(_clipClick);
	}
}
