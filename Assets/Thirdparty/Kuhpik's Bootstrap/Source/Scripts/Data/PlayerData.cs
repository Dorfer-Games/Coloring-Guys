using NaughtyAttributes;
using System;

namespace Kuhpik
{
    /// <summary>
    /// Used to store player's data. Change it the way you want.
    /// </summary>
    [Serializable]
    public class PlayerData
    {
        public int level;
        public int money;
        public int numberIterationLevels;
        public int lastIterationLevels;
        public int RateUs;
        public string RateUsDateTime;
        public int countOpensItemStore;
    }
}