using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class TouchInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{

	private bool ifTouched = false;

	public void OnPointerDown (PointerEventData data) 
    {
		ifTouched = true;
	}

	public void OnPointerUp (PointerEventData data) 
    {
		ifTouched = false;
	}

	public bool IfTouched () 
    {
		return ifTouched;
	}
}
