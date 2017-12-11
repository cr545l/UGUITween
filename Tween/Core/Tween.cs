using UnityEngine;
using System;
using System.Collections;

namespace Lofle.Tween
{
	public abstract class Tween : MonoBehaviour
	{
		public enum Style
		{
			Once,
			PingPong,
		}

		public enum eDirection
		{
			None,
			Forwarded,
			Reversed,
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
		private bool _bForward = true;

		/// <summary>
		/// 해당값이 false인 경우 From To를 시작기준을 1로 두면 안되고 0부터 처리 필요
		/// </summary>
		[Tooltip( "절대좌표" )]
		[SerializeField]
		private bool _isAbsolute = false;

		[SerializeField]
		private bool _isPlayOnStart = true;

		[SerializeField]
		private bool _bLoop = false;

		private Coroutine _update = null;

		private float _playTime = 0.0f;
		private bool _bPingPongEnd = false;
		private bool _bPlayReservation = false;

		private eDirection _endDirection = eDirection.None;

		public bool isAbsolute { get { return _isAbsolute; } set { _isAbsolute = value; } }
		public bool isForward { get { return _bForward; } set { _bForward = value; } }
		public bool isPlaying { get { return null != _update; } }
		public bool isEnded { get { return !isPlaying; } }
		public float PlayTime { get { return _playTime; } }
		public float TotalTime { get { return _totalTime; } set { _totalTime = value; } }
		public AnimationCurve Curve { get { return _curve; } set { _curve = value; } }

		public bool isPlayOnStart { get { return _isPlayOnStart; } set { _isPlayOnStart = value; } }
		public bool isLoop { get { return _bLoop; } set { _bLoop = value; } }

		void Awake()
		{
			Init();
			if( _isPlayOnStart )
			{
				Invoke( 0, null );
			}
		}

		abstract protected void Init();

		public void Setting( Tween source )
		{
			_style = source._style;
			_curve = source._curve;
			_totalTime = source._totalTime;
			_bForward = source._bForward;
			_isAbsolute = source._isAbsolute;
			_bForward = source._bForward;
		}

		private IEnumerator Play( Action callback )
		{
			_bPlayReservation = false;

			bool bPlay = true;
			while( bPlay )
			{
				_playTime += Time.smoothDeltaTime;
				ListenUpdate( GetFactor( () => { bPlay = false; } ) );
				yield return null;
			}
			InvokeStop();

			if( null != callback )
			{
				callback();
			}
		}

		public void Play( bool bForward )
		{
			if( bForward )
			{
				PlayForward();
			}
			else
			{
				PlayReverse();
			}
		}

		public void PlayForward() { PlayForward( null ); }
		public void PlayForward( Action callback )
		{
			if( !_bForward )
			{
				InvokeToggle( callback );
			}
			else
			{
				Invoke( 0, callback );
			}
		}

		public void PlayReverse() { PlayReverse( null ); }
		public void PlayReverse( Action callback )
		{
			if( _bForward )
			{
				InvokeToggle( callback );
			}
			else
			{
				Invoke( 0, callback );
			}
		}

		private void InvokeToggle( Action callback )
		{
			Invoke( !_bForward, _totalTime - _playTime, callback );
		}

		private void Invoke( bool bForward, float time, Action callback )
		{
			_bForward = bForward;
			Invoke( time, callback );
		}
		
		private void Invoke( float time, Action callback )
		{
			_bPingPongEnd = false;
			_playTime = time;
			enabled = true;
			
			if( null != _update )
			{
				StopCoroutine( _update );
			}
			
			if( gameObject.activeInHierarchy )
			{
				_update = StartCoroutine( Play( callback ) );
			}
			else
			{
				Debug.LogWarningFormat( "{0} 현재 해당 게임 오브젝트가 비활성 상태로 동작을 실패함, Active를 true로 변경 필요", gameObject.name );
			}
		}

		public void InvokeStop()
		{
			_update = null;
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
					_endDirection = eDirection.Forwarded;
					_eventEndForward.Invoke();
				}
			}
			else
			{
				if( null != _eventEndReverse )
				{
					_endDirection = eDirection.Reversed;
					_eventEndReverse.Invoke();
				}
			}
		}

		private float GetFactor( Action stop )
		{
			float result = 0.0f;

			if( _bForward )
			{
				result = (_playTime) / _totalTime;
			}
			else
			{
				result = (_totalTime - (_playTime)) / _totalTime;
			}

			if( isEnd( result ) )
			{
				if( _bForward )
				{
					result = 1.0f;
				}
				else
				{
					result = 0.0f;
				}
				stop();
			}

			return _curve.Evaluate( result );
		}

		private bool isEnd( float value )
		{
			bool result = false;

			result = _bForward ? isForwardEnd( value ) : isReverseEnd( value );

			if( result )
			{
				InvokeCallbackEnd();
				InvokeCallback( _bForward );

				switch( _style )
				{
					case Style.Once:
						{

						}
						break;

					case Style.PingPong:
						{
							if( _bLoop || !_bPingPongEnd )
							{
								_bForward = !_bForward;

								_playTime = 0.0f;
								result = false;

								_bPingPongEnd = true;
							}
						}
						break;
				}

				if( _bLoop )
				{
					_playTime = 0.0f;
					result = false;
				}
			}

			return result;
		}

		private bool isForwardEnd( float value )
		{
			return (1.0f <= value);
		}

		private bool isReverseEnd( float value )
		{
			return (value <= 0.0f);
		}

		virtual protected void ListenUpdate( float factor ) { }
	}
}