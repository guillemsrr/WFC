using System.Collections.Generic;
using WFC.Utilities;

namespace WFC.Modules
{
    [System.Serializable]
    public class PersistentPossibleNeighbors
    {
        public List<int> FrontPossibleNeighbors = new List<int>();
        public List<int> BackPossibleNeighbors = new List<int>();
        public List<int> LeftPossibleNeighbors = new List<int>();
        public List<int> RightPossibleNeighbors = new List<int>();
        public List<int> UpPossibleNeighbors = new List<int>();
        public List<int> DownPossibleNeighbors = new List<int>();

        public Dictionary<Direction, List<int>> PossibleNeighbors = new Dictionary<Direction, List<int>>();

        public PersistentPossibleNeighbors()
        {
            PossibleNeighbors[Direction.Back] = BackPossibleNeighbors;
            PossibleNeighbors[Direction.Front] = FrontPossibleNeighbors;
            PossibleNeighbors[Direction.Left] = LeftPossibleNeighbors;
            PossibleNeighbors[Direction.Right] = RightPossibleNeighbors;
            PossibleNeighbors[Direction.Up] = UpPossibleNeighbors;
            PossibleNeighbors[Direction.Down] = DownPossibleNeighbors;
        }
    }
}