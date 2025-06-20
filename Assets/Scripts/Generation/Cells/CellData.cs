using System.Collections.Generic;
using WFC.Modules;

namespace WFC.Generation.Cells
{
    public class CellData
    {
        private List<ModuleData> _possibleModules;
        public int TotalWeight { get; set; }
        public float SumOfLogWeight { get; set; }
        public bool IsErroneus { get; set; }

        public CellData(ModuleData[] moduleDatas)
        {
            _possibleModules = new List<ModuleData>(moduleDatas);
        }

        public CellData(CellData cellData)
        {
            _possibleModules = new List<ModuleData>(cellData.PossibleModules);
            TotalWeight = cellData.TotalWeight;
            SumOfLogWeight = cellData.SumOfLogWeight;
            IsErroneus = cellData.IsErroneus;
            CollapsedModuleData = cellData.CollapsedModuleData;
        }

        public ModuleData CollapsedModuleData { get; set; }
        public List<ModuleData> PossibleModules => _possibleModules;
    }
}