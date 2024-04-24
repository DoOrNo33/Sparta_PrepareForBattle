using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.IO;




namespace Game
{
    internal class Program
    {
        

        static void Main(string[] args)
        {
            bool gameOn = true;                 // 게임 구동
            bool quitInventory = false;
            bool quitEquipment = false;
            bool quitShop = false;             
            bool quitBuyItem = false;             // 상점 확인
            bool quitSellMyItemCheck = false;
            bool quitDungeonCheck = false;
            bool quitInn = false;
            bool isLoad = false;

            int dungeonClearCount = 0;
            int requireExp = 1;


            // 아이템 클래스 생성
            Item noviceArmor = new Item();
            noviceArmor.SetItem("수련자 갑옷     ", 2, 5f  , "수련에 도움을 주는 갑옷입니다.                   ", 1000, "수련자 갑옷", 1);
            Item ironArmor = new Item();
            ironArmor.SetItem("무쇠갑옷        ", 2, 9f  , "무쇠로 만들어져 튼튼한 갑옷입니다.               ", 1800, "무쇠갑옷", 2);
            Item spartanArmor = new Item();
            spartanArmor.SetItem("스파르타의 갑옷 ", 2, 15f , "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 3500, "스파르타의 갑옷", 3);
            Item wornSword = new Item();
            wornSword.SetItem("낡은 검         ", 1, 2f  , "쉽게 볼 수 있는 낡은 검 입니다.                  ", 600, "낡은 검", 4);
            Item bronzeAxe = new Item();
            bronzeAxe.SetItem("청동 도끼       ", 1, 5f  , "어디선가 사용됐던거 같은 도끼입니다.             ", 1500, "청동 도끼", 5);
            Item spartanSpear = new Item();
            spartanSpear.SetItem("스파르타의 창   ", 1, 7f  , "스파르타의 전사들이 사용했다는 전설의 창입니다.  ", 2700, "스파르타의 창", 6);
            Item legendarySword = new Item();
            legendarySword.SetItem("전설의 검       ", 1, 80f, "용사를 위한 전설의 검 입니다.                    ", 100, "전설의 검", 7);

            Item[] shopItems = new Item[] { noviceArmor, ironArmor, spartanArmor, wornSword, bronzeAxe, spartanSpear, legendarySword };

            //List<Item> myItems = new List<Item>();

            List<string> loadItemStr = new List<string>();


            // 던전 생성

            Dungeon easyDungeon = new Dungeon("쉬운 던전", 5f, 1000);
            Dungeon normalDungeon = new Dungeon("일반 던전", 11f, 1700);
            Dungeon hardDungeon = new Dungeon("어려운 던전", 17f, 2500);

            Dungeon[] dungeons = [easyDungeon, normalDungeon, hardDungeon];


            do
            {
                Console.Clear();

                if (isLoad == false)
                {
                    if (File.Exists("D:\\TaihaData.text"))              // 데이터 저장 위치
                    {
                        LoadDate();
                    }
                    isLoad = true;
                }

                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점\n4. 던전입장\n5. 여관\n\n0. 게임 종료");
                Console.WriteLine("\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                //int firstAction = SelectNumber(5);
                MainMenuAction firstAction = (MainMenuAction)GameManager.instance.SelectNumber(5);

                switch (firstAction)
                {
                    case MainMenuAction.QuitGame:
                        Console.WriteLine("게임을 종료합니다.");
                        int tempItemCount = 0;
                        int i = 0;
                        foreach (Item getItem in shopItems)
                        {
                            i++;
                            tempItemCount++;
                            string tempItemName = getItem.number + ":" + getItem.get + ":" + getItem.equip;
                            File.WriteAllText($"D:\\TaihaItemData[{i}].text", tempItemName.ToString());
                        }
                        string myVariable =
                            $"{GameManager.instance.character.name}," +
                            $"{GameManager.instance.character.level}," +
                            $"{GameManager.instance.character.hitPoint}," +
                            $"{GameManager.instance.character.gold}," +
                            $"{tempItemCount}," +
                            $"{dungeonClearCount}," +
                            $"{requireExp}";                    // 데이터 저장
                        File.WriteAllText("D:\\TaihaData.text", myVariable.ToString());

                        gameOn = false;
                        break;
                    case MainMenuAction.ViewStatus:                                 
                        Status status = new Status();
                        status.ViewStatus();
                        break;
                    case MainMenuAction.OpenInventory:
                        quitInventory = false;
                        do
                        {
                            ViewInventory();
                        }
                        while (!quitInventory);
                        break;
                    case MainMenuAction.VisitShop:
                        quitShop = false;
                        do
                        {
                            VisitShop();
                        }
                        while (!quitShop);
                        break;
                    case MainMenuAction.EnterDungeon:
                        if (GameManager.instance.character.hitPoint <= 0)
                        {
                            Console.WriteLine("체력이 다 떨어졌습니다. 여관에서 휴식을 취해주세요.");
                            Console.WriteLine("<Press Any Key>");
                            Console.Write(">>");
                            Console.ReadLine();
                        }
                        else
                        {
                            quitDungeonCheck = false;
                            do
                            {
                                EnterDungeon();
                            }
                            while (!quitDungeonCheck && GameManager.instance.character.hitPoint > 0);
                        }
                        break;
                    case MainMenuAction.VisitInn:
                        quitInn = false;
                        do
                        {
                            VisitInn();
                        }
                        while (!quitInn);
                        break;
                }
            }
            while (gameOn);


            void ViewInventory()
            {
                Console.Clear();
                Console.WriteLine("인벤토리");
                Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.\n\n[아이템 목록]\n");
                foreach (Item name in GameManager.instance.myItems)
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

                int inventoryAction = GameManager.instance.SelectNumber(1);  // 인벤토리창 액션 선택

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
                foreach (Item name in GameManager.instance.myItems)
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

                int equipAction = GameManager.instance.SelectNumber(GameManager.instance.myItems.Count);      // 장착 액션은 아이템 수에 한정

                if (equipAction == 0)
                {
                    quitEquipment = true;
                }
                else if (GameManager.instance.myItems[equipAction - 1].equip)            // 그 아이템을 장착하고 있다면,
                {
                    if (GameManager.instance.myItems[equipAction - 1].type == 1)
                    {
                        GameManager.instance.character.adAttackValue -= GameManager.instance.myItems[equipAction - 1].value;
                    }
                    else
                    {
                        GameManager.instance.character.adDefenseValue -= GameManager.instance.myItems[equipAction - 1].value;
                    }
                    GameManager.instance.myItems[equipAction - 1].equip = false;
                }
                else
                {
                    foreach (Item item in GameManager.instance.myItems)      // 장착되어있는 장비 중 타입이 장착하려는 장비를 찾아서 장착 해제
                    {
                        if (item.equip)
                        {
                            if (item.type == GameManager.instance.myItems[equipAction - 1].type)
                            {
                                if (GameManager.instance.myItems[equipAction - 1].type == 1)
                                {
                                    GameManager.instance.character.adAttackValue -= GameManager.instance.myItems[equipAction - 1].value;
                                }
                                else
                                {
                                    GameManager.instance.character.adDefenseValue -= GameManager.instance.myItems[equipAction - 1].value;
                                }
                                item.equip = false;
                            }
                        }
                    }
                    GameManager.instance.myItems[equipAction - 1].equip = true;      // 장착 아이템 설정

                    if (GameManager.instance.myItems[equipAction - 1].type == 1)         // 장착 아이템이 무기라면 공격력 +
                    {
                        GameManager.instance.character.adAttackValue += GameManager.instance.myItems[equipAction - 1].value;
                    }
                    else
                    {
                        GameManager.instance.character.adDefenseValue += GameManager.instance.myItems[equipAction - 1].value;
                    }


                }
            }

            void VisitShop()
            {
                Console.Clear();
                Console.WriteLine("상점");
                Console.WriteLine("필요한 아이템을 얻을 수 잇는 상점입니다.\n\n[보유 골드]");
                Console.WriteLine($"{GameManager.instance.character.gold} G\n");
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

                ShopAction shopAction = (ShopAction)GameManager.instance.SelectNumber(2);  // 상점 액션 선택

                switch (shopAction)
                {
                    case ShopAction.QuitShop:
                        quitShop = true;
                        break;
                    case ShopAction.BuyItem:
                        quitBuyItem = false;

                        do
                        {
                            BuyShopItem();
                        }
                        while (!quitBuyItem);

                        break;
                    case ShopAction.SellItem:
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
                Console.WriteLine($"{GameManager.instance.character.gold} G");
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

                int buyAction = GameManager.instance.SelectNumber(shopItems.Length);

                if (buyAction == 0)
                {
                    quitBuyItem = true;
                }
                else if (shopItems[buyAction - 1].get)
                {
                    Console.WriteLine("이미 구매한 아이템입니다.");
                    Console.WriteLine("<Press Any Key>");
                    Console.Write(">>");
                    Console.ReadLine();
                }
                else
                {
                    if (GameManager.instance.character.gold >= shopItems[buyAction - 1].price)
                    {
                        Console.WriteLine("구매를 완료했습니다.\n<Press Any Key>");
                        GameManager.instance.character.gold -= shopItems[buyAction - 1].price;
                        shopItems[buyAction - 1].get = true;                        // 내 아이템 리스트에 추가
                        GameManager.instance.myItems.Add(shopItems[buyAction - 1]);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Gold 가 부족합니다.\n<Press Any Key>");
                        Console.Write(">>");
                        Console.ReadLine();
                    }
                }
            }

            void SellMyItem()
            {
                Console.Clear();
                Console.WriteLine("상점 - 아이템 판매");
                Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n\n[보유 골드]\n");
                Console.WriteLine($"{GameManager.instance.character.gold} G\n\n[아이템 목록]");
                int i = 0;
                foreach (Item name in GameManager.instance.myItems)
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

                int sellAction = GameManager.instance.SelectNumber(GameManager.instance.myItems.Count);

                if (sellAction == 0)
                {
                    quitSellMyItemCheck = true;
                }
                else
                {
                    GameManager.instance.character.gold += (GameManager.instance.myItems[sellAction - 1].price * 85 / 100);
                    GameManager.instance.myItems[sellAction - 1].equip = false;
                    GameManager.instance.myItems[sellAction - 1].get = false;
                    GameManager.instance.myItems.RemoveAt(sellAction - 1);

                }

            }

            void EnterDungeon()
            {
                Console.Clear();
                Console.WriteLine("던전입장");
                Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
                Console.WriteLine("1. 쉬운 던전      | 방어력 5 이상 권장");
                Console.WriteLine("2. 일반 던전      | 방어력 11 이상 권장");
                Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");


                Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int dungeonSelectAction = GameManager.instance.SelectNumber(3);

                if (dungeonSelectAction == 0)
                {
                    quitDungeonCheck = true;
                }
                else
                {
                    ClearDungeon(dungeonSelectAction);
                }

            }

            void ClearDungeon(int difficult)
            {
                Random random = new Random();               // 랜덤 객체 생성
                Console.Clear();
                if (dungeons[difficult - 1].recommandDefenseValue > GameManager.instance.character.adDefenseValue)
                {
                    int i = random.Next(0, 10);
                    if (i < 4)
                    {
                        Console.WriteLine("던전 클리어 실패");
                        Console.WriteLine($"\n{dungeons[difficult - 1].dungeonLevel}을 클리어에 실패하였습니다.\n");
                        Console.WriteLine("[탐험 결과]");
                        int temp = GameManager.instance.character.hitPoint;
                        GameManager.instance.character.hitPoint = GameManager.instance.character.hitPoint / 2;
                        if (GameManager.instance.character.hitPoint < 0)
                        {
                            GameManager.instance.character.hitPoint = 0;
                            Console.WriteLine("체력이 다 떨어졌습니다. 여관에서 휴식을 취해주세요.");
                        }
                        Console.WriteLine($"체력 {temp} -> {GameManager.instance.character.hitPoint}");
                        Console.WriteLine("\n<Press Any Key>");
                        Console.Write(">>");
                        Console.ReadLine();
                    }
                    else
                    {
                        dungeonClearCount++;
                        float gap = (dungeons[difficult - 1].recommandDefenseValue - GameManager.instance.character.adDefenseValue);
                        int j = random.Next(20 + (int)gap, 36 + (int)gap);
                        int tempHitPoint = GameManager.instance.character.hitPoint;                                                              // 체력 설정
                        GameManager.instance.character.hitPoint -= j;
                        Console.WriteLine("던전 클리어");
                        Console.WriteLine($"축하합니다!!\n{dungeons[difficult - 1].dungeonLevel}을 클리어 하엿습니다.\n");
                        Console.WriteLine("[탐험 결과]");
                        if (GameManager.instance.character.hitPoint < 0)
                        {
                            GameManager.instance.character.hitPoint = 0;
                            Console.WriteLine("체력이 다 떨어졌습니다. 여관에서 휴식을 취해주세요.");
                        }
                        LevelCheck(dungeonClearCount);
                        Console.WriteLine($"체력 {tempHitPoint} -> {GameManager.instance.character.hitPoint}");                                // 골드 보상 설정
                        int k = random.Next((int)GameManager.instance.character.adAttackValue, (int)(((GameManager.instance.character.adAttackValue) * 2) + 1));
                        int adReward = dungeons[difficult - 1].clearReward * (1 + (k / 100));
                        int tempGold = GameManager.instance.character.gold;
                        GameManager.instance.character.gold += adReward;
                        Console.WriteLine($"Gold {tempGold} G -> {GameManager.instance.character.gold} G");
                        Console.WriteLine("\n<Press Any Key>");
                        Console.Write(">>");
                        Console.ReadLine();
                    }
                }
                else
                {
                    dungeonClearCount++;
                    float gap = (dungeons[difficult - 1].recommandDefenseValue - GameManager.instance.character.adDefenseValue);
                    int j = random.Next(20 + (int)gap, 36 + (int)gap);
                    if (j < 0)
                    {
                        j = 0;
                    }
                    int tempHitPoint = GameManager.instance.character.hitPoint;                                                              // 체력 설정
                    GameManager.instance.character.hitPoint -= j;
                    Console.WriteLine("던전 클리어");
                    Console.WriteLine($"축하합니다!!\n{dungeons[difficult - 1].dungeonLevel}을 클리어 하엿습니다.\n");
                    Console.WriteLine("[탐험 결과]");
                    if (GameManager.instance.character.hitPoint < 0)
                    {
                        GameManager.instance.character.hitPoint = 0;
                        Console.WriteLine("체력이 다 떨어졌습니다. 여관에서 휴식을 취해주세요.");
                    }
                    LevelCheck(dungeonClearCount);
                    Console.WriteLine($"체력 {tempHitPoint} -> {GameManager.instance.character.hitPoint}");                                // 골드 보상 설정
                    int k = random.Next((int)GameManager.instance.character.adAttackValue, (int)(((GameManager.instance.character.adAttackValue) * 2) + 1));
                    int adReward = dungeons[difficult - 1].clearReward * (1 + (k / 100));
                    int tempGold = GameManager.instance.character.gold;
                    GameManager.instance.character.gold += adReward;
                    Console.WriteLine($"Gold {tempGold} G -> {GameManager.instance.character.gold} G");
                    Console.WriteLine("\n<Press Any Key>");
                    Console.Write(">>");
                    Console.ReadLine();
                }


            }

            void VisitInn()
            {
                Console.Clear();
                Console.WriteLine("여관");
                Console.WriteLine($"500 G 를 내면 휴식할 수 있습니다. (보유 골드 : {GameManager.instance.character.gold} G)\n\n[현재 체력]\n{GameManager.instance.character.hitPoint}");
                Console.WriteLine("\n1. 휴식하기\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");

                int restAction = GameManager.instance.SelectNumber(1);  //액션 선택

                if (restAction == 0)
                {
                    quitInn = true;
                }
                else
                {
                    if (GameManager.instance.character.hitPoint == 100)
                    {
                        Console.WriteLine("이미 충분히 쉬었습니다.\n<Press Any Key>");
                        Console.ReadLine();
                    }
                    else
                    {
                        if (GameManager.instance.character.gold >= 500)
                        {
                            int tempHitPoint = GameManager.instance.character.hitPoint;
                            GameManager.instance.character.hitPoint = 100;
                            GameManager.instance.character.gold -= 500;
                            Console.WriteLine($"휴식을 완료했습니다.\n체력 : {tempHitPoint} -> {GameManager.instance.character.hitPoint}");
                            Console.WriteLine("<Press Any Key>");
                            Console.ReadLine();
                        }
                        else
                        {
                            if (GameManager.instance.character.hitPoint <= 0)
                            {
                                Console.WriteLine("여관 주인이 당신을 불쌍히 여겨 숙박을 허락했습니다.\n");
                                int tempHitPoint = GameManager.instance.character.hitPoint;
                                GameManager.instance.character.hitPoint = 100;
                                GameManager.instance.character.gold = 0;
                                Console.WriteLine($"휴식을 완료했습니다.\n체력 : {tempHitPoint} -> {GameManager.instance.character.hitPoint}");
                                Console.WriteLine("<Press Any Key>");
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Gold 가 부족합니다.\n<Press Any Key>");
                                Console.ReadLine();
                            }

                        }
                    }
                }
            }

            void LevelCheck(int count)                          // 카운트가 필요량에 도달했다면 레벨업, 필요량 +1, 클리어 카운트 초기화
            {
                if (requireExp == count)  
                {
                    GameManager.instance.character.level++;
                    GameManager.instance.character.attackValue += 0.5f;
                    GameManager.instance.character.adAttackValue += 0.5f;
                    requireExp++;
                    dungeonClearCount = 0;
                    Console.WriteLine("레벨 업!");
                }
            }

            void LoadDate()
            {
                string loadData = File.ReadAllText("D:\\TaihaData.text");       // 세이브 기록 위치
                string[] loadDatas = loadData.Split(',');
                GameManager.instance.character.level = int.Parse(loadDatas[1]);
                GameManager.instance.character.attackValue = (10f + (GameManager.instance.character.level * 0.5f) - 0.5f);
                GameManager.instance.character.adAttackValue = GameManager.instance.character.attackValue;
                GameManager.instance.character.hitPoint = int.Parse(loadDatas[2]);
                GameManager.instance.character.gold = int.Parse(loadDatas[3]);
                dungeonClearCount = int.Parse(loadDatas[5]);
                requireExp = int.Parse(loadDatas[6]);

                if (int.Parse(loadDatas[4]) != 0)                                 // 5번째가 아이템 갯수 // 아이템 기록 확인
                {                                                               // getItem.number + ":" + getItem.get + ":" + getItem.equip;
                    for (int i = 0; i < int.Parse(loadDatas[4]); i++)
                    {
                        string loadItem = File.ReadAllText($"D:\\TaihaItemData[{i + 1}].text");
                        //loadItemStr.Add(loadItem);
                        //loadItemCount++;
                        string[] loadGetItems = loadItem.Split(":");
                        if ((bool.Parse(loadGetItems[1])) == true)
                        {
                            foreach (Item tempGetItem in shopItems)
                            {
                                if (tempGetItem.number == int.Parse(loadGetItems[0]))   // 불러온 아이템은  획득 처리
                                {
                                    tempGetItem.get = true;
                                    GameManager.instance.myItems.Add(tempGetItem);


                                }

                                if (bool.Parse(loadGetItems[2]) == true)              // 불러온 아이템 장착 처리
                                {
                                    foreach (Item tempEquipItem in GameManager.instance.myItems)
                                    {
                                        if (int.Parse(loadGetItems[0]) == tempEquipItem.number)
                                        {
                                            tempEquipItem.equip = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                foreach (Item tempEquipItem in GameManager.instance.myItems)                         // 장착 아이템 확인해서 수정 공격력, 방어력 입력
                {
                    if (tempEquipItem.equip == true)
                    {
                        if (tempEquipItem.type == 1)
                        {
                            GameManager.instance.character.adAttackValue += tempEquipItem.value;
                        }
                        else
                        {
                            GameManager.instance.character.adDefenseValue += tempEquipItem.value;
                        }
                    }
                }
            }
        }

        enum MainMenuAction                             // 열거형 도전
        {
            QuitGame,
            ViewStatus,
            OpenInventory,
            VisitShop,
            EnterDungeon,
            VisitInn
        }

        enum ShopAction
        {
            QuitShop,
            BuyItem,
            SellItem
        }

    }


}
