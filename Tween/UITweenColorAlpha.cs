using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Lofle.Tween
{
	public class UITweenColorAlpha : TweenGenericFloat
	{
		private float _alpha = 0.0f;

		private Image[] _findImages = null;
		private Text[] _findtexts = null;

		override protected void GenericInInit()
		{
			_findImages = gameObject.GetComponentsInChildren<Image>();
			_findtexts = gameObject.GetComponentsInChildren<Text>();
		}

		override protected void SetValue( float value )
		{
			_alpha = value;

			SetImageAlpha( _alpha );
			SetTextAlpha( _alpha );
		}

		private void SetImageAlpha( float value )
		{
			for( int i = 0; i < _findImages.Length; i++ )
			{
				_findImages[i].color = new Color( _findImages[i].color.r, _findImages[i].color.g, _findImages[i].color.b, value );
			}
		}

		private void SetTextAlpha( float value )
		{
			for( int i = 0; i < _findtexts.Length; i++ )
			{
				_findtexts[i].color = new Color( _findtexts[i].color.r, _findtexts[i].color.g, _findtexts[i].color.b, value );
			}
		}

		override protected float GetValue()
		{
			return _alpha;
		}
	}
}