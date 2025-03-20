namespace MultiThread
{
    internal class Program
    {
        static ConsoleKey Dir = ConsoleKey.Enter;
        static int Xindex = 10;
        static int Yindex = 10;
        static void Main(string[] args)
        {
            //Thread t = new Thread(NewThreadLogic);
            //t.Start();
            //t.IsBackground = true;// 设置为后台线程，当主线程结束时，后台线程也跟着结束
            //Thread.Sleep(1000);

            Thread dir = new Thread(MoveThread);
            dir.IsBackground = true;
            dir.Start();

            Console.CursorVisible = false;
            Display(0, 0);

            while (true)
            {
                Sweep();
                switch (Dir)
                {
                    case ConsoleKey.W:
                        Display(0, -1);
                        break;
                    case ConsoleKey.A:
                        Display(-2, 0);
                        break;
                    case ConsoleKey.S:
                        Display(0, 1);
                        break;
                    case ConsoleKey.D:
                        Display(2, 0);
                        break;
                    default:
                        continue;
                }
                Thread.Sleep(200);
            }

        }
        
        static void Display(int x,int y)
        {
            Xindex += x;
            Yindex += y;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Xindex, Yindex);
            Console.Write("★");
        }
        static void Sweep()
        {
            Console.SetCursorPosition(Xindex, Yindex);
            Console.Write("  ");
        }
        static void MoveThread()
        {
            while (true)
            {
                Dir = Console.ReadKey(true).Key;
            }
        }

        static void NewThreadLogic()
        {
            while (true)
            {
                Console.WriteLine("多线程任务进行中······");
            }
        }
    }
}
