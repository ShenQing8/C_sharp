using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// 场景枚举
    /// </summary>
    enum E_GameSences
    {
        /// <summary>
        /// 开始
        /// </summary>
        Open,
        /// <summary>
        /// 结束
        /// </summary>
        End,
        /// <summary>
        /// 游戏进行中场景
        /// </summary>
        OnDoing
    }

    /// <summary>
    /// 方块形状
    /// </summary>
    enum E_GridShap
    {
        /// <summary>
        /// 墙
        /// </summary>
        Wall = '■',
        /// <summary>
        /// 蛇头
        /// </summary>
        Head = '●',
        /// <summary>
        /// 蛇身体
        /// </summary>
        Body = '□',
        /// <summary>
        /// 食物
        /// </summary>
        Food = '⭕'
    }

    /// <summary>
    /// 方块颜色
    /// </summary>
    enum E_GridColor
    {
        Wall = ConsoleColor.Red,
        Head = ConsoleColor.Magenta,
        Body = ConsoleColor.Green,
        Food = ConsoleColor.Yellow
    }

    /// <summary>
    /// 移动枚举
    /// </summary>
    enum E_Move
    {
        /// <summary>
        /// 上
        /// </summary>
        UP,
        /// <summary>
        /// 下
        /// </summary>
        DOWN,
        /// <summary>
        /// 左
        /// </summary>
        LEFT,
        /// <summary>
        /// 右
        /// </summary>
        RIGHT
    }

    class StaticMembers
    {
        // 窗口宽高
        static public int LENGTH = 90;
        static public int WIDTH = 25;
        // 蛇的初始位置
        public static int BirthX = 40;
        public static int BirthY = 10;
        // 游戏场景基类
        public static BaseBeginAndEndSences baseSences = new Program();
        public static GameOn gameon = new GameOn();
    }
}
