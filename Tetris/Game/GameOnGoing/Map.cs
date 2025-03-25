using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.StaticMembers;

namespace Game.GameOnGoing
{
    class Map : IDraw
    {
        public bool[,] mapinfo;
        private int map_length;
        private int map_width;

        public Map()
        {
            map_length = LENGTH / 2 - 2;
            map_width = WIDTH - 1;
            mapinfo = new bool[WIDTH, LENGTH / 2];
        }

        // 消除一行
        public void RemoveLine()
        {
            bool flag = true;
            for (int i = map_width; i > 0; --i)
            {
                flag = true;
                for (int j = 1; j <= map_length; ++j)
                {
                    if (mapinfo[i, j] == false)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    for (int k = 1; k <= map_length; ++k)
                        mapinfo[i, k] = false;
                    DownLine(i);
                }
            }
        }

        // 整体向下移
        public void DownLine(int row)
        {
            for (int i = row; i > 0; --i)
            {
                for (int j = 1; j <= map_length; ++j)
                {
                    mapinfo[i, j] = mapinfo[i - 1, j];
                }
            }
        }

        // 判断是否失败
        public void IsDefeat()
        {
            for (int j = 1; j <= map_length; ++j)
            {
                if (mapinfo[1, j] == true)
                    program.GameOpenEnd(E_GameSences.End);
            }
        }

        public void Draw()
        {
            Console.ForegroundColor = (ConsoleColor)E_GridColor.Stable;
            for (int i = map_width - 1; i > -1; --i)
            {
                for (int j = 1; j <= map_length; ++j)
                {
                    Console.SetCursorPosition(2 * j, i);
                    if (mapinfo[i, j])
                        Console.Write((char)E_GridShape.Shape);
                    else
                        Console.Write("  ");
                }
            }
        }
    }
}
