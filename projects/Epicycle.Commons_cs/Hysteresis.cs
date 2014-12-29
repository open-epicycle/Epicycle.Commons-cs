using System;
using System.Collections.Generic;
using System.Linq;

namespace Epicycle.Commons
{
    public sealed class Hysteresis
    {
        // 50% overlap between phases
        public Hysteresis(double range, int phaseCount)
        {
            ArgAssert.GreaterThan(range, "range", 0);
            ArgAssert.AtLeast(phaseCount, "phaseCount", 2);

            var downwardsThresholds = new double[phaseCount];
            var upwardsThresholds = new double[phaseCount];           

            var step = range / (phaseCount + 1);

            downwardsThresholds[0] = 0;
            downwardsThresholds[1] = step;

            for (var i = 2; i < phaseCount; i++)
            {
                downwardsThresholds[i] = i * step;
                upwardsThresholds[i - 2] = downwardsThresholds[i];
            }

            upwardsThresholds[phaseCount - 2] = phaseCount * step;
            upwardsThresholds[phaseCount - 1] = range;

            _downwardsThresholds = downwardsThresholds;
            _upwardsThresholds = upwardsThresholds;
            _equilibria = ComputeEquilibria();
        }

        public Hysteresis(IReadOnlyList<double> downwardsThresholds, IReadOnlyList<double> upwardsThresholds)
        {
            _downwardsThresholds = downwardsThresholds;
            _upwardsThresholds = upwardsThresholds;

            ValidateThresholds();

            _equilibria = ComputeEquilibria();
        }
        
        private readonly IReadOnlyList<double> _downwardsThresholds;
        private readonly IReadOnlyList<double> _upwardsThresholds;
        private readonly IReadOnlyList<double> _equilibria;

        private IReadOnlyList<double> ComputeEquilibria()
        {
            var equilibria = new double[PhaseCount];

            equilibria[0] = _downwardsThresholds.First();
            equilibria[PhaseCount - 1] = _upwardsThresholds.Last();

            for (var i = 1; i < PhaseCount - 1; i++)
            {
                equilibria[i] = (_downwardsThresholds[i] + _upwardsThresholds[i]) / 2;
            }

            return equilibria;
        }

        private void ValidateThresholds()
        {
            ArgAssert.AtLeast(_downwardsThresholds.Count, "thresholds.Count", 2);
            ArgAssert.Equal(_downwardsThresholds.Count, "downwardsThreholds.Count", _upwardsThresholds.Count, "upwardsTresholds.Count");

            for (var i = 1; i < PhaseCount; i++)
            {
                ArgAssert.GreaterThan
                    (_upwardsThresholds[i - 1], string.Format("_upwardsThresholds[{0}]", i - 1),
                    _downwardsThresholds[i], string.Format("_downwardsThresholds[{0}]", i));

                ArgAssert.GreaterThan
                    (_upwardsThresholds[i], string.Format("_upwardsThresholds[{0}]", i),
                    _upwardsThresholds[i - 1], string.Format("_upwardsThresholds[{0}]", i - 1));

                ArgAssert.GreaterThan
                    (_downwardsThresholds[i], string.Format("_downwardsThresholds[{0}]", i),
                    _downwardsThresholds[i - 1], string.Format("_downwardsThresholds[{0}]", i - 1));
            }
        }

        private double _state;
        private int? _phase;

        public void Initialize(int phase)
        {
            _phase = phase;
            _state = _equilibria[phase];
        }
        
        public void Initialize(int phase, double state)
        {
            ArgAssert.GreaterThan(state, "state", _downwardsThresholds[phase], string.Format("_downwardsThresholds[{0}]", phase));
            ArgAssert.LessThan(state, "state", _upwardsThresholds[phase], string.Format("_upwardsThresholds[{0}]", phase));

            _phase = phase;
            _state = state;
        }

        public void Reset()
        {
            _phase = null;
        }

        public void Update(int phase, double magnitude)
        { 
            var equilibrium = _equilibria[phase];

            if (_phase.Value == phase)
            {
                if (_state > equilibrium)
                {
                    _state = Math.Max(_state - magnitude, equilibrium);
                }
                else
                {
                    _state = Math.Min(_state + magnitude, equilibrium);
                }
            }
            else if (_phase.Value < phase)
            {
                _state += magnitude;

                while (_state > _upwardsThresholds[_phase.Value])
                {
                    _phase++;

                    if (_phase.Value == phase)
                    {
                        _state = Math.Min(_state, equilibrium);
                        break;
                    }
                }
            }
            else // _phase.Value > phase
            {
                _state -= magnitude;

                while (_state < _downwardsThresholds[_phase.Value])
                {
                    _phase--;

                    if (_phase.Value == phase)
                    {
                        _state = Math.Max(_state, equilibrium);
                        break;
                    }
                }
            }
        }

        public int? Phase
        {
            get { return _phase; }
        }

        public bool IsInitialized
        {
            get { return _phase.HasValue; }
        }

        public int PhaseCount
        {
            get { return _downwardsThresholds.Count; }
        }
    }
}
