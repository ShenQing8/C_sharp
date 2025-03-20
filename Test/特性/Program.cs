using System.Reflection;

namespace 特性
{
    // 特性的本质是个类
    // 我们可以利用特性类来为元数据添加额外信息
    // 比如一个类、成员方法、成员方法里的参数、成员变量等为他们添加更更多的额外信息
    // 之后可以通过反射来获取这些额外信息

    // 继承特性基类
    class MyCustomAttribute : Attribute
    {
        public string info;

        public MyCustomAttribute(string info)
        {
            this.info = info;
        }

        public void TestFunc()
        {
            Console.WriteLine("特性的方法");
        }
    }

    [MyCustom("这是一个")]
    class MyClass
    {

    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom(@"D:\github Code\C_sharp\Test\类库工程\bin\Debug\类库工程");

            Type Player = assembly.GetType("类库工程.Player");

            MethodInfo Display = Player.GetMethod("Display");

            FieldInfo[] fieldInfos = Player.GetFields();
            for (int i = 0; i < fieldInfos.Length; ++i)
            {
                Console.WriteLine(fieldInfos[i]);
            }

            FieldInfo strname = Player.GetField("name");

            object newplayer = Activator.CreateInstance(Player, "老王", 10, 15, 23, 20);
            //Console.WriteLine(newplayer);
            Display.Invoke(newplayer, null);

            // 先得到自定义特性的Type
            Type attribute = assembly.GetType("类库工程.MyCustomAttribute");
            if (strname.GetCustomAttribute(attribute) != null)
            {
                Console.WriteLine("非法操作，随意修改name成员");
            }
            else
            {
                strname.SetValue(newplayer, "老李");
            }
            Display.Invoke(newplayer, null);

        }
    }
}
