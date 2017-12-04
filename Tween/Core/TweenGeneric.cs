using UnityEngine;
using System.Collections;

namespace Lofle.Tween
{
	public abstract class TweenGeneric<T> : Tween
	{
		[SerializeField]
		private T _from;
		[SerializeField]
		private T _to;

		private T _by;
		protected GameObject _target = null;

		public T From { get { return _from; } set { _from = value; } }
		public T To { get { return _to; } set { _to = value; } }

		abstract protected void SetValue( T value );
		abstract protected T GetValue();

		abstract protected T Sum( T lParameter, T rParameter );
		abstract protected T Subtraction( T lParameter, T rParameter );
		abstract protected T Multiply( T lParameter, float rParameter );

		public void Setting( T from, T to, Tween source )
		{
			base.Setting( source );

			_from = from;
			_to = to;
		}

		virtual protected void GenericInInit() { }
		override protected void Init()
		{
			GenericInInit();
			_target = gameObject;

			if( !isAbsolute )
			{
				_from = Sum( _from, GetValue() );
				_to = Sum( _to, GetValue() );
			}

			_by = Subtraction( _to, _from );

			SetValue( isForward ? _from : _to );
		}

		override protected void ListenUpdate( float factor )
		{
			SetValue( Sum( _from, Multiply( _by, factor ) ) );
		}
	}
}