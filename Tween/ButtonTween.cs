using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ButtonTween : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	[SerializeField]
	private UnityEvent _eventOnPointerDown = new UnityEvent();
	[SerializeField]
	private UnityEvent _eventOnPointerUp = new UnityEvent();

	public UnityEvent EventOnPointerDown { get { return _eventOnPointerDown; } }
	public UnityEvent EventOnPointerUp { get { return _eventOnPointerUp; } }

	public void OnPointerDown( PointerEventData eventData )
	{
		if( null != _eventOnPointerDown )
		{
			_eventOnPointerDown.Invoke();
		}
	}

	public void OnPointerUp( PointerEventData eventData )
	{
		if( null != _eventOnPointerUp )
		{
			_eventOnPointerUp.Invoke();
		}
	}
}
