using System;
using System.Collections.Generic;
using UnityEngine;

namespace WFC.Utilities
{
    public enum Direction
    {
        Left = 0,
        Down = 1,
        Back = 2,
        Right = 3,
        Up = 4,
        Front = 5
    }
    
    public class Directions
    {
        public static Dictionary<Direction, Vector3Int> DirectionsByVectors = new Dictionary<Direction, Vector3Int>
        {
            [Direction.Down] = Vector3Int.down,
            [Direction.Up] = Vector3Int.up,
            [Direction.Left] = Vector3Int.left,
            [Direction.Right] = Vector3Int.right,
            [Direction.Front] = Vector3Int.forward,
            [Direction.Back] = Vector3Int.back
        };

        public static Direction[] HorizontalDirections =
        {
            Direction.Front,
            Direction.Left,
            Direction.Back,
            Direction.Right
        };

        public static Direction RotateDirection(Direction direction, Rotation rotation)
        {
            if (IsVertical(direction))
            {
                return direction;
            }

            int index = Array.IndexOf(HorizontalDirections, direction) + (int)rotation;
            return HorizontalDirections[index% HorizontalDirections.Length];
        }

        public static Direction FlipDirection(Direction direction)
        {
            return (Direction)(((int)direction + 3) % 6);
        }

        private static bool IsVertical(Direction direction)
        {
            return direction == Direction.Up || direction == Direction.Down;
        }
    }
}