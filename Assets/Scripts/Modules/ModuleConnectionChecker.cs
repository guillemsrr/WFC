using WFC.Utilities;

namespace WFC.Modules
{
    public class ModuleConnectionChecker
    {
        public bool CanConnect(ModuleData moduleData, ModuleData possibleNeighbor, Direction direction)
        {
            Direction neighborDirection = Directions.FlipDirection(direction);
            neighborDirection = Directions.RotateDirection(neighborDirection, possibleNeighbor.Rotation);
            direction = Directions.RotateDirection(direction, moduleData.Rotation);

            return moduleData.ModuleController.FacesByDirection[direction].TypeNumber == possibleNeighbor.ModuleController.FacesByDirection[neighborDirection].TypeNumber;
        }
    }
}