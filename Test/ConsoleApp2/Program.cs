using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Net.WebSockets;
using System.Runtime.Intrinsics.Arm;
///
/// 暂停一回合事件体系, Move2后退会跳出棋盘bug
///

namespace ConsoleApp2
{
    /// <summary>
    /// “特殊事件”结构体
    /// </summary>
    struct ShiJian
    {
        // 颜色
        public ConsoleColor color;
        // 形状
        public char shap;
        // 位置
        public int[,] position;

        public ShiJian(ConsoleColor clr, char sp, int[,] po)
        {
            color = clr;
            shap = sp;
            position = po;
        }
    }
    struct Gamer
    {
        public int x;
        public int y;
        public char shape;
        public ConsoleColor color;
        public string name;

        public Gamer(int x, int y, char shape, ConsoleColor color, string name)
        {
            this.x = x;
            this.y = y;
            this.shape = shape;
            this.color = color;
            this.name = name;
        }
    }
    internal class Program
    {
        #region 常量
        // 设置窗口长宽
        /// <summary>
        /// 横
        /// </summary>
        const int LONG = 80;
        /// <summary>
        /// 竖
        /// </summary>
        const int WIDTH = 50;
        // 设置棋盘行列
        const int CHESSROW = 25;// 行
        const int CHESSCLO = 25;// 列
        // 棋盘起点
        const int HeadX = 15;
        const int HeadY = 6;
        // 棋盘的符号
        const char DAMN = '●';
        const char SUIDAO = '∠';
        const char STOP = '‖';
        const char SIMPLE = '□';
        // 玩家符号
        const char PLAYER = '★';
        // 电脑符号
        const char COMPUTER = '▲';
        // 玩家与电脑重合符号
        const char CON = '6';
        #endregion


