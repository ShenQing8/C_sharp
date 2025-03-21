using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    struct BlockTypes
    {
        public BlockBack[,] blocktypes;
        
        public BlockTypes()
        {
            blocktypes = new BlockBack[7, 4];
            for (int i = 0; i < 7; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    blocktypes[i, j].isGrid = new bool[4, 4];
                }
            }
            /*初始化28种情况*/
            // T字型
            for (int i = 0; i < 3; ++i)
                blocktypes[0, 0].isGrid[1, i] = true;
            blocktypes[0, 0].isGrid[2, 1] = true;
            // L型
            for (int i = 1; i < 4; ++i)
                blocktypes[1, 0].isGrid[i, 1] = true;
            blocktypes[1, 0].isGrid[3, 2] = true;
            // 反L型
            for (int i = 1; i < 4; ++i)
                blocktypes[2, 0].isGrid[i, 2] = true;
            blocktypes[2, 0].isGrid[3, 1] = true;
            // Z型
            for (int i = 0; i < 2; ++i)
                blocktypes[3, 0].isGrid[1, i] = true;
            for (int i = 1; i < 3; ++i)
                blocktypes[3, 0].isGrid[2, i] = true;
            // 反Z型
            for (int i = 0; i < 2; ++i)
                blocktypes[4, 0].isGrid[2, i] = true;
            for (int i = 1; i < 3; ++i)
                blocktypes[4, 0].isGrid[1, i] = true;
            // 正方形
            for (int i = 1; i < 3; ++i)
                blocktypes[5, 0].isGrid[1, i] = true;
            for (int i = 1; i < 3; ++i)
                blocktypes[5, 0].isGrid[2, i] = true;
            // 竖型
            for (int i = 0; i < 4; ++i)
                blocktypes[6, 0].isGrid[i, 1] = true;

            BlockBack tmp = new BlockBack();
            tmp.isGrid = new bool[4, 4];
            for (int i = 0; i < 7; ++i)
            {
                for (int j = 1; j < 4; ++j)
                {
                    // 暂存上一个的状态
                    for (int m = 0; m < 4; ++m)
                    {
                        for (int n = 0; n < 4; ++n)
                        {
                            tmp.isGrid[m, n] = blocktypes[i, j - 1].isGrid[m, n];
                        }
                    }
                    // 将上一个顺时针旋转90°
                    for (int m = 0; m < 4; ++m)
                    {
                        for (int n = 0; n < 4; ++n)
                        {
                            blocktypes[i, j].isGrid[m, n] = tmp.isGrid[-(n - 3), m];
                        }
                    }
                }
            }
        }
    }
}
