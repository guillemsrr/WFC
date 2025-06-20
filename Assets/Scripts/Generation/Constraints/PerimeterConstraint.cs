using System.Collections.Generic;
using UnityEngine;
using WFC.Generation.Cells;
using WFC.Utilities;

namespace WFC.Generation.Constraints
{
    public class PerimeterConstraint: ConstraintApplier
    {
        private int[] _perimeterModuleNumber;
        private Vector3Int _gridDimensions;
        
        public PerimeterConstraint(int[] perimeterModuleNumber, Vector3Int gridDimensions)
        {
            _perimeterModuleNumber = perimeterModuleNumber;
            _gridDimensions = gridDimensions;
        }

        public void ApplyConstraint(Dictionary<Vector3Int, CellController> cells)
        {
            foreach (KeyValuePair<Vector3Int,CellController> positionCell in cells)
            {
                AxisConstraint(positionCell.Value, positionCell.Key.x, _gridDimensions.x, Direction.Left);
                AxisConstraint(positionCell.Value, positionCell.Key.y, _gridDimensions.y, Direction.Down);
                AxisConstraint(positionCell.Value, positionCell.Key.z, _gridDimensions.z, Direction.Back);
            }
        }

        private void AxisConstraint(CellController cell, int position, int dimension, Direction direction)
        {
            if (position == 0)
            {
                foreach (int id in _perimeterModuleNumber)
                {
                    cell.Propagate(Directions.FlipDirection(direction), id);
                }

            }

            if (position == dimension - 1)
            {
                foreach (int id in _perimeterModuleNumber)
                {
                    cell.Propagate(direction, id);
                }
            }
        }
    }
}