using WFC.Utilities;

namespace WFC.Modules
{
    [System.Serializable]
    public class ModuleData
    {
        public string Key;
        public int Number;
        public int Frequency = 1;
        public Rotation Rotation;
        public ModuleController ModuleController;
        public PersistentPossibleNeighbors PersistentPossibleNeighbors;

        public ModuleData(ModuleData moduleData)
        {
            Key = moduleData.Key;
            Number = moduleData.Number;
            Frequency = moduleData.Frequency;
            Rotation = moduleData.Rotation;
            ModuleController = moduleData.ModuleController;
            PersistentPossibleNeighbors = moduleData.PersistentPossibleNeighbors;
        }
        
        public ModuleData(ModuleController moduleController, Rotation rotation, int numberModuleData)
        {
            Key = numberModuleData + "_" +moduleController.name + "_" + rotation;
            Number = numberModuleData;
            Rotation = rotation;
            ModuleController = moduleController;
            PersistentPossibleNeighbors = new PersistentPossibleNeighbors();
        }
        
        public void AddPossibleNeighbor(Direction direction, int possibleNeighbor)
        {
            PersistentPossibleNeighbors.PossibleNeighbors[direction].Add(possibleNeighbor);
        }
    }
}