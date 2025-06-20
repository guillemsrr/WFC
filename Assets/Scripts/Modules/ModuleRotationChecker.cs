using System.Collections.Generic;
using WFC.Utilities;

namespace WFC.Modules
{
    public class ModuleRotationChecker
    {
        private readonly Rotation[] _rotations = { Rotation.Rot90, Rotation.Rot180, Rotation.Rot270 };
        private ModuleController _moduleController;
        public List<Rotation> GetAllDifferentRotations(ModuleController moduleController)
        {
            _moduleController = moduleController;
            _moduleController.InitializeDirectionsDictionary();
            
            List<Rotation> rotations = new List<Rotation>();
            rotations.Add(Rotation.Rot0);

            foreach (Rotation rotation in _rotations)
            {
                if (!DuplicatedRotation(rotation, rotations))
                {
                    rotations.Add(rotation);
                }
            }

            return rotations;
        }

        private bool DuplicatedRotation(Rotation newRotation, List<Rotation> rotations)
        {
            foreach (Rotation differentRotation in rotations)
            {
                if (!AreAnyOfModuleFacesDifferent(differentRotation, newRotation))
                {
                    return true;
                }
            }

            return false;
        }

        private bool AreAnyOfModuleFacesDifferent(Rotation differentRotation, Rotation newRotation)
        {
            foreach (Direction horizontalDirection in Directions.HorizontalDirections)
            {
                Face differentFace = _moduleController.FacesByDirection[Directions.RotateDirection(horizontalDirection, differentRotation)];
                Face newFace = _moduleController.FacesByDirection[Directions.RotateDirection(horizontalDirection, newRotation)];

                if (differentFace.TypeNumber != newFace.TypeNumber) return true;
            }

            return false;
        }
    }
}