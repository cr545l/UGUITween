using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public class TweenRotation : TweenGenericVector3
	{
		override protected void SetValue( Vector3 value )
		{
			_target.transform.localEulerAngles = value;
		}

		override protected Vector3 GetValue()
		{
			return _target.transform.localEulerAngles;
		}
	}
}