using System;


namespace Function
{
    class Program
    {
        static int add(float a, float b)
        {
            return (int)(a + b);
        }

        static void TestButtn()
        {
            char a = Console.ReadKey().KeyChar;
        
        }

        static void RandomNum_test()
        {
            Random rd = new Random();
            for(int i = 0; i < 20; ++i)
            {
                Console.Write($"{rd.Next(1, 7)} ");
            }
        }

        static void Switch_test()
        {
            int a;

            while (true)
            {
                a = int.Parse(Console.ReadLine());
                switch (a)
                {
                    case 1:
                        break;
                    case 0:
                        break;
                    // 总结：下面两行没用
                    default:
                        continue;
                }
            }
        }

        static void Main(string[] args)
        {
            // TestButtn();
            //RandomNum_test();
            //switch (Console.ReadKey(true).Key)
            //{
            //    ‖
            //}
            Switch_test();
        }
    }
}
