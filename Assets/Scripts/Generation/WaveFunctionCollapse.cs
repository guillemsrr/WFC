using System.Collections.Generic;
using UnityEngine;
using WFC.Generation.Backtracking;
using WFC.Generation.Cells;
using WFC.Generation.Waves;
using WFC.Utilities;

namespace WFC.Generation
{
    public class WaveFunctionCollapse
    {
        private readonly Wave _wave;
        private List<CellController> _uncollapsedCells;
        private EntropyHeap _entropyHeap;
        private BacktrackingHandler _backtrackingHandler;


        private int NumberCells => _uncollapsedCells.Count;

        public WaveFunctionCollapse(Wave wave)
        {
            _wave = wave;
            _uncollapsedCells = new List<CellController>(wave.Cells.Values);
            _entropyHeap = new EntropyHeap(wave.Cells.Values);
            _backtrackingHandler = new BacktrackingHandler(this);
        }

        public void Observe()
        {
            while (NumberCells != 0)
            {
                CellController randomCell = _entropyHeap.GetCell();
                if (randomCell == null)
                {
                    _entropyHeap.AddLowestEntropyCell(_uncollapsedCells);
                    randomCell = _entropyHeap.GetCell();
                }

                if (randomCell == null)
                {
                    _backtrackingHandler.DiscardCurrentState();
                    continue;
                }
                Propagate(randomCell);
            }
        }

        private void Collapse(CellController randomCell)
        {
            randomCell.Collapse();
            RemoveCollapsedCell(randomCell);
        }

        private void RemoveCollapsedCell(CellController randomCell)
        {
            _uncollapsedCells.Remove(randomCell);
        }

        private void Propagate(CellController collapsedCell)
        {
            Queue<CellController> cellsToUpdate = new Queue<CellController>();
            cellsToUpdate.Enqueue(collapsedCell);

            while (cellsToUpdate.Count != 0)
            {
                CellController cell = cellsToUpdate.Dequeue();
                Collapse(cell);
                
                foreach (KeyValuePair<Direction, Vector3Int> directionVector in Utilities.Directions.DirectionsByVectors)
                {
                    Vector3Int offset = cell.Position + directionVector.Value;
                    if (!_wave.Cells.ContainsKey(offset))
                    {
                        continue;
                    }

                    CellController propagatedCell = _wave.Cells[offset];
                    if (/*propagatedCell.CellData.IsErroneus || */propagatedCell.IsCollapsed)
                    {
                        continue;
                    }

                    if (!cell.IsCollapsed)
                    {
                        return;
                    }
                
                    propagatedCell.Propagate(directionVector.Key, cell.CellData.CollapsedModuleData.Number);

                    if (propagatedCell.OnlyOnePossibility)
                    {
                        cellsToUpdate.Enqueue(propagatedCell);
                    }

                    if (propagatedCell.CellData.IsErroneus)
                    {
                        _backtrackingHandler.DiscardCurrentState();
                        return;
                    }
                }
            }
            
            _backtrackingHandler.AddState(_wave, _uncollapsedCells, _entropyHeap);
        }

        public void SetState(TrackingState trackingState)
        {
            _wave.SetCellsData(trackingState.WaveData);
            _uncollapsedCells = new List<CellController>(trackingState.UncollapsedCells);
            _entropyHeap = new EntropyHeap(trackingState.EntropyHeap);
        }
    }
}