using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{ 
	public abstract class TweenGenericFloat : TweenGeneric<float>
	{
		override abstract protected void SetValue( float value );
		override abstract protected float GetValue();

		override protected float Sum( float lParameter, float rParameter ) { return lParameter + rParameter; }
		override protected float Subtraction( float lParameter, float rParameter ) { return lParameter - rParameter; }
		override protected float Multiply( float lParameter, float rParameter ) { return lParameter * rParameter; }
	}
}