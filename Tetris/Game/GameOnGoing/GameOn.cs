using Game.GameOnGoing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Game.StaticMembers;


namespace Game
{
    class GameOn
    {
        private ConsoleKey a;
        private bool isRuninig;
        private Thread move_thread;
        private Wall wall;
        private Block block;
        public Map map;

        public GameOn()
        {
            // 生成墙
            wall = new Wall();
            block = new Block();
            map = new Map(this);
            // 开新线程用来方块移动
            move_thread = new Thread(MoveThread);
            move_thread.IsBackground = true;


            // 线程是否运行
            isRuninig = true;
        }

        public void GameOnGoing()
        {
            Console.Clear();
            // 绘制墙
            wall.Draw();
            // 绘制方块
            block.Draw();
            // 开启线程
            move_thread.Start();
            //InputThread.Instance.moveaction += MoveThread;

            while (true)
            {
                lock (block)
                {
                    block.MoveAction(E_Move.Down);
                    block.Refresh(map);
                }
                Thread.Sleep(300);
            }
        }

        public void ThreadEnd()
        {
            isRuninig = false;
            move_thread = null;
            //InputThread.Instance.moveaction -= MoveThread;
            program.GameOpenEnd(E_GameSences.End);
        }

        private void MoveThread()
        {
            while (isRuninig)
            {
                if (Console.KeyAvailable)
                {
                    lock (block)
                    {
                        //a = Console.ReadKey(true).Key;

                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.A:
                                block.MoveAction(E_Move.Left);
                                break;
                            case ConsoleKey.D:
                                block.MoveAction(E_Move.Right);
                                break;
                            case ConsoleKey.Spacebar:
                                block.MoveAction(E_Move.Reverse);
                                break;
                            case ConsoleKey.S:
                                block.MoveAction(E_Move.Down);
                                block.Refresh(map);
                                break;
                        }
                    }
                }
            }

        }
    }
}