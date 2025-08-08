using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandeler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public static GameObject itemBeingDragged;
	public static int valor;
	public Vector3 startPosition;
	public static Transform startParent;
	public GameObject b1;
	public GameObject b10;
	public GameObject b100;

	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		itemBeingDragged = gameObject;
		startPosition = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		itemBeingDragged = null;
		GetComponent<CanvasGroup> ().blocksRaycasts = true;

		if (transform.parent.tag == "lixo") {
			Destroy(gameObject);
		}
		
		if (transform.parent == startParent) {
			transform.position = startPosition;
		}

		// Garante que uma nova peça seja criada no slot de origem se ele ficar vazio
		if (startParent.childCount == 0)
		{
			if (startParent.tag == "1") {
				Transform blocoNovo = Instantiate(b1, startParent).transform;
				blocoNovo.localScale = Vector3.one; 
			} else if (startParent.tag == "10") {
				Transform blocoNovo = Instantiate(b10, startParent).transform;
				blocoNovo.localScale = Vector3.one; 
			} else if (startParent.tag == "100") {
				Transform blocoNovo = Instantiate(b100, startParent).transform;
				blocoNovo.localScale = Vector3.one; 
			}
		}

		ExecuteEvents.ExecuteHierarchy < IHasChanged> (gameObject, null, (x, y) => x.HasChanged ());
	}

	#endregion
}