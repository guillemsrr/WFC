using System.Collections.Generic;
using WFC.Generation.Cells;
using WFC.Generation.Waves;

namespace WFC.Generation.Backtracking
{
    public class BacktrackingHandler
    {
        private const int MAX_BACKTRACKS = 100;

        private WaveFunctionCollapse _waveFunctionCollapse;
        private List<TrackingState> _trackingStates;
        private int _numberBacktracks;

        public bool CanRestart => _numberBacktracks < MAX_BACKTRACKS;

        public BacktrackingHandler(WaveFunctionCollapse waveFunctionCollapse)
        {
            _waveFunctionCollapse = waveFunctionCollapse;
            _trackingStates = new List<TrackingState>();
        }

        public void DiscardCurrentState()
        {
            int lastIndex = _trackingStates.Count - 1;
            _numberBacktracks++;
            _waveFunctionCollapse.SetState(_trackingStates[lastIndex]);
        }

        public void AddState(Wave wave, List<CellController> uncollapsedCells, EntropyHeap entropyHeap)
        {
            TrackingState state = new TrackingState(wave, uncollapsedCells, entropyHeap);
            _trackingStates.Add(state);
        }
    }
}