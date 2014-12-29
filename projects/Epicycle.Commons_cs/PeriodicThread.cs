namespace Epicycle.Commons
{
    public sealed class PeriodicThread : BasePeriodicThread
    {
        public interface IUpdatable
        {
            void Update();
        }

        public PeriodicThread(IUpdatable updatable, int delay, int minDelay) : base(delay, minDelay)
        {
            _updatable = updatable;

            Start();
        }

        private readonly IUpdatable _updatable;

        protected override void Iteration()
        {
            _updatable.Update();
        }
    }
}
