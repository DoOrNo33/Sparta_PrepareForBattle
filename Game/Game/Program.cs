namespace Game
{
    internal class Program
    {


        static void Main(string[] args)
        {
            bool gameOn = true;

            //MakeCharacter();
            Character character = new Character();
            character.level = 1;
            character.name = "Jack";
            character.job = "전사";
            character.attackValue = 10;
            character.defenseValue = 5;
            character.hitPoint = 100;
            character.gold = 1500;

            do
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int firstAction = GameStart();

                switch (firstAction)
                {
                    case 1:                                 // 1. 상태 보기
                        Console.Clear();
                        Console.WriteLine("상태 보기");
                        Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                        Console.WriteLine("Lv. {0:D2}", character.level);
                        Console.WriteLine($"{character.name} ( {character.job} )");
                        Console.WriteLine($"공격력 : {character.attackValue}");
                        Console.WriteLine($"방어력 : {character.defenseValue}");
                        Console.WriteLine($"체 력 : {character.hitPoint}");
                        Console.WriteLine($"Gold : {character.gold} G");
                        Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                        Console.Write(">>");

                        int statusAction = SelectStatusAction();  // 상태창 액션 선택

                        break;
                    case 2:
                        Console.WriteLine("2. 인벤토리를 골랐습니다.");
                        break;
                    case 3:
                        Console.WriteLine("3. 상점을 골랐습니다.");
                        gameOn = false;
                        break;
                }
            }
            while (gameOn);
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

        static public int SelectStatusAction()
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
                else if (select < 0 || select > 0)
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
        public int level = 0;
        public string name = "";
        public string job = "";
        public int attackValue = 0;
        public int defenseValue = 0;
        public int hitPoint = 0;
        public int gold = 0;

    }
}
