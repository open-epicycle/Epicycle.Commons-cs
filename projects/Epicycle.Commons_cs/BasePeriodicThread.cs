// [[[[INFO>
// Copyright 2014 Epicycle (http://epicycle.org)
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
// For more information check https://github.com/open-epicycle/Epicycle.Commons-cs
// ]]]]

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
