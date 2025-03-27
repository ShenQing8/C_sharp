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
        private int down_speed = 300;
        private bool isRuninig = true;

        public void GameOnGoing()
        {
            Console.Clear();
            // 绘制墙
            Wall wall = new Wall();
            wall.Draw();
            // 生成新方块并绘制
            block.Draw();

            // 开新线程用来方块移动
            Thread move_thread = new Thread(MoveThread);
            move_thread.IsBackground = true;
            move_thread.Start();

            while (true)
            {

                if (Console.KeyAvailable)
                {
                    lock (block)
                    {
                        a = Console.ReadKey(true).Key;
                        switch (a)
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
                                if (block.Refresh())
                                {

                                    program.GameOpenEnd(E_GameSences.End);
                                }
                                break;
                        }
                    }
                }
            }
        }

        //private void ThreadEnd()
        //{
        //    isRuninig = false;
        //}

        public void MoveThread()
        {
            while (isRuninig)
            {
                lock (block)
                {
                    block.MoveAction(E_Move.Down);
                    block.Refresh();
                }
                Thread.Sleep(down_speed);
            }
        }
    }
}