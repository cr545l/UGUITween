using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public class TweenTransform : Tween
	{
		[SerializeField]
		private Transform _from = null;
		[SerializeField]
		private Transform _to = null;

		private TweenPosition _position = null;
		private TweenRotation _rotation = null;
		private TweenScale _scale = null;

		override protected void Init()
		{
			_position = gameObject.AddComponent<TweenPosition>();
			_rotation = gameObject.AddComponent<TweenRotation>();
			_scale = gameObject.AddComponent<TweenScale>();

			_position.Setting( _from.transform.position, _to.transform.position, this );
			_rotation.Setting( _from.transform.eulerAngles, _to.transform.eulerAngles, this );
			_scale.Setting( _from.transform.localScale, _to.transform.localScale, this );
		}
	}
}