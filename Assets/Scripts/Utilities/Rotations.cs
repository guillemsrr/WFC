using System.Collections.Generic;
using UnityEngine;

namespace WFC.Utilities
{
    public enum Rotation
    {
        Rot0,
        Rot90,
        Rot180,
        Rot270,
    }
    
    public class Rotations
    {
        public static Dictionary<Rotation, Quaternion> QuaternionByRotation = new Dictionary<Rotation, Quaternion>
        {
            [Rotation.Rot0] = Quaternion.identity,
            [Rotation.Rot90] = Quaternion.Euler(0, 90, 0),
            [Rotation.Rot180] = Quaternion.Euler(0, 180, 0),
            [Rotation.Rot270] = Quaternion.Euler(0, 270, 0)
        };
    }
}