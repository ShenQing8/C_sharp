using Game.GameOnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Game.StaticMembers;

namespace Game
{
    class Block : IDraw
    {
        private Vector2 position;
        private BlockBack[] nowblock;
        private int nowblock_index;
        private Random random;
        private event Action<E_Move> move_action;


        public Block()
        {
            random = new Random();
            position.x = LENGTH / 2 - 3;
            position.y = -1;
            nowblock = new BlockBack[4];
            nowblock_index = 0;
            NewBlock();
            move_action += Sweep;
            move_action += Move;
        }

        public void MoveAction(E_Move move)
        {
            move_action(move);
            Draw();
        }

        // 随机生成一个方块
        private void NewBlock()
        {
            /*第一种概率设计*/
            // 7种初始情况，其中I型和方块型的生成概率是其他types的1/2
            //int n = random.Next(0, 72);
            //int k = 0;
            //if (n < 60)
            //    k = n / 12;
            //else
            //    k = (n + 6) / 12;
            //for (int i = 0; i < 4; ++i)
            //    nowblock[i] = blockTypes.blocktypes[k, i];
            /*第二种概率设计*/
            // 7种情况的概率相同
            int n = random.Next(0, 70);
            int k = n / 10;
            for (int i = 0; i < 4; ++i)
                nowblock[i] = blockTypes.blocktypes[k, i];
        }

        // 方块移动
        private void Move(E_Move move)
        {
            int tmpx = position.x;
            int tmpy = position.y;
            int index = nowblock_index;

            switch (move)
            {
                case E_Move.Left:
                    position.x -= 2;
                    break;
                case E_Move.Right:
                    position.x += 2;
                    break;
                case E_Move.Reverse:
                    nowblock_index = (nowblock_index + 1) % 4;
                    break;
                case E_Move.Down:
                    ++position.y;
                    break;
            }

            //if (position.x >= 2 && position.x < LENGTH - 8)
            //    return;


            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (nowblock[nowblock_index].isGrid[i, j] && (position.x + j * 2 < 2 || position.x + j * 2 > LENGTH - 3))
                    {
                        position.x = tmpx;
                        position.y = tmpy;
                        nowblock_index = index;
                        return;
                    }
                }
            }
        }

        // 判断方块是否落地
        private bool IsLanding(Map map)
        {
            // 落到最低点或落在其他方块上
            for (int i = 3; i > -1; --i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (nowblock[nowblock_index].isGrid[i, j])
                    {
                        if (position.y + i == WIDTH - 2 || map.mapinfo[position.y + i + 1, position.x / 2 + j])
                            return true;
                    }
                }
            }
            return false;
        }

        // 方块落下，更新方块
        public void Refresh(Map map)
        {
            for (int k = 0; k < map.map_width - 1; ++k)
            {
                if (IsLanding(map))
                {
                    for (int i = 0; i < 4; ++i)
                    {
                        for (int j = 0; j < 4; ++j)
                        {
                            if (nowblock[nowblock_index].isGrid[i, j])
                            {
                                map.mapinfo[position.y + i, position.x / 2 + j] = true;
                            }
                        }
                    }
                    map.Draw();
                    Thread.Sleep(100);
                    map.IsDefeat();
                    map.RemoveLine();
                    map.Draw();
                    // 生成新方块
                    position.x = LENGTH / 2 - 3;
                    position.y = 0;
                    nowblock_index = 0;
                    NewBlock();
                }
            }

        }

        private void Sweep(E_Move move)
        {

            for (int i = 0; i < 4; ++i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (nowblock[nowblock_index].isGrid[i, j] && position.y + i > 0)
                    {
                        Console.SetCursorPosition(position.x + j * 2, position.y + i);
                        Console.Write("  ");
                    }
                }
            }
        }

        // 实现绘制接口
        public void Draw()
        {
            Console.ForegroundColor = (ConsoleColor)E_GridColor.Fallen;
            for (int i = 3; i >-1; --i)
            {
                for (int j = 0; j < 4; ++j)
                {
                    if (nowblock[nowblock_index].isGrid[i, j] && position.y + i > 0)
                    {
                        Console.SetCursorPosition(position.x + j * 2, position.y + i);
                        Console.Write((char)E_GridShape.Shape);
                    }
                }
            }
        }
    }
}