using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Net.WebSockets;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;


namespace ConsoleApp2
{
    /// <summary>
    /// 地图方块形状
    /// </summary>
    enum E_GridShap
    {
        /// <summary>
        /// 普通方块
        /// </summary>
        SIMPLE = '□',
        /// <summary>
        /// 炸弹
        /// </summary>
        DAMN = '●',
        /// <summary>
        /// 时空隧道
        /// </summary>
        SUIDAO = '∠',
        /// <summary>
        /// 暂停一回合
        /// </summary>
        STOP = '‖',
        /// <summary>
        /// 玩家
        /// </summary>
        PLAYER = '★',
        /// <summary>
        /// 电脑
        /// </summary>
        COMPUTER = '▲',
        /// <summary>
        /// 玩家与电脑重合
        /// </summary>
        CON = '6'
    }

    /// <summary>
    /// 地图方块颜色
    /// </summary>
    enum E_GridColor
    {
        SIMPLE = ConsoleColor.White,
        DAMN = ConsoleColor.Red,
        SUIDAO = ConsoleColor.Blue,
        STOP = ConsoleColor.Yellow,
        PLAYER = ConsoleColor.DarkMagenta,
        COMPUTER = ConsoleColor.DarkGreen,
        CON = ConsoleColor.DarkYellow
    }
    
    struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    /*方块结构体*/
    struct Grid
    {
        public Vector2 vector;
        public char shap;
        public ConsoleColor color;

        public Grid(int x, int y, char shap, ConsoleColor color)
        {
            vector = new Vector2(x, y);
            this.shap = shap;
            this.color = color;
        }
    }

    /*地图结构体*/
    struct Map
    {
        public Grid[] grids;

        public Map(int num, int x, int y, int l, int w)// x,y为地图起始位置，l为地图横向方块数，w为地图竖向方块数
        {
            // 接收参数，初始化地图数组
            grids = new Grid[num];

            int indexX = 0;
            int indexY = 0;
            int f = 1;
            // 生成随机数，确定普通方块及特殊事件的出现概率
            Random rd = new Random();
            for(int i = 0; i < num; ++i)
            {
                int rd_num = rd.Next(1, 101);
                if(rd_num <= 85 || i == 0 || i == num - 1)
                {
                    grids[i].shap = (char)E_GridShap.SIMPLE;
                    grids[i].color = (ConsoleColor)E_GridColor.SIMPLE;
                }
                else if (rd_num <= 90)
                {
                    grids[i].shap = (char)E_GridShap.DAMN;
                    grids[i].color = (ConsoleColor)E_GridColor.DAMN;
                }
                else if (rd_num <= 95)
                {
                    grids[i].shap = (char)E_GridShap.SUIDAO;
                    grids[i].color = (ConsoleColor)E_GridColor.SUIDAO;
                }
                else
                {
                    grids[i].shap = (char)E_GridShap.STOP;
                    grids[i].color = (ConsoleColor)E_GridColor.STOP;
                }
                // 初始化地图格子坐标
                grids[i].vector.x = x;
                grids[i].vector.y = y;
                if (indexX == l)// l暂定为24
                {
                    ++y;
                    ++indexY;
                    if (indexY == w)
                    {
                        indexX = 0;
                        indexY = 0;
                        f = -f;
                    }
                }
                else
                {
                    x += 2 * f;
                    ++indexX;
                }
            }
        }

        public void Draw()
        {
            for(int i = 0; i < grids.Length; ++i)
            {
                Console.ForegroundColor = grids[i].color;
                Console.SetCursorPosition(grids[i].vector.x, grids[i].vector.y);
                Console.Write(grids[i].shap);
            }
        }
    }

    /*游戏人结构体*/
    struct Gamer
    {
        public int nowIndex;
        public char shap;
        public ConsoleColor color;
        public string name;
        public bool stop = false;

