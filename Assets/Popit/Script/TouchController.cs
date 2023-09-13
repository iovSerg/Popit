
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchController :MonoBehaviour ,IPointerDownHandler, IDragHandler, IPointerUpHandler
{
	private Camera mainCamera;
	private void Start()
	{
		mainCamera = Camera.main;
	}
	public void OnDrag(PointerEventData eventData)
	{
		
	}

	public void OnDrop(PointerEventData eventData)
	{
		
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		Ray ray = mainCamera.ScreenPointToRay(eventData.position);
		if(Physics.Raycast(ray.origin, ray.direction,out RaycastHit hitInfo))
		{
			if(hitInfo.collider.gameObject.TryGetComponent<SphereController>(out SphereController sphere))
			{
				if(sphere.isActive)
				sphere.SetState(false);
			}
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		
	}
}
