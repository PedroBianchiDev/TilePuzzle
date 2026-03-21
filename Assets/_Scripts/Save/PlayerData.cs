using System;
using System.Collections.Generic;

namespace TilePuzzle.Save
{
    [Serializable]
    public class PlayerData
    {
        public int gold;
        public List<string> boughtLevels = new();
    }
}