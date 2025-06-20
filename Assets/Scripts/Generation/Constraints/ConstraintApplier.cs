using System.Collections.Generic;
using UnityEngine;
using WFC.Generation.Cells;

namespace WFC.Generation.Constraints
{
    public interface ConstraintApplier
    {
        void ApplyConstraint(Dictionary<Vector3Int, CellController> cells);
    }
}