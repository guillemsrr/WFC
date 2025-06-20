using System.Collections.Generic;
using WFC.Generation.Cells;
using WFC.Generation.Waves;

namespace WFC.Generation.Backtracking
{
    public class BacktrackingHandler
    {
        private const int MAX_RESTARTS = 10;

        private WaveFunctionCollapse _waveFunctionCollapse;
        private List<TrackingState> _trackingStates;
        private int _numberRestarts;

        public bool CanRestart => _numberRestarts < MAX_RESTARTS;

        public BacktrackingHandler(WaveFunctionCollapse waveFunctionCollapse)
        {
            _waveFunctionCollapse = waveFunctionCollapse;
            _trackingStates = new List<TrackingState>();
        }

        public void DiscardCurrentState()
        {
            int lastIndex = _trackingStates.Count - 1;
            if (lastIndex == 0)
            {
                _numberRestarts++;
            }

            _waveFunctionCollapse.SetState(_trackingStates[lastIndex]);
        }

        public void AddState(Wave wave, List<CellController> uncollapsedCells, EntropyHeap entropyHeap)
        {
            TrackingState state = new TrackingState(wave, uncollapsedCells, entropyHeap);
            _trackingStates.Add(state);
        }

        public void Restart()
        {
            TrackingState firstState = _trackingStates[0];
            _trackingStates.Clear();
            _trackingStates.Add(firstState);
        }
    }
}