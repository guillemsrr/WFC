using System.Collections.Generic;
using UnityEngine;
using WFC.Modules;
using WFC.Utilities;

namespace WFC.Generation.Cells
{
    public class CellController
    {
        private Transform _parent;
        private float _entropyNoise;
        private float _size;

        public delegate void CellPropagated(CellController cell);

        public event CellPropagated CellPropagatedEvent;

        public Vector3Int Position { get; }
        public bool CanCollapse => !IsCollapsed && HasPossibilities;
        public bool HasPossibilities => CellData.PossibleModules.Count != 0;

        public bool IsCollapsed => CellData.CollapsedModuleData != null;
        public bool OnlyOnePossibility => CellData.PossibleModules.Count == 1;
        public List<ModuleData> Possibilities => CellData.PossibleModules;

        public CellData CellData { get; set; }

        public CellController(Transform parent, ModuleData[] moduleDatas, Vector3Int position, float size)
        {
            _parent = parent;
            Position = position;
            _size = size;

            CellData = new CellData(moduleDatas);
        }

        public void Collapse()
        {
            CellData.CollapsedModuleData = GetWeightedRandomModule();
        }

        public void InstantiateModule()
        {
            Vector3 position = new Vector3(Position.x, Position.y, Position.z);
            position *= _size;
            position += _parent.position;
            
            ModuleController collapsedModule = Object.Instantiate(CellData.CollapsedModuleData.ModuleController,
                position,
                Rotations.QuaternionByRotation[CellData.CollapsedModuleData.Rotation], _parent);
            collapsedModule.transform.name += Position + " " + CellData.CollapsedModuleData.Number.ToString();
        }

        private ModuleData GetWeightedRandomModule()
        {
            int randomWeight = Random.Range(0, CellData.TotalWeight + 1);
            foreach (ModuleData possibleModule in CellData.PossibleModules)
            {
                randomWeight -= possibleModule.Frequency;
                if (randomWeight <= 0)
                {
                    return possibleModule;
                }
            }

            return CellData.PossibleModules[0];
        }

        public bool Propagate(Direction direction, int collapsedModuleDataNumber)
        {
            Queue<ModuleData> impossibleModules = new Queue<ModuleData>();
            foreach (ModuleData possibleModule in CellData.PossibleModules)
            {
                if (!possibleModule.PersistentPossibleNeighbors.PossibleNeighbors[Directions.FlipDirection(direction)]
                        .Contains(collapsedModuleDataNumber))
                {
                    impossibleModules.Enqueue(possibleModule);
                }
            }

            bool changed = impossibleModules.Count != 0;
            foreach (ModuleData impossibleModule in impossibleModules)
            {
                RemoveImpossibleModule(impossibleModule);
            }

            if (CellData.PossibleModules.Count == 0)
            {
                CellData.IsErroneus = true;
            }

            if (changed)
            {
                CellPropagatedEvent?.Invoke(this);
            }

            return changed;
        }

        private void RemoveImpossibleModule(ModuleData impossibleModule)
        {
            CellData.PossibleModules.Remove(impossibleModule);
            CellData.TotalWeight -= impossibleModule.Frequency;
            CellData.SumOfLogWeight -= Mathf.Log(impossibleModule.Frequency);
        }

        public float GetEntropy()
        {
            float entropy = Mathf.Log(CellData.TotalWeight) - CellData.SumOfLogWeight / CellData.TotalWeight +
                            _entropyNoise;
            if (float.IsNaN(entropy))
            {
                return 0;
            }

            return entropy;
        }

        public void SetWeightData(int totalWeight, float logWeight, float noise)
        {
            CellData.TotalWeight = totalWeight;
            CellData.SumOfLogWeight = logWeight;
            _entropyNoise = noise;
        }
    }
}