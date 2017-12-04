using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public abstract class TweenGenericColor : TweenGeneric<Color>
	{
		override abstract protected void SetValue( Color value );
		override abstract protected Color GetValue();

		override protected Color Sum( Color lParameter, Color rParameter ) { return lParameter + rParameter; }
		override protected Color Subtraction( Color lParameter, Color rParameter ) { return lParameter - rParameter; }
		override protected Color Multiply( Color lParameter, float rParameter ) { return lParameter * rParameter; }
	}
}