        public Gamer(int index, string name)
        {

            nowIndex = index;
            shap = name == "玩家" ? (char)E_GridShap.PLAYER : (char)E_GridShap.COMPUTER;
            color = name == "玩家" ? (ConsoleColor)E_GridColor.PLAYER : (ConsoleColor)E_GridColor.COMPUTER;
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
        const int CHESSL = 24;
        const int CHESSW = 2;
        // 棋盘起点
        const int HeadX = 15;
        const int HeadY = 6;
        #endregion

        /*打印墙*/
        static void DisplayBoard()
        {
            // 设置墙的颜色
            Console.ForegroundColor = ConsoleColor.Red;
            // 横墙
            Console.SetCursorPosition(2, 0);
            for (int i = 2; i < LONG - 1; i += 2)
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
            Console.Write($"{(char)E_GridShap.SIMPLE}---普通格子");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"{(char)E_GridShap.DAMN}---炸弹，后退5格");
            Console.SetCursorPosition(2, WIDTH * 3 / 4);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"{(char)E_GridShap.SUIDAO}---传送门，随机传送，至多前进或后退6格");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{(char)E_GridShap.STOP}---时停，暂停行动一回合");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 2);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write($"{(char)E_GridShap.PLAYER}---玩家");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write($"    {(char)E_GridShap.COMPUTER}---电脑");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 3);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write($"{(char)E_GridShap.CON}---玩家与电脑重合");
            Console.SetCursorPosition(2, WIDTH * 3 / 4 + 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("按任意键进行操作");
        }

