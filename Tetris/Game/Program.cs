using static Game.StaticMembers;

namespace Game
{
    internal class Program
    {
        public void GameOpenEnd(E_GameSences sences)
        {
            Console.Clear();
            // 设置光标位置
            Console.SetCursorPosition(LENGTH / 2 - 5, WIDTH / 2 - 7);
            // 设置字体颜色
            Console.ForegroundColor = ConsoleColor.White;
            // 打印
            Console.WriteLine(sences == E_GameSences.Open ? "俄罗斯方块" : "The End");
            Console.SetCursorPosition(LENGTH / 2 - 8, WIDTH - 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("'w'、's'---上下选择");
            Console.SetCursorPosition(LENGTH / 2 - 8, WIDTH - 1);
            Console.WriteLine("\"回车\"---确认");

            // 接收的参数
            ConsoleKey c;
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

                c = Console.ReadKey(true).Key;
                switch (c)
                {
                    case ConsoleKey.W:
                    case ConsoleKey.S:
                        m = !m;
                        break;
                    case ConsoleKey.Enter:
                        // 开始游戏
                        if (m && sences == E_GameSences.Open)
                            gameon.GameOnGoing();
                        else if (m && sences == E_GameSences.End)
                            program.GameOpenEnd(E_GameSences.Open);
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
            Console.SetWindowSize(LENGTH, WIDTH_BUFFER);
            Console.SetBufferSize(LENGTH, WIDTH_BUFFER);
            // 设置背景颜色
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            // 设置光标为不可见
            Console.CursorVisible = false;

            // 进入游戏主界面
            program.GameOpenEnd(E_GameSences.Open);
        }
    }
}
