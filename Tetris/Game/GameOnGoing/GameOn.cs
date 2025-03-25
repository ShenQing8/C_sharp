using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class GameOn
    {
        private ConsoleKey a;

        public void GameOnGoing()
        {
            Console.Clear();
            // 绘制墙
            Wall wall = new Wall();
            wall.Draw();
            // 生成新方块并绘制
            Block block = new Block();
            block.Draw();

            // 读取键盘操作
            // 开新线程用来方块移动
            Thread move_thread = new Thread(MoveThread);
            move_thread.IsBackground = true;
            move_thread.Start();

            // 正常下落速度 0.5秒
            int down_speed = 500;
            while (true)
            {
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
                        down_speed = 200;
                        break;
                    //default:
                    //    a = ConsoleKey.W;
                    //    break;
                }
                a = ConsoleKey.W;
                block.MoveAction(E_Move.Down);
                block.Refresh();
                Thread.Sleep(down_speed);
                down_speed = 500;
            }
        }

        public void MoveThread()
        {

            while (true)
            {
                a = Console.ReadKey(true).Key;
            }
        }
    }
}
