using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace ConsoleApp1
{
    class Program
    {
        const int LONG = 60;
        const int WIDTH = 40;
        static void Display(int x, int y)
        {
            // 光标位置
            Console.SetCursorPosition(x, y);
            // 字体颜色
            Console.ForegroundColor = ConsoleColor.Yellow;
            // 打印
            Console.Write("■");
        }

        static void DisplayBoard(int p_hp, int d_hp, int p_ht, int d_ht)
        {
            Console.SetCursorPosition(2, WIDTH - 10);
            Console.WriteLine("                                                   ");
            Console.SetCursorPosition(2, WIDTH - 10);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("你对大魔王造成了 {0} 点伤害，大魔王还剩 {1} 点血量", p_ht, d_hp);
            Console.SetCursorPosition(2, WIDTH - 9);
            Console.WriteLine("                                                   ");
            Console.SetCursorPosition(2, WIDTH - 9);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("大魔王对你造成了 {0} 点伤害，你还剩 {1} 点血量", d_ht, p_hp);
        }

        static void GameBegin()
        {
            Console.SetCursorPosition(LONG / 2 - 7, 8);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("小方块营救公主");
            char c = ' ';
            int m = 0;
            do
            {
                Console.SetCursorPosition(LONG / 2 - 4, 10);
                Console.Write("        ");
                Console.SetCursorPosition(LONG / 2 - 4, 10);
                Console.ForegroundColor = m == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("开始游戏");
                Console.SetCursorPosition(LONG / 2 - 4, 12);
                Console.Write("       ");
                Console.SetCursorPosition(LONG / 2 - 4, 12);
                Console.ForegroundColor = m == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");
                c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'W':
                    case 'w':
                    case 'S':
                    case 's':
                        m = (m + 1) % 2;
                        break;
                    case 'J':
                    case 'j':
                        if (m == 0)
                            // 开始游戏
                            GameOn();
                        else
                            Environment.Exit(0);
                        break;
                }
            } while (true);
        }

        static void GameOn()
        {
            Console.Clear();
            /*初始化玩家血量和魔王血量*/
            int player_hp = 100;
            int devl_hp = 250;

            /*打印红墙*/
            Console.ForegroundColor = ConsoleColor.Red;
            for (int i = 0; i <= LONG - 2; i += 2)
            {
                // 上
                Console.SetCursorPosition(i, 1);
                Console.Write("■");
                // 下
                Console.SetCursorPosition(i, WIDTH - 1);
                Console.Write("■");
            }
            for (int i = 1; i < WIDTH - 1; ++i)
            {
                // 左
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                // 右
                Console.SetCursorPosition(LONG - 2, i);
                Console.Write("■");
            }
            // 隔墙
            Console.SetCursorPosition(2, WIDTH - 11);
            for (int i = 2; i <= LONG - 4; i += 2)
                Console.Write("■");


            /*打印玩家*/
            Console.SetCursorPosition(10, 5);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("■");

            /*打印魔王*/
            Console.SetCursorPosition(LONG - 12, WIDTH - 17);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("▲");

            /*控制玩家*/
            int x = 10;
            int y = 5;
            int tmpx = x;
            int tmpy = y;
            do
            {
                tmpx = x;
                tmpy = y;
                char i = Console.ReadKey(true).KeyChar;
                switch (i)
                {
                    case 'W':
                    case 'w':
                        if (y - 1 <= 0)
                            continue;
                        else
                            --y;
                        break;
                    case 'A':
                    case 'a':
                        if (x - 2 <= 0)
                            continue;
                        else
                            x -= 2;
                        break;
                    case 'S':
                    case 's':
                        if (y + 1 >= WIDTH - 11)
                            continue;
                        else
                            ++y;
                        break;
                    case 'D':
                    case 'd':
                        if (x + 2 >= LONG - 2)
                            continue;
                        else
                            x += 2;
                        break;
                    default:
                        continue;
                }
                // 判断是否要打魔王
                if (x == LONG - 12 && y == WIDTH - 17)
                {
                    x = tmpx;
                    y = tmpy;
                    Random r = new Random();
                    int play_ht = r.Next(150);
                    int devl_ht = r.Next(70);
                    devl_hp -= play_ht;
                    if (devl_hp > 0)
                        player_hp -= devl_ht;
                    // 打印计分板
                    DisplayBoard(player_hp, devl_hp, play_ht, devl_ht);
                    // 判断输赢
                    if (devl_hp <= 0)
                    {
                        // 擦除魔王
                        Console.SetCursorPosition(LONG - 12, WIDTH - 17);
                        Console.Write("  ");

                        Console.SetCursorPosition(2, WIDTH - 10);
                        Console.WriteLine("                                                   ");
                        Console.SetCursorPosition(2, WIDTH - 9);
                        Console.WriteLine("                                                   ");
                        Console.SetCursorPosition(2, WIDTH - 8);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("恭喜你战胜了大魔王！！！    去营救你的公主吧！！！");
                        Gameon2(x, y);
                    }
                    else if (player_hp <= 0)
                        GameOver(1);
                }
                else
                {
                    // 擦除之前的方块
                    Console.SetCursorPosition(tmpx, tmpy);
                    Console.Write("  ");
                    // 绘制新的方块
                    Display(x, y);
                }
            } while (true);
        }
        static void Gameon2(int x, int y)
        {
            // 打印公主
            Console.SetCursorPosition(10, 5);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(":|:");
            /*控制玩家*/
            int tmpx = x;
            int tmpy = y;
            do
            {
                tmpx = x;
                tmpy = y;
                char i = Console.ReadKey(true).KeyChar;
                switch (i)
                {
                    case 'W':
                    case 'w':
                        if (y - 1 <= 0)
                            continue;
                        else
                            --y;
                        break;
                    case 'A':
                    case 'a':
                        if (x - 2 <= 0)
                            continue;
                        else
                            x -= 2;
                        break;
                    case 'S':
                    case 's':
                        if (y + 1 >= WIDTH - 11)
                            continue;
                        else
                            ++y;
                        break;
                    case 'D':
                    case 'd':
                        if (x + 2 >= LONG - 2)
                            continue;
                        else
                            x += 2;
                        break;
                    default:
                        continue;
                }
                if (x == 10 && y == 5)
                {
                    GameOver(0);
                }
                else
                {
                    // 擦除之前的方块
                    Console.SetCursorPosition(tmpx, tmpy);
                    Console.Write("  ");
                    // 绘制新的方块
                    Display(x, y);
                }
            } while (true);
        }

        static void GameOver(int flag)
        {
            Console.Clear();
            Console.SetCursorPosition(LONG / 2 - 12, 8);
            Console.ForegroundColor = ConsoleColor.White;
            if (flag == 0)
            {
                Console.Write("你成功从魔王手中救走了公主");
                Console.SetCursorPosition(LONG / 2 - 12, 9);
                Console.Write("日后公主和你过起了没羞没臊的生活");
            }
            else
            {
                Console.Write("你没能从大魔王手中救走公主");
                Console.SetCursorPosition(LONG / 2 - 12, 9);
                Console.Write("魔王和公主过上了不可描述的生活");
            }
            char c = ' ';
            int m = 0;
            do
            {
                Console.SetCursorPosition(LONG / 2 - 4, 11);
                Console.Write("        ");
                Console.SetCursorPosition(LONG / 2 - 4, 11);
                Console.ForegroundColor = m == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("重新游戏");
                Console.SetCursorPosition(LONG / 2 - 4, 13);
                Console.Write("       ");
                Console.SetCursorPosition(LONG / 2 - 4, 13);
                Console.ForegroundColor = m == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");
                c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'W':
                    case 'w':
                    case 'S':
                    case 's':
                        m = (m + 1) % 2;
                        break;
                    case 'J':
                    case 'j':
                        if (m == 0)
                            GameOn();
                        else
                            Environment.Exit(0);
                        break;
                }
            } while (true);
        }

        static void Main(string[] args)
        {
            #region 
            // // 设置控制台大小
            // // 窗口大小 缓冲区大小
            // // 1、先设置窗口大小，再设置缓冲区大小
            // // 2、缓冲区大小不能小于窗口大小
            // // 3、窗口大小不能大于最大尺寸
            // Console.SetWindowSize(50, 50);
            // Console.SetBufferSize(50, 100);

            // // 设置背景颜色
            // // 重置背景颜色后，需要clear一次才能将背景颜色全部改变
            // Console.BackgroundColor = ConsoleColor.Red;
            // Console.Clear();

            // // 光标显隐
            // Console.CursorVisible = false;

            // // 设置光标位置
            // Console.SetCursorPosition(4,2);
            // Console.ForegroundColor = ConsoleColor.Blue;
            // Console.WriteLine("12345");

            // // 关闭控制台
            // Console.ReadKey(true);
            // Environment.Exit(0);
            #endregion
            // 设置窗口大小
            Console.SetWindowSize(LONG, WIDTH);
            Console.SetBufferSize(LONG, WIDTH);
            // 设置背景颜色
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            // 设置光标为不可见
            Console.CursorVisible = false;
            do
            {
                GameBegin();
            } while (true);

        }
    }
}
