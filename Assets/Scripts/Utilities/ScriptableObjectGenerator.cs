using UnityEditor;
using UnityEngine;

namespace WFC.Utilities
{
    public class ScriptableObjectGenerator
    {
        #if UNITY_EDITOR
        
        private const string BASE_PATH = "Assets/ScriptableObjects/";
        private const string ASSET_EXTENSION = ".asset";
        
        public static void SaveAsset(string path, ScriptableObject scriptableObject)
        {
            path = BASE_PATH + path + ASSET_EXTENSION;
            AssetDatabase.CreateAsset(scriptableObject, path);
            AssetDatabase.SaveAssets();
        }
        
        #endif
    }
}