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

        static void Rd_test()
        {
            Random rd = new Random();
            for(int i = 0; i < 20; ++i)
            {
                Console.Write($"{rd.Next(1, 7)} ");
            }

        }
        static void Main(string[] args)
        {
            // TestButtn();
            Rd_test();
            //switch (Console.ReadKey(true).Key)
            //{
            //    ‖
            //}

        }
    }
}