        #region 打印操作信息
        static void Sweep()
        {
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 1);
            Console.Write("                                      ");
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
            Console.Write("                                      ");
        }
        static void DisplayMoveInfo(ref Gamer gamer, int step)
        {
            Sweep();
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 1);
            Console.ForegroundColor = gamer.color;
            Console.Write($"{gamer.name}摇到了{step}，向前移动{step}格");
        }
        static void EventDamn(ref Gamer gamer)
        {
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
            Console.ForegroundColor = gamer.color;
            Console.Write($"{gamer.name}遇到了炸弹，后退5格");
        }
        static void EventStop(ref Gamer gamer)
        {
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
            Console.ForegroundColor = gamer.color;
            Console.Write($"{gamer.name}遇到了时停，暂停一回合");
        }
        static void EventSuidao(ref Gamer gamer, int step)
        {
            Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
            Console.ForegroundColor = gamer.color;
            if(step > 0)
                Console.Write($"{gamer.name}遇到了时空隧道, 前进{step}格");
            else
                Console.Write($"{gamer.name}遇到了时空隧道, 后退{step}格");
        }
        #endregion

        static void DrawPopMap(ref Map map, int reg)
        {
            Console.SetCursorPosition(map.grids[reg].vector.x, map.grids[reg].vector.y);
            Console.ForegroundColor = map.grids[reg].color;
            Console.Write(map.grids[reg].shap);
        }

        /*移动函数*/
        static bool GamerMove(ref Gamer gamer, ref Map map)
        {
            // 记录初始gamer的坐标
            int reg = gamer.nowIndex;
            // 生成随机数
            Random rd = new Random();
            int rd_num = rd.Next(1, 7);
            gamer.nowIndex += rd_num;
            // 若走到尽头则胜利
            if(gamer.nowIndex > map.grids.Length - 1)
            {
                gamer.nowIndex = map.grids.Length - 1;
                DrawPopMap(ref map, reg);
                return true;
            }
            // 打印移动信息
            DisplayMoveInfo(ref gamer, rd_num);
            // 判断前进后的格子类型，不同格子不同处理
            switch (map.grids[gamer.nowIndex].shap)
            {
                case (char)E_GridShap.SIMPLE:
                    break;
                case (char)E_GridShap.DAMN:
                    gamer.nowIndex -= 5;
                    EventDamn(ref gamer);
                    if (gamer.nowIndex < 0)
                        gamer.nowIndex = 0;
                    break;
                case (char)E_GridShap.STOP:
                    gamer.stop = true;
                    EventStop(ref gamer);
                    break;
                case (char)E_GridShap.SUIDAO:
                    rd_num = rd.Next(-6, 7);
                    gamer.nowIndex += rd_num;
                    EventSuidao(ref gamer, rd_num);
                    if (gamer.nowIndex < 0)
                        gamer.nowIndex = 0;
                    else if(gamer.nowIndex > map.grids.Length - 1)
                    {
                        gamer.nowIndex = map.grids.Length - 1;
                        DrawPopMap(ref map, reg);
                        return true;
                    }
                    break;
            }

            DrawPopMap(ref map, reg);
            return false;
        }


        /*绘制玩家与电脑*/
        static void DrawGamers(ref Gamer player, ref Gamer computer, ref Map map)
        {
            if(player.nowIndex == computer.nowIndex)
            {
                Console.SetCursorPosition(map.grids[player.nowIndex].vector.x, map.grids[player.nowIndex].vector.y);
                Console.ForegroundColor = (ConsoleColor)E_GridColor.CON;
                Console.Write((char)E_GridShap.CON);
            }
            else
            {
                Console.SetCursorPosition(map.grids[player.nowIndex].vector.x, map.grids[player.nowIndex].vector.y);
                Console.ForegroundColor = (ConsoleColor)E_GridColor.PLAYER;
                Console.Write((char)E_GridShap.PLAYER);

                Console.SetCursorPosition(map.grids[computer.nowIndex].vector.x, map.grids[computer.nowIndex].vector.y);
                Console.ForegroundColor = (ConsoleColor)E_GridColor.COMPUTER;
                Console.Write((char)E_GridShap.COMPUTER);
            }
        }

        /*Gamer移动板块*/
        static void GamerRound(ref Gamer gamer, ref Map map)
        {
            if(gamer.stop)
            {
                Sweep();
                EventStop(ref gamer);
                gamer.stop = false;
            }
            else
            {
                if(GamerMove(ref gamer, ref map))
                {
                    Sweep();
                    Console.SetCursorPosition(2, WIDTH * 7 / 8 + 2);
                    Console.ForegroundColor = gamer.color;
                    Console.Write($"{gamer.name}到达终点，获得胜利！！！");
                    Console.SetCursorPosition(2, WIDTH * 7 / 8 + 3);
                    Console.ForegroundColor = gamer.color;
                    Console.Write("          按任意键结束游戏");
                    Console.ReadKey(true);
                    GameSense("结束界面");
                }
            }
        }

        /*开始与结束界面*/
        static void GameSense(string sp)
        {
            Console.Clear();
            // 设置光标位置
            Console.SetCursorPosition(LONG / 2 - 4, WIDTH / 2 - 7);
            // 设置字体颜色
            Console.ForegroundColor = ConsoleColor.White;
            // 打印
            Console.WriteLine(sp == "开始界面" ? "大富翁" : "The End");
            Console.SetCursorPosition(LONG / 2 - 8, WIDTH - 2);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("'w'、's'---上下选择");
            Console.SetCursorPosition(LONG / 2 - 8, WIDTH - 1);
            Console.WriteLine("\"回车\"---确认");

            // 接收的参数
            char c = ' ';
            bool m = true;
            do
            {
                // 先清除再打印
                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 5);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 5);
                Console.ForegroundColor = m ? ConsoleColor.Red : ConsoleColor.White;
                Console.WriteLine(sp == "开始界面" ? "开始游戏" : "重新游戏");

                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 3);
                Console.WriteLine("          ");
                Console.SetCursorPosition(LONG / 2 - 5, WIDTH / 2 - 3);
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
                        if (m)
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

            // 创建地图结构体
            Map map = new Map(300, HeadX, HeadY, CHESSL, CHESSW);
            map.Draw();

            // 创建玩家与电脑
            Gamer Player = new Gamer(0, "玩家");
            Gamer Computer = new Gamer(0, "电脑");

            while(true)
            {
                DrawGamers(ref Player, ref Computer, ref map);

                // 接收玩家输入，进行下一步
                Console.ReadKey(true);
                GamerRound(ref Player, ref map);
                DrawGamers(ref Player, ref Computer, ref map);

                Console.ReadKey(true);
                GamerRound(ref Computer, ref map);
                DrawGamers(ref Player, ref Computer, ref map);

            }
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
            GameSense("开始界面");

            Console.ReadKey();
        }
    }
}
