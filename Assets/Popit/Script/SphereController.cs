using System.Collections;
using UnityEngine;

public class SphereController : MonoBehaviour
{
	public float timeActive;
	public bool isActive = false;

	[SerializeField] private IGameController gameController;
	[SerializeField] private MeshRenderer render;
	private void Awake()
	{
		gameController = GetComponentInParent<IGameController>();

		render = GetComponent<MeshRenderer>();
	}
	public void ClickSphere()
	{
		render.enabled = true;
	}
	public void SetState(bool state)
	{
		if (!state) { gameController.PlayClick(); }
		render.enabled = state;
		isActive = state;
	}

	private IEnumerator ActiveSphere(float timeActive)
	{
		yield return new WaitForSeconds(timeActive);
		render.enabled = true;
	}
	private IEnumerator NotActiveSphere(float timeActive)
	{
		yield return new WaitForSeconds(timeActive);
		render.enabled = true;
	}
}
