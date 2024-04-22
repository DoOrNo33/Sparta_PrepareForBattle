namespace Game
{
    internal class Program
    {


        static void Main(string[] args)
        {
            bool gameOn = true;                 // 게임 구동
            
                                                // 아이템 클래스 생성
            Item noviceArmor = new Item();
            noviceArmor.SetItem("수련자 갑옷     ", 2, 5  , "수련에 도움을 주는 갑옷입니다.                   ", 1000);
            Item ironArmor = new Item();
            ironArmor.SetItem("무쇠갑옷        ", 2, 9  , "무쇠로 만들어져 튼튼한 갑옷입니다.               ", 1800);
            Item spartanArmor = new Item();
            spartanArmor.SetItem("스파르타의 갑옷 ", 2, 15 , "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500);
            Item wornSword = new Item();
            wornSword.SetItem("낡은 검         ", 1, 2  , "쉽게 볼 수 있는 낡은 검 입니다.                  ", 600);
            Item bronzeAxe = new Item();
            bronzeAxe.SetItem("청동 도끼       ", 1, 5  , "어디선가 사용됐던거 같은 도끼입니다.             ", 1500);
            Item spartanSpear = new Item();
            spartanSpear.SetItem("스파르타의 창   ", 1, 7  , "스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 2700);

            List<Item> shopItems = new List<Item>();    // 상점 목록
            shopItems.Add(noviceArmor);
            shopItems.Add(ironArmor);
            shopItems.Add(spartanArmor);
            shopItems.Add(wornSword);
            shopItems.Add(bronzeAxe);
            shopItems.Add(spartanSpear);

            List<Item> inventoryItems = new List<Item>();



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
                Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점\n0. 게임 종료");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int firstAction = GameStart();

                switch (firstAction)
                {
                    case 0:
                        Console.WriteLine("게임을 종료합니다.");
                        gameOn = false;
                        break;
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
                        Console.Clear();
                        Console.WriteLine("인벤토리");
                        Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n");
                        // 인벤토리 리스트
                        Console.WriteLine("\n1. 장착 관리\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                        Console.Write(">>");

                        int inventoryAction = SelectInventoryAction();  // 인벤토리창 액션 선택

                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("상점");
                        Console.WriteLine("필요한 아이템을 얻을 수 잇는 상점입니다.\n\n[보유 골드]");
                        Console.WriteLine($"{character.gold} G\n");
                        Console.WriteLine("[아이템 목록]");
                        foreach (Item name in shopItems)              // 상점 아이템 리스트출력
                        {
                            if (!name.get)                              // 아이템 팔렸나?
                            {
                                if (name.type == 1)                 // 무기
                                {
                                    Console.WriteLine($"- {name.name}| 공격력 +{name.value}  | {name.information}| {name.price} G");
                                }

                                else if (name.type == 2)                //방어구
                                {
                                    if (name.value > 9)            
                                    {
                                        Console.WriteLine($"- {name.name}| 방어력 +{name.value} | {name.information}| {name.price} G");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"- {name.name}| 방어력 +{name.value}  | {name.information}| {name.price} G");
                                    }
                                }
                            }
                            else
                            {
                                if (name.type == 1)                 // 무기
                                {
                                    Console.WriteLine($"- {name.name}| 공격력 +{name.value}  | {name.information}| 구매완료");
                                }

                                else if (name.type == 2)                //방어구
                                {
                                    if (name.value > 9)
                                    {
                                        Console.WriteLine($"- {name.name}| 방어력 +{name.value} | {name.information}| 구매완료");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"- {name.name}| 방어력 +{name.value}  | {name.information}| 구매완료");
                                    }
                                }
                            }


                        }
                        // 상점 리스트
                        Console.WriteLine("\n1. 아이템 구매\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                        Console.Write(">>");

                        int shopAction = SelectInventoryAction();  // 상점 액션 선택

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
                else if (select < 0 || select > 3)
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

        static public int SelectInventoryAction()
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
                else if (select < 0 || select > 1)
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
    
    public class Item
    {
        public string name = "";
        public int type = 0; // 1: 무기, 2: 방어구
        public int value = 0;
        public string information = "";
        public int price = 0;
        public bool get = false;

        public void SetItem(string Name, int Type, int Value, string Information, int Price)
        {
            name = Name;
            type = Type;
            value = Value;
            information = Information;
            price = Price;
        }
    }
}
