﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    internal class Status
    {
        public void ViewStatus()
        {
            float attackItemValue = 0;
            float defenseItemValue = 0;

            Console.Clear();
            Console.WriteLine("상태 보기");
            Console.WriteLine("캐릭터의 정보가 표시됩니다.\n");
            Console.WriteLine("Lv. {0:D2}", GameManager.instance.character.level);
            Console.WriteLine($"{GameManager.instance.character.name} ( {GameManager.instance.character.job} )");
            foreach (Item item in GameManager.instance.myItems)                                  //무기 꼈는지 체크
            {
                if (item.type == 1)
                {
                    if (item.equip)
                    {
                        attackItemValue += item.value;
                    }
                }
            }

            foreach (Item item in GameManager.instance.myItems)                      // 방어구 아이템 꼈는지 체크
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
                Console.WriteLine($"공격력 : {GameManager.instance.character.adAttackValue}");
            }
            else
            {
                Console.WriteLine($"공격력 : {(GameManager.instance.character.adAttackValue)} (+{attackItemValue})");
            }

            if (defenseItemValue == 0)
            {
                Console.WriteLine($"방어력 : {GameManager.instance.character.adDefenseValue}");
            }
            else
            {
                Console.WriteLine($"방어력 : {(GameManager.instance.character.adDefenseValue)} (+{defenseItemValue})");
            }


            Console.WriteLine($"체 력 : {GameManager.instance.character.hitPoint}");
            Console.WriteLine($"Gold : {GameManager.instance.character.gold} G");
            Console.WriteLine("\n0. 나가기\n\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            int statusAction = GameManager.instance.SelectNumber(0);  // 상태창 액션 선택
        }
        

    }
}
