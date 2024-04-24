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

        public Dungeon(string Level, float Value, int Reward)
        {
            dungeonLevel = Level;
            recommandDefenseValue=Value;
            clearReward = Reward;
        }

    }
}