        /*打印墙*/
        static void DisplayBoard()
        {
            // 设置墙的颜色
            Console.ForegroundColor = ConsoleColor.Red;
            // 横墙
            Console.SetCursorPosition(2, 0);
            for(int i = 2; i < LONG - 1; i += 2)
            {
                Console.Write("■");
            }
            Console.SetCursorPosition(2, WIDTH * 3 / 4 - 3);
            for (int i = 2; i < LONG - 1; i += 2)
            {
                Console.Write("■");
            }
            Console.SetCursorPosition(2, WIDTH * 7 / 8);
            for (int i = 2; i < LONG - 1; i += 2)
            {
                Console.Write("■");
            }
            Console.SetCursorPosition(2, WIDTH - 1);
            for (int i = 2; i < LONG - 1; i += 2)
            {
                Console.Write("■");
            }
            // 竖墙
            for (int i = 0; i < WIDTH; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
            }
            for (int i = 0; i < WIDTH; ++i)
            {
                Console.SetCursorPosition(LONG - 2, i);
                Console.Write("■");
            }
            // 操作信息
            Console.SetCursorPosition(2, WIDTH * 3 / 4 - 2);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{SIMPLE}---普通格子");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{DAMN}---炸弹，后退5格");
            Console.SetCursorPosition(2, WIDTH * 3 / 4);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{SUIDAO}---传送门，随机传送，至多前进或后退6格");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{STOP}---时停，暂停行动一回合");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 2);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{PLAYER}---玩家");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"    {COMPUTER}---电脑");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 3);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{CON}---玩家与电脑重合");
        }

        /*初始化棋盘的数组*/
        static void InitArr(ref char[,] arr, int row, int clo)
        {
            ShiJian ZhaDamn = new ShiJian(ConsoleColor.Red, DAMN,
                                new int[,] { { 0, 10 }, { 1, 24 }, { 2, 3 }, { 6, 13 }, { 10, 9 }, { 16, 4 } });
            ShiJian SuiDao = new ShiJian(ConsoleColor.Blue, SUIDAO,
                                            new int[,] { { 2, 19 }, { 4, 2 }, { 7, 0 }, { 14, 23 }, { 20, 10 } });
            ShiJian Stop = new ShiJian(ConsoleColor.Yellow, STOP,
                                            new int[,] { { 8, 16 }, { 12, 16 }, { 18, 2 }, { 22, 23 } });
            // 先将需要的棋盘初始未‘□’
            int m = 0;
            for (int i = 0; i < row; i += 2)
            {
                int j = 0;
                for (j = 0; j < clo; ++j)
                {
                    arr[i, j] = SIMPLE;
                }
                if (i == 24)
                    break;
                arr[i + 1, m == 0 ? j - 1 : 0] = SIMPLE;
                m = (m + 1) % 2;
            }
            // 再初始化“特殊事件”棋盘
            // 炸弹
            for (int i = 0; i < ZhaDamn.position.GetLength(0); ++i)
            {
                arr[ZhaDamn.position[i, 0], ZhaDamn.position[i, 1]] = ZhaDamn.shap;
            }
            // 时空隧道
            for (int i = 0; i < SuiDao.position.GetLength(0); ++i)
            {
                arr[SuiDao.position[i, 0], SuiDao.position[i, 1]] = SuiDao.shap;
            }
            // 暂停一回合
            for (int i = 0; i < Stop.position.GetLength(0); ++i)
            {
                arr[Stop.position[i, 0], Stop.position[i, 1]] = Stop.shap;
            }
        }

        /*检查玩家和电脑的坐标是否重合*/
        static bool CheckPosition(int x, int y, int m, int n)
        {
            return x == m && y == n ? true : false;
        }

        /*判定打印方块的颜色*/
        static ConsoleColor DefColor(char c)
        {
            switch (c)
            {
                case SIMPLE:
                    return ConsoleColor.White;
                case DAMN:
                    return ConsoleColor.Red;
                case SUIDAO:
                    return ConsoleColor.Blue;
                case STOP:
                    return ConsoleColor.Yellow;
                default:
                    return ConsoleColor.White;
            }
        }

        /*玩家移动函数*/
        static void GamerMove(ref Gamer gamer, int rd_num)
        {
            // 移动体系，四套判断
            // 往右走
            if(gamer.y % 4 == 0 || gamer.y % 4 == 1)
            {
                GamerMove2(ref gamer, rd_num, 1, 24, 1);
            }
            // 往左走
            else if(gamer.y % 4 == 2 || gamer.y % 4 == 3)
            {
                GamerMove2(ref gamer, rd_num, -1, 0, 1);
            }
        }
        static void GamerMove2(ref Gamer gamer, int rd_num, int des, int end, int UPandDOWD)
        {
            // des为1，向右走，des为-1，向左走
            // UPandDOWN为1时，向下走，UPandDOWN为-1时，向上走
            // 向下走
            if (des == 1 ? gamer.x + rd_num > 24 : gamer.x - rd_num < 0)
            {
                rd_num -= Math.Abs(end - gamer.x);
                gamer.x = end;
                int cha = 2 * (gamer.y / 2 + 1) - gamer.y;
                // 向上下走后拐弯
                if (rd_num > cha)
                {
                    rd_num -= cha;
                    gamer.y += cha * UPandDOWD;
                    gamer.x -= rd_num * des;
                }
                // 只向上下走
                else
                {
                    gamer.y += rd_num * UPandDOWD;
                }
            }
            // 不向下走
            else
            {
                gamer.x += rd_num * des;
            }
        }

        /*打印操作信息*/
        static void DisplayMove(ref Gamer gamer, int move)
        {
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 1);
            Console.Write("                                      ");
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
            Console.Write("                                ");

            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 1);
            Console.ForegroundColor = gamer.color;
            Console.Write($"{gamer.name}摇到了{move}，前进{move}格");
        }

        /*遇到特殊事件*/
        static void MeetEvent(ref Gamer gamer, ref char[,] BoardArr, int move)
        {
            Random rd = new Random();
            // des_end[0]中存储的是正方向，des_end[1]中存储的是反方向
            int[,] des_end = { { 1, 24 }, { 1, 24 } };
            if (gamer.y % 4 == 0 || gamer.y % 4 == 1)// 右
            {
                des_end[1, 0] = -1;
                des_end[1, 1] = 0;
            }
            else if (gamer.y % 4 == 2 || gamer.y % 4 == 3)// 左0
            {
                des_end[0, 0] = -1;
                des_end[0, 1] = 0;
            }
            switch (BoardArr[gamer.y, gamer.x])
            {
                case SIMPLE:
                    DisplayMove(ref gamer, move);
                    break;
                case DAMN:
                    DisplayMove(ref gamer, move);
                    GamerMove2(ref gamer, 5, des_end[1, 0], des_end[1, 1], -1);
                    Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
                    Console.ForegroundColor = gamer.color;
                    Console.Write("遇到了炸弹，后退5格");
                    break;
                case SUIDAO:
                    DisplayMove(ref gamer, move);
                    int rd_num = rd.Next(-6, 7);
                    if (rd_num > 0)
                        GamerMove2(ref gamer, rd_num, des_end[0, 0], des_end[0, 1], 1);
                    else if(rd_num < 0)
                        GamerMove2(ref gamer, -rd_num, des_end[1, 0], des_end[1, 1], -1);
                    Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
                    Console.ForegroundColor = gamer.color;
                    Console.Write($"遇到了传送门，传送了{rd_num}格");
                    break;
                case STOP:
                    DisplayMove(ref gamer, move);
                    Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
                    Console.ForegroundColor = gamer.color;
                    Console.Write("遇到了时停，暂停一回合");
                    break;
            }
        }

        /*开始前*/
        static void GameBegin()
        {
            // 设置光标位置
            Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 2 - 7);
            // 设置字体颜色
            Console.ForegroundColor = ConsoleColor.White;
            // 打印
            Console.WriteLine("大富翁");
            Console.SetCursorPosition(LONG / 2 - 8, WIDTH - 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("'w'、's'---上下选择");
            Console.SetCursorPosition(LONG / 2 - 8, WIDTH - 1);
            Console.WriteLine("\"回车\"---确认");

            // 接收的参数
            char c = ' ';
            int m = 0;
            do
            {
                // 先清除再打印
                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 5);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 5);
                Console.ForegroundColor = m == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine("开始游戏");

                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 3);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 3);
                Console.ForegroundColor = m == 0 ? ConsoleColor.White : ConsoleColor.Red;
                Console.WriteLine("退出游戏");

                c = Console.ReadKey(true).KeyChar;
                switch(c)
                {
                    case 'w':
                    case 'W':
                    case 's':
                    case 'S':
                        m = (m + 1) % 2;
                        break;
                    case '\r':
                        // 开始游戏
                        if (m == 0)
                            GameOn();
                        else
                            Environment.Exit(0);
                        break;
                    default:
                        continue;
                }

            } while (true);
        }

        /*游戏中*/
        static void GameOn()
        {
            Console.Clear();
            DisplayBoard();

            char[,] BoardArr = new char[CHESSROW,CHESSCLO];
            InitArr(ref BoardArr, CHESSROW, CHESSCLO);
            int m = HeadY;// 棋盘打印的起始竖向
            /*打印棋盘*/
            for (int i = 0; i < CHESSROW; ++i)
            {
                int n = HeadX;// 棋盘打印的起始横向
                for (int j = 0; j < CHESSCLO; ++j)
                {
                    Console.SetCursorPosition(n, m);
                    Console.ForegroundColor = DefColor(BoardArr[i, j]);
                    Console.Write(BoardArr[i, j]);
                    n += 2;
                }
                ++m;
            }

            /*打印玩家与电脑*/
            Gamer Player = new Gamer(0, 0, PLAYER, ConsoleColor.DarkMagenta, "玩家");
            Gamer Computer = new Gamer(0, 0, COMPUTER, ConsoleColor.DarkGreen, "电脑");

            Random rd = new Random();// 随机数类
            bool Round_Contro = true;// 回合制控制变量，true为玩家回合，false为电脑回合
            int player_x = Player.x;
            int player_y = Player.y;
            int computer_x = Computer.x;
            int computer_y = Computer.y;
            char c;
            do
            {
                // 判断玩家与电脑是否重合
                if (CheckPosition(Player.x, Player.y, Computer.x, Computer.y))
                {
                    Console.SetCursorPosition(player_x * 2 + HeadX, player_y + HeadY);
                    Console.ForegroundColor = DefColor(BoardArr[player_y, player_x]);
                    Console.Write(BoardArr[player_y, player_x]);
                    Console.SetCursorPosition(computer_x * 2 + HeadX, computer_y + HeadY);
                    Console.ForegroundColor = DefColor(BoardArr[computer_y, computer_x]);
                    Console.Write(BoardArr[computer_y, computer_x]);

                    Console.SetCursorPosition(Player.x * 2 + HeadX, Player.y + HeadY);
                    Console.Write("  ");
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.SetCursorPosition(Player.x * 2 + HeadX, Player.y + HeadY);
                    Console.Write(CON);
                }
                else
                {
                    Console.SetCursorPosition(player_x * 2 + HeadX, player_y + HeadY);
                    Console.ForegroundColor = DefColor(BoardArr[player_y, player_x]);
                    Console.Write(BoardArr[player_y, player_x]);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.SetCursorPosition(Player.x * 2 + HeadX, Player.y + HeadY);
                    Console.Write(PLAYER);

                    Console.SetCursorPosition(computer_x * 2 + HeadX, computer_y + HeadY);
                    Console.ForegroundColor = DefColor(BoardArr[computer_y, computer_x]);
                    Console.Write(BoardArr[computer_y, computer_x]);
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.SetCursorPosition(Computer.x * 2 + HeadX, Computer.y + HeadY);
                    Console.Write(COMPUTER);
                }
                // 更新记录的玩家和电脑的x,y
                player_x = Player.x;
                player_y = Player.y;
                computer_x = Computer.x;
                computer_y = Computer.y;
                /*等待玩家输入*/
                c = Console.ReadKey(true).KeyChar;
                int rd_num = rd.Next(1, 7);// 生成随机数
                // “回车”进行下一步， 其他的则继续输入
                switch (c)
                {
                    case '\r':
                        if (Round_Contro)
                        {
                            GamerMove(ref Player, rd_num);
                            // 判断胜利
                            if (Player.y > 24 || (Player.x == 24 && Player.y == 24))
                                GameOver("玩家");
                            MeetEvent(ref Player, ref BoardArr, rd_num);
                        }
                        else
                        {
                            GamerMove(ref Computer, rd_num);
                            // 判断胜利
                            if (Computer.y > 24 || (Computer.x == 24 && Computer.y == 24))
                                GameOver("电脑");
                            MeetEvent(ref Computer, ref BoardArr, rd_num);
                        }
                        break;
                    default:
                        continue;
                }
                Round_Contro = !Round_Contro;// 反回合


            } while (true);
        }

        /*游戏结束*/
        static void GameOver(string winer)
        {
            Console.Clear();
            Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{winer}获胜");
            Console.SetCursorPosition(LONG / 2 - 8, WIDTH - 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("'w'、's'---上下选择");
            Console.SetCursorPosition(LONG / 2 - 8, WIDTH - 1);
            Console.WriteLine("\"回车\"---确认");

            char c = ' ';
            bool m = true;
            do
            {
                Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 2 - 5);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 2 - 5);
                Console.ForegroundColor = m ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine("重新游戏");
                Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 2 - 3);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 2 - 3);
                Console.ForegroundColor = m ? ConsoleColor.White : ConsoleColor.Red;
                Console.WriteLine("退出游戏");

                c = Console.ReadKey(true).KeyChar;
                switch (c)
                {
                    case 'W':
                    case 'w':
                    case 'S':
                    case 's':
                        m = !m;
                        break;
                    case '\r':
                        if (m)
                            GameOn();
                        else
                            Environment.Exit(0);
                        break;
                }
            } while (true);
        }

        static void Main(string[] args)
        {
            // 设置窗口大小
            Console.SetWindowSize(LONG, WIDTH);
            Console.SetBufferSize(LONG, WIDTH);
            // 设置背景颜色
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            // 设置光标为不可见
            Console.CursorVisible = false;

            // 开始游戏
            GameBegin();

            Console.ReadKey();
        }
    }
}
