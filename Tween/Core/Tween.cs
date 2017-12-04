using UnityEngine;
using System;

namespace Lofle.Tween
{
	public abstract class Tween : MonoBehaviour
	{
		public enum Style
		{
			Once,
			Loop,
			PingPong,
		}

		public enum Direction
		{
			Reverse = -1,
			Toggle = 0,
			Forward = 1,
		}

		public event Action _eventEndForward = null;
		public event Action _eventEndReverse = null;
		public event Action _eventEnd = null;

		[SerializeField]
		private UnityEngine.Events.UnityEvent _finishEvent = null;

		[Tooltip("재생방법")]
		[SerializeField]
		protected Style _style = Style.Once;

		[SerializeField]
		protected AnimationCurve _curve = new AnimationCurve( new Keyframe( 0f, 0f, 0f, 1f ), new Keyframe( 1f, 1f, 1f, 0f ) );

		[SerializeField]
		protected float _totalTime = 1f;

		[Tooltip( "정방향 재생" )]
		[SerializeField]
		private bool _isForward = true;

		/// <summary>
		/// 해당값이 false인 경우 From To를 시작기준을 1로 두면 안되고 0부터 처리 필요
		/// </summary>
		[Tooltip( "절대좌표" )]
		[SerializeField]
		private bool _isAbsolute = false;

		[SerializeField]
		private bool _isPlayOnStart = true;

		protected bool _bStart = false;
		private float _playTime = 0.0f;

		public bool isAbsolute { get { return _isAbsolute; } set { _isAbsolute = value; } }
		public bool isForward { get { return _isForward; } set { _isForward = value; } }
		public bool isStart { get { return _bStart; } }
		public float PlayTime { get { return _playTime; } }
		public float TotalTime { get { return _totalTime; } set { _totalTime = value; } }
		public AnimationCurve Curve { get { return _curve; } set { _curve = value; } }

		public bool isPlayOnStart { get { return _isPlayOnStart; } set { _isPlayOnStart = value; } }

		void Start()
		{
			Init();
			if( _isPlayOnStart )
			{
				Invoke();
			}
		}

		abstract protected void Init();

		public void Setting( Tween source )
		{
			_style = source._style;
			_curve = source._curve;
			_totalTime = source._totalTime;
			_isForward = source._isForward;
			_isAbsolute = source._isAbsolute;
			_isForward = source._isForward;
		}

		public void Invoke( float time = 0.0f )
		{
			_bStart = true;
			_playTime = time;
			enabled = true;
		}

		public void Invoke( bool duration, float time = 0.0f )
		{
			SetForward( duration );
			Invoke( time );
		}

		public void InvokeForward()
		{
			if( !_isForward )
			{
				InvokeToggle();
			}
			else
			{
				Invoke();
			}
		}

		public void InvokeReverse()
		{
			if( _isForward )
			{
				InvokeToggle();
			}
			else
			{
				Invoke();
			}
		}

		public void InvokeToggle()
		{
			Invoke( !_isForward, _totalTime- _playTime );
		}

		public void SetForward( bool duration )
		{
			_isForward = duration;
		}

		public void InvokeStop()
		{
			_bStart = false;
			enabled = false;
		}

		private void InvokeCallbackEnd()
		{
			if( null != _finishEvent )
			{
				_finishEvent.Invoke();
			}

			if( null != _eventEnd )
			{
				_eventEnd.Invoke();
			}
		}

		private void InvokeCallback( bool bForward )
		{
			if( bForward )
			{
				if( null != _eventEndForward )
				{
					_eventEndForward.Invoke();
				}
			}
			else
			{
				if( null != _eventEndReverse )
				{
					_eventEndReverse.Invoke();
				}
			}
		}

		private float GetFactor()
		{
			float result = 0.0f;

			if( _isForward )
			{
				result = (_playTime) / _totalTime;
			}
			else
			{
				result = (_totalTime - (_playTime)) / _totalTime;
			}

			if( IsEnd( result ) )
			{
				InvokeStop();

				if( _isForward )
				{
					result = 1.0f;
				}
				else
				{
					result = 0.0f;
				}
			}

			return _curve.Evaluate( result );
		}

		private bool IsEnd( float value )
		{
			bool result = false;

			result = _isForward ? IsForward( value ) : IsReverse( value );

			if( result )
			{
				InvokeCallbackEnd();
				InvokeCallback( _isForward );

				switch( _style )
				{
					case Style.Once:
						{

						}
						break;

					case Style.Loop:
						{
							_playTime = 0.0f;
							result = false;
						}
						break;

					case Style.PingPong:
						{
							_isForward = !_isForward;

							_playTime = 0.0f;
							result = false;
						}
						break;
				}
			}
			return result;
		}

		private bool IsForward( float value )
		{
			return (1.0f <= value);
		}

		private bool IsReverse( float value )
		{
			return (value <= 0.0f);
		}

		void Update()
		{
			if( _bStart )
			{
				_playTime += Time.smoothDeltaTime;
				ListenUpdate( GetFactor() );
			}
		}
		virtual protected void ListenUpdate( float factor ) { }
	}
}