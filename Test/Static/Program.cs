using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace STACK
{
    internal class Program
    {
        static void DecToBin(int num)
        {
            Console.Write($"{num}的二进制是：");
            Stack stack = new Stack();
            while (true)
            {
                stack.Push(num % 2);
                num /= 2;
                if (num == 0)
                    break;
            }
            while (stack.Count > 0)
            {
                Console.Write(stack.Pop());
            }
            Console.WriteLine();
        }
        static void Main(string[] args)
        {
            //DecToBin(10);
            //DecToBin(8);
            //DecToBin(22);
            //DecToBin(1);
            int a = 10;
            Console.Write(typeof(int));

        }
    }
}
