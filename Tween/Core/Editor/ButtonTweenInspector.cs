using Lofle.Tween;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor( typeof( ButtonTween ) )]
public class ButtonTweenInspector : Editor
{
	public override void OnInspectorGUI()
	{
		// base.OnInspectorGUI();

		var buttonEvent = Selection.activeGameObject.GetComponent<ButtonTween>();

		GUILayout.BeginHorizontal();

		GUI.enabled = null == buttonEvent.GetComponent<TweenScale>();
		if( GUILayout.Button( "Scale" ) )
		{
			var tween = buttonEvent.gameObject.AddComponent<TweenScale>();
			tween.isPlayOnStart = false;
			tween.To = new Vector3( 0.1f, 0.1f, 0 );
			tween.TotalTime = 0.2f;
			tween.Curve = AnimationCurve.EaseInOut( 0, 0, 1, 1 );

			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerDown, tween.PlayForward );
			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerUp, tween.PlayReverse );
		}

		GUI.enabled = null == buttonEvent.GetComponent<TweenRotation>();
		if( GUILayout.Button( "Rotation" ) )
		{
			var tween = buttonEvent.gameObject.AddComponent<TweenRotation>();
			tween.isPlayOnStart = false;
			tween.To = new Vector3( 1, 1, 1 );

			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerDown, tween.PlayForward );
			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerUp, tween.PlayReverse );
		}

		GUI.enabled = null == buttonEvent.GetComponent<TweenPosition>();
		if( GUILayout.Button( "Position" ) )
		{
			var tween = buttonEvent.gameObject.AddComponent<TweenPosition>();
			tween.isPlayOnStart = false;
			tween.To = new Vector3( 1, 1, 1 );

			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerDown, tween.PlayForward );
			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerUp, tween.PlayReverse );
		}

		GUI.enabled = null == buttonEvent.GetComponent<UITweenColorAlpha>();
		if( GUILayout.Button( "ColorAlpha" ) )
		{
			var tween = buttonEvent.gameObject.AddComponent<UITweenColorAlpha>();
			tween.isPlayOnStart = false;

			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerDown, tween.PlayForward );
			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerUp, tween.PlayReverse );
		}

		GUI.enabled = null == buttonEvent.GetComponent<TweenTransform>();
		if( GUILayout.Button( "Transform" ) )
		{
			var tween = buttonEvent.gameObject.AddComponent<TweenTransform>();
			tween.isPlayOnStart = false;

			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerDown, tween.PlayForward );
			UnityEventTools.AddPersistentListener( buttonEvent.EventOnPointerUp, tween.PlayReverse );
		}

		GUILayout.EndHorizontal();
	}
}