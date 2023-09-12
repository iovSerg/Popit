using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SphereController[] spheres;

    [SerializeField] private float _waitActiveSphere = 1f;
    [SerializeField] private int _quantitySphere = 3;
    [SerializeField] private bool _isActiveCourutine = false;
	[SerializeField] private AudioClip _clipClick;

	private AudioSource audioSource;


	private float _toNextLevel = 3;

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
		CheckActiveSphere();
		if(_toNextLevel == 0) NextLevel();
    }

	private void NextLevel()
	{
		_toNextLevel = 3;
		_quantitySphere++;
		if(_quantitySphere ==  spheres.Length)
		{
			Time.timeScale = 0f;
		}
		if (_waitActiveSphere > 0.2f)
			_waitActiveSphere -= 0.1f;
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
		yield return new WaitForSeconds(_waitActiveSphere);
        ActiveSphere();
		_toNextLevel--;
		_isActiveCourutine = false;
	}
	public void PlayClick()
	{
		if (!audioSource.isPlaying) audioSource.PlayOneShot(_clipClick);
	}
}
