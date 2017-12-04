using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public class TweenScale : TweenGenericVector3
	{
		override protected void SetValue( Vector3 value )
		{
			_target.transform.localScale = value;
		}

		override protected Vector3 GetValue()
		{
			return _target.transform.localScale;
		}
	}
}