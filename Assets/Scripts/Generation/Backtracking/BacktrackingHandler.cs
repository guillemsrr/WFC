using System.Collections.Generic;
using WFC.Generation.Cells;
using WFC.Generation.Waves;

namespace WFC.Generation.Backtracking
{
    public class BacktrackingHandler
    {
        private const int MAX_BACKTRACKING_TRIES = 100;
        
        private WaveFunctionCollapse _waveFunctionCollapse;
        private List<TrackingState> _trackingStates;
        private int _numberTries;

        public BacktrackingHandler(WaveFunctionCollapse waveFunctionCollapse)
        {
            _waveFunctionCollapse = waveFunctionCollapse;
            _trackingStates = new List<TrackingState>();
        }

        public void DiscardCurrentState()
        {
            _numberTries++;

            if (_numberTries > MAX_BACKTRACKING_TRIES)
            {
                Restart();
            }
            _waveFunctionCollapse.SetState(_trackingStates[_trackingStates.Count - 1]);
        }

        public void AddState(Wave wave, List<CellController> uncollapsedCells, EntropyHeap entropyHeap)
        {
            TrackingState state = new TrackingState(wave, uncollapsedCells, entropyHeap);
            _trackingStates.Add(state);
        }

        private void Restart()
        {
            TrackingState firstState = _trackingStates[0];
            _trackingStates.Clear();
            _trackingStates.Add(firstState);
        }
    }
}