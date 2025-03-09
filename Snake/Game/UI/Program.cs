using System.Net.NetworkInformation;
using static Game.StaticMembers;

namespace Game
{
    internal class Program
    {
        public void GameOpenEnd(E_GameSences sences)
        {
            Console.Clear();
            // 设置光标位置
            Console.SetCursorPosition(LENGTH / 2 - 4, WIDTH / 2 - 7);
            // 设置字体颜色
            Console.ForegroundColor = ConsoleColor.White;
            // 打印
            Console.WriteLine(sences == E_GameSences.Open ? "贪吃蛇" : "The End");
            Console.SetCursorPosition(LENGTH / 2 - 8, WIDTH - 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("'w'、's'---上下选择");
            Console.SetCursorPosition(LENGTH / 2 - 8, WIDTH - 1);
            Console.WriteLine("\"回车\"---确认");

            // 接收的参数
            char c = ' ';
            bool m = true;
            do
            {
                // 先清除再打印
                Console.SetCursorPosition(LENGTH / 2 - 5, WIDTH / 2 - 5);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LENGTH / 2 - 5, WIDTH / 2 - 5);
                Console.ForegroundColor = m ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine(sences == E_GameSences.Open ? "开始游戏" : "回到主菜单");

                Console.SetCursorPosition(LENGTH / 2 - 5, WIDTH / 2 - 3);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LENGTH / 2 - 5, WIDTH / 2 - 3);
                Console.ForegroundColor = m ? ConsoleColor.White : ConsoleColor.Red;
                Console.WriteLine("退出游戏");

                c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'w':
                    case 'W':
                    case 's':
                    case 'S':
                        m = !m;
                        break;
                    case '\r':
                        // 开始游戏
                        if (m && sences == E_GameSences.Open)
                        {
                            GameOn gameOn = new GameOn();
                            gameOn.GameWorking();
                        }
                        else if (m && sences == E_GameSences.End)
                        {
                            Program program = new Program();
                            program.GameOpenEnd(E_GameSences.Open);
                        }
                        else
                            Environment.Exit(0);
                        break;
                    default:
                        continue;
                }

            } while (true);
        }
        static void Main(string[] args)
        {
            // 设置窗口大小
            Console.SetWindowSize(LENGTH, WIDTH);
            Console.SetBufferSize(LENGTH, WIDTH);
            // 设置背景颜色
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            // 设置光标为不可见
            Console.CursorVisible = false;

            // 进入游戏主界面
            Program program = new Program();
            program.GameOpenEnd(E_GameSences.Open);
        }
    }
}
