using System;
using System.Collections;

namespace TestArr
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] arr1 = [1, 2, 3, 4, 5, 6];
            //Console.WriteLine(arr1[1]);
            //int[] arr2 = new int[2];
            //Console.WriteLine("{0}, {1}", arr2[0], arr2[1]);
            //ArrayList arr3;// 动态数组
            //int[,] arr4 = new int[2, 3]{{1,2,3},
            //                            {2,3,4}};
            //int[,] arr5 = { { 1, 2, 3 }, { 4, 5, 6 } };
            //Console.WriteLine(arr5.GetLength(0));
            //Console.WriteLine(arr5.GetLength(1));

            #region 引用类型地址的测试
            //int[] arr1 = [1, 2, 3, 4, 5];
            //int[] arr2 = arr1;
            //arr2[0] = 5;
            //Console.WriteLine("{0},{1}", arr1[0], arr2[0]);

            //arr2 = [1, 2, 3, 4, 5];
            //Console.WriteLine("{0},{1}", arr1[0], arr2[0]);

            //string str1 = "123";
            //string str2 = str1;
            //str2 = "321";
            #endregion

            int[] a = new int[] { 10 };
            int[] b = a;
            b = new int[5];

        }
    }
}
