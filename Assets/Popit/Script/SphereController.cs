using System.Collections;
using UnityEngine;

public class SphereController : MonoBehaviour
{
	public float timeActive;
	public bool isActive = false;

	[SerializeField] private GameController gameController;
	[SerializeField] private MeshRenderer render;
	private void Awake()
	{
		gameController = GetComponentInParent<GameController>();

		render = GetComponent<MeshRenderer>();
	}
	public void ClickSphere()
	{
		render.enabled = true;
	}
	public void SetState(bool state)
	{
		if (!state) { gameController.PlayClick(this); }
		render.enabled = state;
		isActive = state;
	}
	public void ActiveSphere(float time)
	{
		StartCoroutine(CorutineActiveSphere(time));
	}
	private IEnumerator CorutineActiveSphere(float timeActive)
	{
		yield return new WaitForSeconds(timeActive);
		isActive = true;
		render.enabled = true;
	}
	private IEnumerator NotActiveSphere(float timeActive)
	{
		yield return new WaitForSeconds(timeActive);
		render.enabled = true;
	}
}
