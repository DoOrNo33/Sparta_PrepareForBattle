namespace Game
{
    internal class Program
    {


        static void Main(string[] args)
        {
            int firstAction = 0;

            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("\n1. 상태 보기\n2. 인벤토리\n3. 상점");
            Console.WriteLine("\n원하시는 행동을 입력해주세요.");
            Console.Write(">>");

            firstAction = GameStart();

            switch(firstAction)
            {
                case 1:
                    Console.WriteLine("1을 골랐습니다.");
                    break;
                case 2:
                    Console.WriteLine("2를 골랐습니다.");
                    break;
                case 3:
                    Console.WriteLine("3을 골랐습니다.");
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }

        static private int GameStart()
        {
            int select = 0;
            string input = Console.ReadLine();
            bool isNumber;
            isNumber = int.TryParse(input, out select);
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

    }
}
