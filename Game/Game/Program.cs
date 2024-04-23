using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace Game
{
    internal class Program
    {


        static void Main(string[] args)
        {
            bool gameOn = true;                 // 게임 구동
            bool quitInventory = false;
            bool quitEquipment = false;
            bool shopQuitCheck = false;             
            bool buyShopItemCheck = false;             // 상점 확인
            bool quitSellMyItemCheck = false;
            

            // 아이템 클래스 생성
            Item noviceArmor = new Item();
            noviceArmor.SetItem("수련자 갑옷     ", 2, 5  , "수련에 도움을 주는 갑옷입니다.                   ", 1000, "수련자 갑옷");
            Item ironArmor = new Item();
            ironArmor.SetItem("무쇠갑옷        ", 2, 9  , "무쇠로 만들어져 튼튼한 갑옷입니다.               ", 1800, "무쇠갑옷");
            Item spartanArmor = new Item();
            spartanArmor.SetItem("스파르타의 갑옷 ", 2, 15 , "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, "스파르타의 갑옷");
            Item wornSword = new Item();
            wornSword.SetItem("낡은 검         ", 1, 2  , "쉽게 볼 수 있는 낡은 검 입니다.                  ", 600, "낡은 검");
            Item bronzeAxe = new Item();
            bronzeAxe.SetItem("청동 도끼       ", 1, 5  , "어디선가 사용됐던거 같은 도끼입니다.             ", 1500, "청동 도끼");
            Item spartanSpear = new Item();
            spartanSpear.SetItem("스파르타의 창   ", 1, 7  , "스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 2700, "스파르타의 창");
            Item legendarySword = new Item();
            legendarySword.SetItem("전설의 검       ", 1, 80, "용사를 위한 전설의 검 입니다.                    ", 100, "전설의 검");

            Item[] shopItems = new Item[] { noviceArmor, ironArmor, spartanArmor, wornSword, bronzeAxe, spartanSpear, legendarySword };

            List<Item> myItems = new List<Item>();



            //MakeCharacter();
            Character character = new Character();
            character.level = 1;
            character.name = "Jack";
            character.job = "전사";
            character.attackValue = 10;
            character.defenseValue = 5;
            character.hitPoint = 100;
            character.gold = 5500;

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
                        ViewStatus();
                        break;
                    case 2:
                        quitInventory = false;
                        do
                        {
                            ViewInventory();
                        }
                        while (!quitInventory);

                        break;
                    case 3:
                        shopQuitCheck = false;
                        do
                        {
                            VisitShop();
                        }
                        while (!shopQuitCheck);
                        
                        break;
                }
            }
            while (gameOn);


            void ViewStatus()
            {
                int attackItemValue = 0;
                int defenseItemValue = 0;

                Console.Clear();
                Console.WriteLine("상태 보기");
                Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
                Console.WriteLine("Lv. {0:D2}", character.level);
                Console.WriteLine($"{character.name} ( {character.job} )");
                foreach (Item item in myItems)                                  //무기 꼈는지 체크
                {
                    if (item.type == 1)            
                    {
                        if (item.equip)            
                        {
                            attackItemValue += item.value;
                        }
                    }
                }

                foreach (Item item in myItems)                      // 방어구 아이템 꼈는지 체크
                {
                    if (item.type == 2)             
                    {
                        if (item.equip)           
                        {
                            defenseItemValue += item.value;
                        }
                    }
                }

                if (attackItemValue == 0)
                { 
                    Console.WriteLine($"공격력 : {character.attackValue}");
                }
                else
                {
                    Console.WriteLine($"공격력 : {(character.attackValue + attackItemValue)} (+{attackItemValue})");
                }

                if (defenseItemValue == 0)
                {
                    Console.WriteLine($"방어력 : {character.defenseValue}");
                }
                else
                {
                    Console.WriteLine($"방어력 : {(character.defenseValue + defenseItemValue)} (+{defenseItemValue})");
                }


                Console.WriteLine($"체 력 : {character.hitPoint}");
                Console.WriteLine($"Gold : {character.gold} G");
                Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int statusAction = SelectStatusAction();  // 상태창 액션 선택
            }

            void ViewInventory()
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n");
                foreach (Item name in myItems)
                {
                    if (name.equip)                 // 장착 했다면
                    {
                        if (name.type == 1)                 // 무기
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- [E]{name.name}| 공격력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- [E]{name.name}| 공격력 +{name.value}  | {name.information}");
                            }

                        }

                        else if (name.type == 2)                //방어구
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- [E]{name.name}| 방어력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- [E]{name.name}| 방어력 +{name.value}  | {name.information}");
                            }
                        }
                    }
                    else                                    // 장착 안했다면
                    {
                        if (name.type == 1)                 // 무기
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {name.name}   | 공격력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- {name.name}   | 공격력 +{name.value}  | {name.information}");
                            }
                        }

                        else if (name.type == 2)                //방어구
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {name.name}   | 방어력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- {name.name}   | 방어력 +{name.value}  | {name.information}");
                            }
                        }
                    }
                }
             
                Console.WriteLine("\n1. 장착 관리\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int inventoryAction = SelectInventoryAction();  // 인벤토리창 액션 선택

                switch (inventoryAction)
                {
                    case 0:
                        quitInventory = true;
                        break;
                    case 1:
                        quitEquipment = false;

                        do
                        {
                            EquipItem();                        // 장착 액션
                        }
                        while (!quitEquipment);

                        break;
                }

            }

            void EquipItem()
            {
                Console.Clear();
                Console.WriteLine("인벤토리 - 장착 관리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n");
                int i = 0;                          // 반복 제어
                foreach (Item name in myItems)
                {
                    i++;
                    if (name.equip)                 // 장착 했다면
                    {
                        if (name.type == 1)                 // 무기
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 공격력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 공격력 +{name.value}  | {name.information}");
                            }
                        }

                        else if (name.type == 2)                //방어구
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 방어력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 방어력 +{name.value}  | {name.information}");
                            }
                        }
                    }
                    else                                    // 장착 안했다면
                    {
                        if (name.type == 1)                 // 무기
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} {name.name}   | 공격력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} {name.name}   | 공격력 +{name.value}  | {name.information}");
                            }
                        }

                        else if (name.type == 2)                //방어구
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} {name.name}   | 방어력 +{name.value} | {name.information}");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} {name.name}   | 방어력 +{name.value}  | {name.information}");
                            }
                        }
                    }
                }
                Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int equipAction = SelectNumber(myItems.Count);      // 장착 액션은 아이템 수에 한정

                if (equipAction == 0)
                {
                    quitEquipment = true;
                }
                else if (myItems[equipAction - 1].equip)
                {
                    myItems[equipAction - 1].equip = false;
                }
                else
                {
                    foreach (Item item in myItems)      // 장착되어있는 장비 중 타입이 장착하려는 장비를 찾아서 장착 해제
                    {
                        if (item.equip)
                        {
                            if (item.type == myItems[equipAction - 1].type)
                            {
                                item.equip = false;
                            }
                        }
                    }
                    myItems[equipAction - 1].equip = true;

                }
            }

            void VisitShop()
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 잇는 상점입니다.\n\n[보유 골드]");
                Console.WriteLine($"{character.gold} G\n");
                Console.WriteLine("[아이템 목록]");
                for (int i = 0; i < shopItems.Length; i++)              // 상점 아이템 리스트출력
                {
                    if (!shopItems[i].get)                              // 아이템 팔렸나?
                    {
                        if (shopItems[i].type == 1)                 // 무기
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 공격력 +{shopItems[i].value} | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 공격력 +{shopItems[i].value}  | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                        }

                        else if (shopItems[i].type == 2)                //방어구
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 방어력 +{shopItems[i].value} | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 방어력 +{shopItems[i].value}  | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                        }
                    }
                    else
                    {
                        if (shopItems[i].type == 1)                 // 무기
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 공격력 +{shopItems[i].value} | {shopItems[i].information}| 구매완료");
                            }
                            else
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 공격력 +{shopItems[i].value}  | {shopItems[i].information}| 구매완료");
                            }
                        }

                        else if (shopItems[i].type == 2)                //방어구
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 방어력 +{shopItems[i].value} | {shopItems[i].information}| 구매완료");
                            }
                            else
                            {
                                Console.WriteLine($"- {shopItems[i].name}| 방어력 +{shopItems[i].value}  | {shopItems[i].information}| 구매완료");
                            }
                        }
                    }
                }
                // 상점 리스트
                Console.WriteLine("\n1. 아이템 구매\n2. 아이템 판매\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int shopAction = SelectNumber(2);  // 상점 액션 선택

                switch (shopAction)
                {
                    case 0:
                        shopQuitCheck = true;
                        break;
                    case 1:
                        buyShopItemCheck = false;

                        do
                        {
                            BuyShopItem();
                        }
                        while (!buyShopItemCheck);

                        break;
                    case 2:
                        quitSellMyItemCheck = false;
                        do
                        {
                            SellMyItem();
                        }
                        while (!quitSellMyItemCheck);
                        break;
                }
            }
           
            void BuyShopItem()                        // 상점 아이템 구매 함수
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]");
                Console.WriteLine($"{character.gold} G");
                Console.WriteLine("\n[아이템 목록]");

                for (int i = 0; i < shopItems.Length; i++)              // 상점 아이템 리스트 출력
                {
                    if (!shopItems[i].get)                              // 아이템 팔렸나?
                    {
                        if (shopItems[i].type == 1)                 // 무기
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 공격력 +{shopItems[i].value} | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 공격력 +{shopItems[i].value}  | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                        }

                        else if (shopItems[i].type == 2)                //방어구
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value} | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value}  | {shopItems[i].information}| {shopItems[i].price} G");
                            }
                        }
                    }
                    else
                    {
                        if (shopItems[i].type == 1)                 // 무기
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 공격력 +{shopItems[i].value} | {shopItems[i].information}| 구매완료");
                            }
                            else
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 공격력 +{shopItems[i].value}  | {shopItems[i].information}| 구매완료");
                            }
                        }

                        else if (shopItems[i].type == 2)                //방어구
                        {
                            if (shopItems[i].value > 9)
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value} | {shopItems[i].information}| 구매완료");
                            }
                            else
                            {
                                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value}  | {shopItems[i].information}| 구매완료");
                            }
                        }
                    }
                }
                // 구매 리스트
                Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int buyAction = SelectNumber(shopItems.Length);

                if (buyAction == 0)
                {
                    buyShopItemCheck = true;
                }
                else if (shopItems[buyAction - 1].get)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                }
                else
                {
                    if (character.gold >= shopItems[buyAction - 1].price)
                    {
                        Console.WriteLine("구매를 완료했습니다.\n<Press Any Key>");
                        character.gold -= shopItems[buyAction - 1].price;
                        shopItems[buyAction - 1].get = true;                        // 내 아이템 리스트에 추가
                        myItems.Add(shopItems[buyAction - 1]);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Gold 가 부족합니다.\n<Press Any Key>");
                        Console.ReadLine();
                    }
                }
            }

            void SellMyItem()
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]\n");
                Console.WriteLine($"{character.gold} G\n\n[아이템 목록]");
                int i = 0;
                foreach (Item name in myItems)
                {
                    i++;
                    if (name.equip)                 // 장착 했다면
                    {
                        if (name.type == 1)                 // 무기
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 공격력 +{name.value} | {name.information}|  {name.price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 공격력 +{name.value}  | {name.information}|  {name.price} G");
                            }
                        }

                        else if (name.type == 2)                //방어구
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 방어력 +{name.value} | {name.information}|  {name.price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} [E]{name.name}| 방어력 +{name.value}  | {name.information}|  {name.price} G");
                            }
                        }
                    }
                    else                                    // 장착 안했다면
                    {
                        if (name.type == 1)                 // 무기
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} {name.name}   | 공격력 +{name.value} | {name.information}|  {name.price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} {name.name}   | 공격력 +{name.value}  | {name.information}|  {name.price} G");
                            }
                        }

                        else if (name.type == 2)                //방어구
                        {
                            if (name.value > 9)
                            {
                                Console.WriteLine($"- {i} {name.name}   | 방어력 +{name.value} | {name.information}|  {name.price} G");
                            }
                            else
                            {
                                Console.WriteLine($"- {i} {name.name}   | 방어력 +{name.value}  | {name.information}|  {name.price} G");
                            }
                        }
                    }
                }
                Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int sellAction = SelectNumber(myItems.Count);

                if (sellAction == 0)
                {
                    quitSellMyItemCheck = true;
                }
                else
                {
                    int sellConfirm = 0;
                    Console.WriteLine($"정말 {myItems[sellAction - 1].nickName}을(를) 판매하시겠습니까?\n1. 예\n0. 아니오");
                    Console.Write(">>");
                    sellConfirm = SelectNumber(1);
                    if (sellConfirm == 1)
                    {
                        character.gold += (myItems[sellAction - 1].price * 85 / 100);
                        myItems[sellAction - 1].equip = false;
                        myItems[sellAction - 1].get = false;
                        myItems.RemoveAt(sellAction - 1);
                    }

                }

            }
        }
         //static public void MakeCharacter()
         //   {
         //       Character character = new Character();
         //       character.Level = 1;
         //       character.Name = "Jack";
         //       character.Job = "전사";
         //       character.AttackValue = 10;
         //       character.DefenseValue = 5;
         //       character.HitPoint = 100;
         //       character.Gold = 1500;

         //   }

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

        static public int SelectNumber(int number)
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


        //static void VisitShop()
        //{
        //    Console.Clear();
        //    Console.WriteLine("상점 - 아이템 구매\n필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]");
        //    Console.WriteLine($"{character.gold} G");
        //    Console.WriteLine("\n[아이템 목록]");

        //    for (int i = 0; i < shopItems.Length; i++)              // 상점 아이템 리스트출력
        //    {
        //        if (!shopItems[i].get)                              // 아이템 팔렸나?
        //        {
        //            if (shopItems[i].type == 1)                 // 무기
        //            {
        //                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 공격력 +{shopItems[i].value}  | {shopItems[i].information}| {shopItems[i].price} G");
        //            }

        //            else if (shopItems[i].type == 2)                //방어구
        //            {
        //                if (shopItems[i].value > 9)
        //                {
        //                    Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value} | {shopItems[i].information}| {shopItems[i].price} G");
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value}  | {shopItems[i].information}| {shopItems[i].price} G");
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (shopItems[i].type == 1)                 // 무기
        //            {
        //                Console.WriteLine($"- {i + 1} {shopItems[i].name}| 공격력 +{shopItems[i].value}  | {shopItems[i].information}| 구매완료");
        //            }

        //            else if (shopItems[i].type == 2)                //방어구
        //            {
        //                if (shopItems[i].value > 9)
        //                {
        //                    Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value} | {shopItems[i].information}| 구매완료");
        //                }
        //                else
        //                {
        //                    Console.WriteLine($"- {i + 1} {shopItems[i].name}| 방어력 +{shopItems[i].value}  | {shopItems[i].information}| 구매완료");
        //                }
        //            }
        //        }
        //    }
        //    // 구매 리스트
        //    Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
        //    Console.Write(">>");

        //    int buyAction = SelectNumber(shopItems.Length);

        //    if (buyAction == 0)
        //    {
        //        buyCheck = true;
        //    }
        //    else if (shopItems[buyAction - 1].get)
        //    {
        //        Console.WriteLine("이미 구매한 아이템입니다.");
        //    }
        //    else
        //    {
        //        if (character.gold >= shopItems[buyAction - 1].price)
        //        {
        //            Console.WriteLine("구매를 완료했습니다.");
        //            character.gold -= shopItems[buyAction - 1].price;
        //            shopItems[buyAction - 1].get = true;
        //            Console.ReadLine();
        //        }
        //        else
        //        {
        //            Console.WriteLine("Gold 가 부족합니다.");
        //            Console.ReadLine();
        //        }
        //    }
        //}

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
        public string nickName = "";
        public int type = 0; // 1: 무기, 2: 방어구
        public int value = 0;
        public string information = "";
        public int price = 0;
        public bool get = false;
        public bool equip = false;

        public void SetItem(string Name, int Type, int Value, string Information, int Price, string NickName)
        {
            name = Name;
            type = Type;
            value = Value;
            information = Information;
            price = Price;
            nickName = NickName;
        }
    }
}
