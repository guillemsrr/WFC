using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using WFC.ScriptableObjects;
using WFC.Utilities;

namespace WFC.Modules
{
    public class ModuleDataBuilder : MonoBehaviour
    {
#if UNITY_EDITOR

        private const string MODULE_DATA_PATH = "ModuleData/";

        [SerializeField] private ModulesDataSO[] _modulesDataSo;
        [SerializeField] private ModuleController[] _moduleControllers;
        [SerializeField] private string _modulesDataName;
        [SerializeField] private List<ModuleController> _perimeterModuleConstraints;

        private readonly ModuleRotationChecker _moduleRotationChecker = new ModuleRotationChecker();
        private readonly ModuleConnectionChecker _moduleConnectionChecker = new ModuleConnectionChecker();
        private List<int> _perimeterModuleNumbers;

        public void CreateModulesData()
        {
            _perimeterModuleNumbers = new List<int>();
            List<ModuleData> moduleDatas = ExtractDataFromModules();
            SetPossibleNeighbors(moduleDatas);
            SaveModuleDatas(moduleDatas);
            
            EditorUtility.SetDirty(this);
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
        }

        private List<ModuleData> ExtractDataFromModules()
        {
            int numberModuleData = 0;
            List<ModuleData> moduleDatas = new List<ModuleData>();
            foreach (ModuleController moduleController in _moduleControllers)
            {
                List<Rotation> differentRotations = _moduleRotationChecker.GetAllDifferentRotations(moduleController);
                foreach (Rotation rotation in differentRotations)
                {
                    ModuleData moduleData = new ModuleData(moduleController, rotation, numberModuleData);
                    moduleDatas.Add(moduleData);

                    if (_perimeterModuleConstraints.Contains(moduleController))
                    {
                        _perimeterModuleNumbers.Add(numberModuleData);
                    }
                    numberModuleData++;
                }
            }

            return moduleDatas;
        }

        private void SetPossibleNeighbors(List<ModuleData> moduleDatas)
        {
            foreach (ModuleData moduleData in moduleDatas)
            {
                foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                {
                    foreach (ModuleData possibleNeighbor in moduleDatas)
                    {
                        if (_moduleConnectionChecker.CanConnect(moduleData, possibleNeighbor, direction))
                        {
                            moduleData.AddPossibleNeighbor(direction, possibleNeighbor.Number);
                        }
                    }
                }
            }
        }

        private void SaveModuleDatas(List<ModuleData> moduleDatas)
        {
            if (CheckAlreadyCreatedModulesData(moduleDatas))
            {
                return;
            }
            
            ModulesDataSO modulesDataSo = ScriptableObject.CreateInstance<ModulesDataSO>();
            modulesDataSo.SetData(moduleDatas.ToArray());
            modulesDataSo.SetPerimeterConstraints(_perimeterModuleNumbers);
            ScriptableObjectGenerator.SaveAsset(MODULE_DATA_PATH + _modulesDataName , modulesDataSo);
        }

        private bool CheckAlreadyCreatedModulesData(List<ModuleData> moduleDatas)
        {
            foreach (ModulesDataSO dataSo in _modulesDataSo)
            {
                if (dataSo.name == _modulesDataName)
                {
                    dataSo.SetData(moduleDatas.ToArray());
                    dataSo.SetPerimeterConstraints(_perimeterModuleNumbers);
                    return true;
                }
            }

            return false;
        }
    #endif
    }
}