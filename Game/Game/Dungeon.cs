using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Dungeon
    {
        public string dungeonLevel = "";
        public float recommandDefenseValue = 0f;
        public int clearReward = 0;

        public void MakeDungeon(string level, float recommand, int reward)
        {
            dungeonLevel = level;
            recommandDefenseValue = recommand;
            clearReward = reward;
        }
    }
}
