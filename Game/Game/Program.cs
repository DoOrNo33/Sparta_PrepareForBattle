namespace Game
{
    internal class Program
    {


        static void Main(string[] args)
        {
            //MakeCharacter();

            Character character = new Character();
            character.Level = 1;
            character.Name = "Jack";
            character.Job = "전사";
            character.AttackValue = 10;
            character.DefenseValue = 5;
            character.HitPoint = 100;
            character.Gold = 1500;

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int firstAction = GameStart();
            
            switch(firstAction)
            {
                case 1:                                 // 1. 상태 보기
                    Console.Clear();
                    Console.WriteLine("상태 보기");
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                    Console.WriteLine("Lv. {0:D2}", character.Level);
                    Console.WriteLine($"{character.Name} ( {character.Job} )");
                    Console.WriteLine($"공격력 : {character.AttackValue}");
                    Console.WriteLine($"방어력 : {character.DefenseValue}");
                    Console.WriteLine($"체 력 : {character.HitPoint}");
                    Console.WriteLine($"Gold : {character.Gold} G");
                    Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                    Console.Write(">>");

                    int statusAction = int.Parse(Console.ReadLine());


                    break;
                case 2:
                    Console.WriteLine("2. 인벤토리를 골랐습니다.");
                    break;
                case 3:
                    Console.WriteLine("3. 상점을 골랐습니다.");
                    break;
            }
        }

        static public int GameStart()
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
                else if (select < 1 || select > 3)
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

        //static public void MakeCharacter()
        //{
        //    Character character = new Character();
        //    character.Level = 1;
        //    character.Name = "Jack";
        //    character.Job = "전사";
        //    character.AttackValue = 10;
        //    character.DefenseValue = 5;
        //    character.HitPoint = 100;
        //    character.Gold = 1500;

        //}

        static public int ViewStatus()
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
                else if (select < 1 || select > 3)
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

    public class Character
    {
        int level = 0;
        string name = "";
        string job = "";
        int attackValue = 0;
        int defenseValue = 0;
        int hitPoint = 0;
        int gold = 0;

        //public int ViewLevel()
        //{
        //    return level;
        //}

        //public string ViewName()
        //{
        //    return name;
        //}

        //public string ViewJob()
        //{
        //    return job;
        //}

        //public int ViewAttackValue()
        //{
        //    return attackValue;
        //}

        //public int ViewDefenseValue()
        //{
        //    return defenseValue;
        //}

        //public int ViewHitPoint()
        //{
        //    return hitPoint;
        //}

        //public int ViewGold()
        //{
        //    return gold;
        //}

        public int Level
        {
            get { return level; } 
            set {  level = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Job
        {
            get { return job; }
            set { job = value; }
        }

        public int AttackValue
        {
            get { return attackValue; }
            set { attackValue = value; }
        }

        public int DefenseValue
        {
            get { return defenseValue; }
            set { defenseValue = value; }
        }

        public int HitPoint
        {
            get { return hitPoint; }
            set { hitPoint = value; }
        }

        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
    }
}
