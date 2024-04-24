using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class GameManager
    {
        private static GameManager Instance;
        public static GameManager instance
        {
            get
            {
                if (Instance == null)
                {
                    Instance = new GameManager();
                    Instance.character = new Character(1, "Jack", "전사", 10f, 10f, 5f, 5f, 100, 1500);
                }
                return Instance;
            }
        }

        public Character character;

        public List<Item> myItems = new List<Item>();

        public Dungeon[] dungeons = new Dungeon[3];    // 던전 관리

        public int SelectNumber(int number)
        {
            int select = 0;
            bool isNumber;
            do
            {
                string input = Console.ReadLine();
                isNumber = int.TryParse(input, out select);
                if (!isNumber)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                }
                else if (select < 0 || select > number)
                {
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.Write(">>");
                    isNumber = false;
                }
            }
            while (!isNumber);

            if (isNumber)
            {
                return select;
            }
            else
            {
                return select;
            }
        }
    }
}
