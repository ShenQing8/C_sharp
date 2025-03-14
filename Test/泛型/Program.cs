namespace 泛型
{
    class TypeOfT
    {

        public string typeofT<T>()
        {
            if (typeof(T) == typeof(int))
                return "整形，4个字节";
            else if (typeof(T) == typeof(char))
                return "字符型，1个字节";
            else if (typeof(T) == typeof(string))
                return $"字符串型，？个字节";
            else if (typeof(T) == typeof(float))
                return "单精度浮点型，4个字节";
            else
                return "其他类型";
        }
    }

    #region 泛型约束
    // 关键字：where
    // 1、值类型                        where 泛型字母:struct
    // 2、引用类型                      where 泛型字母:class
    // 3、存在无参公共构造函数            where 泛型字母:new()
    // 4、某个类本身或其派生类            where 泛型字母:类名
    // 5、某个结构的派生类               where 泛型字母:接口名
    // 6、另一个泛型类型本身或其派生类型   where 泛型字母:另一个泛型字母
    class Test1
    {

    }
    class Test2 : Test1
    {

    }
    class Regulate<T> where T: Test1
    {

    }

    // 单例模式的基类
    class SingleBase<T> where T : new()
    {
        // 有个小缺点，外部可以new出来
        private static T instance = new T();

        public static T Instance
        {
            get
            {
                return instance;
            }
        }
    }

    class GameObject : SingleBase<GameObject>
    {
        public int value = 10;
    }

    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            //TypeOfT ty = new TypeOfT();
            //string type = ty.typeofT<float>();
            //Console.WriteLine(type);

            //Regulate<Test1> regulate = new Regulate<Test1>();

            Console.WriteLine(GameObject.Instance.value);
            Console.WriteLine(SingleBase<int>.Instance);
            Action
        }
    }
}
