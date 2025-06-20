using UnityEditor;
using UnityEngine;
using WFC.Modules;

namespace WFC.Editor
{
    [CustomEditor(typeof(ModuleDataBuilder))]
    public class ModuleDataBuilderEditor: UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            ModuleDataBuilder moduleDataBuilder = (ModuleDataBuilder)target;

            if (GUILayout.Button("Extract Modules Data"))
            {
                moduleDataBuilder.CreateModulesData();
            }
        }
    }
}