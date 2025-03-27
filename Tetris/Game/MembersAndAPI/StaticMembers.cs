using Game.GameOnGoing;
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

    /// <summary>
    /// 移动枚举
    /// </summary>
    enum E_Move
    {
        /// <summary>
        /// 左
        /// </summary>
        Left,
        /// <summary>
        /// 右
        /// </summary>
        Right,
        /// <summary>
        /// 旋转
        /// </summary>
        Reverse,
        /// <summary>
        /// 下落
        /// </summary>
        Down
    }

    class StaticMembers
    {
        // 窗口宽高
        static public int LENGTH = 42;// 必须要是偶数
        static public int WIDTH = 32;// 一行空出来，一行留给墙
        static public int WIDTH_BUFFER = WIDTH + 1;
        // 28种block情况
        static public BlockTypes blockTypes = new BlockTypes();
        // 游戏场景基类
        static public Program program = new Program();
        //static public GameOn gameon = new GameOn();
        // 场景地图
        //static public Map map = new Map();
        // 生成的方块
        //static public Block block = new Block();
    }
}
