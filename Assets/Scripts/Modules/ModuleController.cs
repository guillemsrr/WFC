using System.Collections.Generic;
using UnityEngine;
using WFC.Utilities;

namespace WFC.Modules
{
    public class ModuleController : MonoBehaviour
    {
        [SerializeField] private Face _frontFace;
        [SerializeField] private Face _backFace;
        [SerializeField] private Face _leftFace;
        [SerializeField] private Face _rightFace;
        [SerializeField] private Face _upperFace;
        [SerializeField] private Face _downFace;

        public Dictionary<Direction, Face> FacesByDirection { get; private set; }
        
        public void InitializeDirectionsDictionary()
        {
            FacesByDirection = new Dictionary<Direction, Face>
            {
                [Direction.Front] = _frontFace,
                [Direction.Back] = _backFace,
                [Direction.Left] = _leftFace,
                [Direction.Right] = _rightFace,
                [Direction.Up] = _upperFace,
                [Direction.Down] = _downFace
            };
        }
    }
}