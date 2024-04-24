using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Character
    {
        public int level = 0;
        public string name = "";
        public string job = "";
        public float attackValue = 0;
        public float adAttackValue = 0;
        public float defenseValue = 0;
        public float adDefenseValue = 0;
        public int hitPoint = 0;
        public int gold = 0;

        public Character(
            int Level, 
            string Name, 
            string Job,
            float AttacValue,
            float AdAttackValue,
            float DefenseValue,
            float AdDefenseValue,
            int HitPoint,
            int Gold
            ) 
        {
            level = Level;
            name = Name;
            job = Job;
            attackValue = AttacValue;
            adAttackValue = AdAttackValue;
            defenseValue = DefenseValue;
            adDefenseValue = AdDefenseValue;
            hitPoint = HitPoint;
            gold = Gold;
        }


    }
}
