using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    /// <summary>
    /// 游戏场景枚举
    /// </summary>
    enum E_GameSences
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        Open,
        /// <summary>
        /// 结束场景
        /// </summary>
        End,
        /// <summary>
        /// 游戏中场景
        /// </summary>
        OnDoing
    }

    /// <summary>
    /// 各类方块颜色枚举
    /// </summary>
    enum E_GridColor
    {
        /// <summary>
        /// 墙的颜色
        /// </summary>
        Wall = ConsoleColor.Gray,
        /// <summary>
        /// 下落中的方块颜色
        /// </summary>
        Fallen = ConsoleColor.Green,
        /// <summary>
        /// 已经落下的方块颜色
        /// </summary>
        Stable = ConsoleColor.Red
    }

    /// <summary>
    /// 规定方块的形状
    /// </summary>
    enum E_GridShape
    {
        Shape = '■'
    }

    class StaticMembers
    {
        // 窗口宽高
        static public int LENGTH = 42;
        static public int WIDTH = 32;// 一行空出来，一行留给墙
        // 游戏场景基类
        static public Program program = new Program();
        static public GameOn gameon = new GameOn();
        // 28种block情况
        static public BlockTypes blockTypes = new BlockTypes();
    }
}
