using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Lofle.Tween
{
	public class UITweenColorAlpha : TweenGenericFloat
	{
		private float _alpha = 0.0f;

		private List<KeyValuePair<MaskableGraphic, float>> _list = new List<KeyValuePair<MaskableGraphic, float>>();

		override protected void GenericInInit()
		{
			Find( gameObject.GetComponentsInChildren<MaskableGraphic>() );
		}

		override protected void SetValue( float value )
		{
			_alpha = value;
			
			Set( value );
		}

		private void Set( float value )
		{
			_list.ForEach( x => x.Key.color = new Color( x.Key.color.r, x.Key.color.g, x.Key.color.b, x.Value * value ) );
		}

		private void Find<T>( T[] find ) where T : MaskableGraphic
		{
			foreach( var i in find )
			{
				_list.Add( new KeyValuePair<MaskableGraphic, float>( i, i.color.a ) );
			}
		}

		override protected float GetValue()
		{
			return _alpha;
		}
	}
}