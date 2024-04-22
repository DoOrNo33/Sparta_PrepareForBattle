using System.Drawing;
using System.Xml.Linq;

namespace SnakeGame
{
    internal class Program
    {


        static void Main(string[] args)
        {
            // 뱀의 초기 위치와 방향을 설정하고, 그립니다.
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            // 음식의 위치를 무작위로 생성하고, 그립니다.
            FoodCreator foodCreator = new FoodCreator(80, 20, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            // 게임 루프: 이 루프는 게임이 끝날 때까지 계속 실행됩니다.
            while (true)
            {
                // 키 입력이 있는 경우에만 방향을 변경합니다.

                // 뱀이 이동하고, 음식을 먹었는지, 벽이나 자신의 몸에 부딪혔는지 등을 확인하고 처리하는 로직을 작성하세요.
                // 이동, 음식 먹기, 충돌 처리 등의 로직을 완성하세요.

                Thread.Sleep(100); // 게임 속도 조절 (이 값을 변경하면 게임의 속도가 바뀝니다)

                // 뱀의 상태를 출력합니다 (예: 현재 길이, 먹은 음식의 수 등)
            }
        }


    }

    public class Snake()
    {

    }

    public class FoodCreator()
    {

    }

    public class Point
    {
        public int x { get; set; }
        public int y { get; set; }
        public char sym { get; set; }

        // Point 클래스 생성자
        public Point(int _x, int _y, char _sym)
        {
            x = _x;
            y = _y;
            sym = _sym;
        }

        // 점을 그리는 메서드
        public void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(sym);
        }

        // 점을 지우는 메서드
        public void Clear()
        {
            sym = ' ';
            Draw();
        }

        // 두 점이 같은지 비교하는 메서드
        public bool IsHit(Point p)
        {
            return p.x == x && p.y == y;
        }
    }
    // 방향을 표현하는 열거형입니다.
    public enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }

}
