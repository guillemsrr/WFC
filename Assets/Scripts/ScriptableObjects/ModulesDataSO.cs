using System.Collections.Generic;
using UnityEngine;
using WFC.Modules;

namespace WFC.ScriptableObjects
{
    public class ModulesDataSO:ScriptableObject
    {
        public ModuleData[] ModuleDatas;

        public int[] PerimeterConstraintNumbers;

        public int AirIndex = 0;

        public void SetData(ModuleData[] moduleData)
        {
            ModuleDatas = moduleData;
        }

        public void SetPerimeterConstraints(List<int> perimeterModuleNumbers)
        {
            PerimeterConstraintNumbers = perimeterModuleNumbers.ToArray();
        }
    }
}