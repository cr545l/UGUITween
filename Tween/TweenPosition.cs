using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public class TweenPosition : TweenGenericVector3
	{
		override protected void SetValue( Vector3 value )
		{
			_target.transform.localPosition = value;
		}

		override protected Vector3 GetValue()
		{
			return _target.transform.localPosition;
		}
	}
}