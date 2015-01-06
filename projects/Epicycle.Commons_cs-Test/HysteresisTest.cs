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

using NUnit.Framework;

namespace Epicycle.Commons
{
    [TestFixture]
    public sealed class HysteresisTest : AssertionHelper
    {
        [Test]
        public void Hysteresis_is_uninitialized_after_construction()
        {
            var hysteresis = new Hysteresis(range: 7, phaseCount: 5);

            Expect(hysteresis.IsInitialized, Is.False);
            Expect(hysteresis.Phase, Is.Null);
        }

        [Test]
        public void Hysteresis_is_initialized_to_specified_phase_after_initialization()
        {
            var phase = 3;

            var hysteresis = new Hysteresis(range: 7, phaseCount: 5);
            hysteresis.Initialize(phase);

            Expect(hysteresis.IsInitialized, Is.True);
            Expect(hysteresis.Phase, Is.EqualTo(phase));
        }

        [Test]
        public void Update_of_small_magnitude_doesnt_affect_phase([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase);
            hysteresis.Update(1 - phase, magnitude: 0.6);

            Expect(hysteresis.Phase, Is.EqualTo(phase));
        }

        [Test]
        public void Update_of_large_magnitude_affects_phase([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase);
            hysteresis.Update(1 - phase, magnitude: 0.7);

            Expect(hysteresis.Phase, Is.EqualTo(1 - phase));
        }

        [Test]
        public void Two_small_magnitude_updates_accumulate_to_a_phase_transition([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase);

            hysteresis.Update(1 - phase, magnitude: 0.4);
            Expect(hysteresis.Phase, Is.EqualTo(phase));

            hysteresis.Update(1 - phase, magnitude: 0.3);
            Expect(hysteresis.Phase, Is.EqualTo(1 - phase));
        }

        [Test]
        public void Two_opposite_updates_leave_new_phase_when_second_update_magnitude_is_small([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase);
            hysteresis.Update(1 - phase, magnitude: 0.7);
            hysteresis.Update(phase, magnitude: 0.3);

            Expect(hysteresis.Phase, Is.EqualTo(1 - phase));
        }

        [Test]
        public void Two_opposite_updates_return_old_phase_when_second_update_magnitude_is_large([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase);
            hysteresis.Update(1 - phase, magnitude: 0.7);
            hysteresis.Update(phase, magnitude: 0.4);

            Expect(hysteresis.Phase, Is.EqualTo(phase));
        }

        [Test]
        public void Weak_reverse_update_after_two_strong_direct_updates_doesnt_revert_phase([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase);
            hysteresis.Update(1 - phase, magnitude: 0.7);
            hysteresis.Update(1 - phase, magnitude: 0.3);
            hysteresis.Update(phase, magnitude: 0.6);

            Expect(hysteresis.Phase, Is.EqualTo(1 - phase));
        }

        [Test]
        public void Strong_reverse_update_after_two_strong_direct_updates_reverts_phase([Values(0, 1)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 2);

            hysteresis.Initialize(phase: 0);
            hysteresis.Update(1 - phase, magnitude: 0.7);
            hysteresis.Update(1 - phase, magnitude: 0.7);
            hysteresis.Update(phase, magnitude: 0.7);

            Expect(hysteresis.Phase, Is.EqualTo(phase));
        }

        [Test]
        public void Strong_update_towards_middle_phase_doesnt_overshoot_to_far_phase([Values(0, 2)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 3);

            hysteresis.Initialize(phase);

            hysteresis.Update(phase: 1, magnitude: 1);
            Expect(hysteresis.Phase, Is.EqualTo(1));
        }

        [Test]
        public void Updating_towards_the_present_state_draws_to_equilibrium_enough_to_prevent_subsequent_phase_transition([Values(0, 2)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 3);

            hysteresis.Initialize(phase);

            hysteresis.Update(phase: 1, magnitude: 0.6);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.2);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(phase: 1, magnitude: 0.1);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.1);
            Expect(hysteresis.Phase, Is.EqualTo(1));
        }

        [Test]
        public void Updating_towards_the_present_state_draws_to_equilibrium_not_enough_to_prevent_subsequent_phase_transition([Values(0, 2)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 3);

            hysteresis.Initialize(phase);

            hysteresis.Update(phase: 1, magnitude: 0.6);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.2);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(phase: 1, magnitude: 0.1);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.2);
            Expect(hysteresis.Phase, Is.EqualTo(2 - phase));
        }

        [Test]
        public void Updating_towards_the_present_state_sets_state_to_equilibrium_which_is_enough_to_prevent_subsequent_phase_transition([Values(0, 2)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 3);

            hysteresis.Initialize(phase);

            hysteresis.Update(phase: 1, magnitude: 0.6);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.2);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(phase: 1, magnitude: 0.3);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.2);
            Expect(hysteresis.Phase, Is.EqualTo(1));
        }

        [Test]
        public void Updating_towards_the_present_state_sets_state_to_equilibrium_which_is_not_enough_to_prevent_subsequent_phase_transition([Values(0, 2)] int phase)
        {
            var hysteresis = new Hysteresis(range: 1, phaseCount: 3);

            hysteresis.Initialize(phase);

            hysteresis.Update(phase: 1, magnitude: 0.6);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.2);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(phase: 1, magnitude: 0.3);
            Expect(hysteresis.Phase, Is.EqualTo(1));

            hysteresis.Update(2 - phase, magnitude: 0.3);
            Expect(hysteresis.Phase, Is.EqualTo(2 - phase));
        }
    }
}
