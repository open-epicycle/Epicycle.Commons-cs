// [[[[INFO>
// Copyright 2015 Epicycle (http://epicycle.org, https://github.com/open-epicycle)
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

namespace Epicycle.Commons
{
    public sealed class PeriodicThread : BasePeriodicThread
    {
        private readonly IUpdatable _updatable;

        public PeriodicThread(IUpdatable updatable, double frequency_hz, PeriodicThreadTightness tightness = DefaultTightness)
            : base(frequency_hz, tightness)
        {
            _updatable = updatable;
        }

        public PeriodicThread(IUpdatable updatable, int delay_msec, int minDelay_msec)
            : base(delay_msec, minDelay_msec)
        {
            _updatable = updatable;
        }

        protected override void Iteration()
        {
            _updatable.Update();
        }
    }
}
