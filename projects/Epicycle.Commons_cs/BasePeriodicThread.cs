using System;
using System.Diagnostics;
using System.Threading;

namespace Epicycle.Commons
{
    public abstract class BasePeriodicThread
    {
        private readonly int _delay;
        private readonly int _minDelay;

        private bool _isStopped;

        private Thread _thread;

        public BasePeriodicThread(int delay, int minDelay)
        {
            _delay = delay;
            _minDelay = minDelay;

            _isStopped = false;

            _thread = new Thread(ThreadLoop);
        }

        protected void Start()
        {
            _thread.Start();
        }

        public void Stop()
        {
            _isStopped = true;
            _thread.Interrupt();
        }

        public Thread Thread
        {
            get { return _thread; }
        }

        private void ThreadLoop()
        {
            var stopwatch = new Stopwatch();

            while (!_isStopped)
            {
                stopwatch.Start();
                Iteration();
                stopwatch.Stop();

                int sleepTime = Math.Max(_minDelay, Math.Min(_delay, _delay - (int)stopwatch.ElapsedMilliseconds));

                try
                {
                    Thread.Sleep(sleepTime);
                }
                catch(ThreadInterruptedException) { }
            }
        }

        protected abstract void Iteration();
    }
}
