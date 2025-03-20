using System.Collections;

namespace 迭代器
{
    // foreach遍历会调用 in 后面的 GetEnumerator，并返回一个 IEnumerator
    // 再调用返回的这个 IEnumerator 的 MoveNext 方法，并返回一个bool值
    // 若返回值为 true，则得到 Current ，赋值给 item（in 前面的）
    class CustomList1 : IEnumerable, IEnumerator
    {
        private int[] list;
        private int position = -1;

        public CustomList1()
        {
            list = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        }
        public IEnumerator GetEnumerator()
        {
            Reset();
            return this;
        }

        public object Current
        {
            get
            {
                return list[position];
            }
        }

        public bool MoveNext()
        {
            ++position;
            return position < list.Length;
        }

        public void Reset()
        {
            position = -1;
        }
    }

    // 语法糖方式实现迭代器
    class CustomList2 : IEnumerable
    {
        private int[] list;
        public CustomList2()
        {
            list = new int[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < list.Length; ++i)
            {
                // yield return 暂时返回，保留当前状态
                yield return list[i];
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            CustomList1 list1 = new CustomList1();
            foreach (int item in list1)
            {
                Console.WriteLine(item);
            }

            CustomList2 list2 = new CustomList2();
            foreach (int item in list2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
