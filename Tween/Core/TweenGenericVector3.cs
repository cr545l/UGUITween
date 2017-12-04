using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public abstract class TweenGenericVector3 : TweenGeneric<Vector3>
	{
		override abstract protected void SetValue( Vector3 value );
		override abstract protected Vector3 GetValue();

		override protected Vector3 Sum( Vector3 lParameter, Vector3 rParameter ) { return lParameter + rParameter; }
		override protected Vector3 Subtraction( Vector3 lParameter, Vector3 rParameter ) { return lParameter - rParameter; }
		override protected Vector3 Multiply( Vector3 lParameter, float rParameter ) { return lParameter * rParameter; }
	}
}