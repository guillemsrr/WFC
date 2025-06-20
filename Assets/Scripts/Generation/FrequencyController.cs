using System.Collections.Generic;
using UnityEngine;
using WFC.Generation.Cells;
using WFC.Modules;

namespace WFC.Generation
{
    public class FrequencyController
    {
        private const int HIGH_FREQUENCY = 50;
        
        public void SetOneRandomElementHighFrequency(ModuleData[] moduleDatas)
        {
            int randomIndex = Random.Range(0, moduleDatas.Length);
            SetSpecificElementHighFrequency(moduleDatas, randomIndex);
        }
        
        public void SetSpecificElementRandomFrequency(ModuleData[] moduleDatas, int index)
        {
            moduleDatas[index].Frequency = Random.Range(0, HIGH_FREQUENCY);
        }

        public void SetSpecificElementHighFrequency(ModuleData[] moduleDatas, int index)
        {
            moduleDatas[index].Frequency = HIGH_FREQUENCY;
        }

        public void SetRandomFrequencies(ModuleData[] moduleDatas)
        {
            foreach (ModuleData moduleData in moduleDatas)
            {
                moduleData.Frequency = Random.Range(0, 10);
            }
        }

        public void CalculateInitialWeight(ICollection<CellController> cells)
        {
            foreach (CellController cell in cells)
            {
                CalculateInitialWeight(cell);
            }
        }

        private void CalculateInitialWeight(CellController cellController)
        {
            int totalWeight = 0;
            float logWeight = 0;
            
            foreach (ModuleData possibleModule in cellController.Possibilities)
            {
                totalWeight += possibleModule.Frequency;
                logWeight += Mathf.Log(possibleModule.Frequency);
            }

            float noise = Random.value / 100f;
            
            cellController.SetWeightData(totalWeight, logWeight, noise);
        }
    }
}