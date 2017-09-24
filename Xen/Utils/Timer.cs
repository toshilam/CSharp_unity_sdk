using System;
using Xen.Event;
using Xen.Behaviour;
using Xen.Net;

namespace Xen.Utils
{
	public class Timer : EventDispatcher<TimerEvent>
	{
//		private int _timerID = -1;

		private double _delay;
		public double delay { get{ return this._delay; }}

		private int _repeatCount;
		public int repeatCount { get{ return this._repeatCount; } }

		private int _currentCount;
		public int currentCount { get{ return this._currentCount; } }

		private bool _running;
		public bool running { get{ return this._running; } }

		private System.Timers.Timer _timer;

		private AppBehaviour _behaviour;

		/// <summary>
		/// Initializes a new instance of the <see cref="Xen.Utils.Timer"/> class.
		/// </summary>
		/// <param name="interval">Interval.</param>
		/// <param name="count">Count.</param>
		/// <param name="behaviour">if set event will be dispatched in main thread.</param>
		public Timer (double interval, int count, AppBehaviour behaviour = null)
		{
			if(interval <= 0)
			{
				throw new ArgumentException("Timer : unable to init timer!");
			}

			this._behaviour = behaviour;

			_timer = new System.Timers.Timer ();
			_timer.Elapsed += _TimerEventHandler;
			_timer.Enabled = false;

			this._delay = interval;
			this._repeatCount = count <= 0 ? int.MaxValue : count;
		}

		public bool Start()
		{
			if(this.repeatCount > 0 && this.delay > 0 && !this.running)
			{
				this._running = true;
//				_timer = new System.Timers.Timer (this.delay);
				_timer.Interval = this.delay;
				_timer.Enabled = true;
				_timer.Start ();
				//console.log('this._timerID : ' + this._timerID);
				//wdf.Tracer.echo('Timer : start : ' + this._timerID);
				return true;
			}

			Tracer.Echo("Timer : start : fail : timer is running or no data is set!");
			return false;
		}

		public void Stop()
		{
			this._running = false;

			if (this._timer != null)
			{
				this._timer.Stop ();
				this._timer.Enabled = false;

//				this._timer = null;
			}
		}

		public void Reset()
		{
			this.Stop ();
			this._currentCount = 0;
		}

		private void _TimerEventHandler(Object source, System.Timers.ElapsedEventArgs e)
		{
			Tracer.Echo ("Timer : _TimerEventHandler : TICK!");

			this._running = true;
			++this._currentCount;

			TimerEvent evt = new TimerEvent(TimerEvent.TIMER, this);

			//TODO : check if behaviour is actually added on stage? otherwise may have error?
			if (this._behaviour != null)
			{
				this._behaviour.AddUIThreadAction (new ServiceRequest ("", evt, new Responder(this._TimerEventHandlerMainThread, null)));

			}
			else
			{
				this._TimerEventHandlerMainThread (evt);
			}


			if(this.currentCount >= this.repeatCount)
			{
				this.Reset();

				evt = new TimerEvent (TimerEvent.TIMER_COMPLETE, this);
				if (this._behaviour != null)
				{
					this._behaviour.AddUIThreadAction (new ServiceRequest ("", evt, new Responder(this._TimerEventHandlerMainThread, null)));

				}
				else
				{
					this._TimerEventHandlerMainThread (evt);
				}

			}


		}

		private void _TimerEventHandlerMainThread(object e)
		{
			this.DispatchEvent(e as TimerEvent);
		}


	}
}

