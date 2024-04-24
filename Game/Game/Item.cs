using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public class Item
    {
        public string name = "";
        public string nickName = "";
        public int type = 0; // 1: 무기, 2: 방어구
        public float value = 0;
        public string information = "";
        public int price = 0;
        public bool get = false;
        public bool equip = false;
        public int number = 0;

        public void SetItem(string Name, int Type, float Value, string Information, int Price, string NickName, int num)
        {
            name = Name;
            type = Type;
            value = Value;
            information = Information;
            price = Price;
            nickName = NickName;
            number = num;
        }
    }
}
