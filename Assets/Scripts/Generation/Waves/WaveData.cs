using System.Collections.Generic;
using UnityEngine;
using WFC.Generation.Cells;

namespace WFC.Generation.Waves
{
    public class WaveData
    {
        public Dictionary<Vector3Int, CellData> CellDatas = new Dictionary<Vector3Int, CellData>();

        public WaveData(Wave wave)
        {
            foreach (CellController cellController in wave.Cells.Values)
            {
                CellDatas[cellController.Position] = new CellData(cellController.CellData);
            }
        }
    }
}