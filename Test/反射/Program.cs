using System.Reflection;

namespace 反射
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 先得到类库
            Assembly assembly = Assembly.LoadFrom(@"D:\github Code\C_sharp\Test\类库工程\bin\Debug\类库工程");
            // 再得到类库里的类型
            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; ++i)
            {
                Console.WriteLine(types[i]);
            }
            // 得到类库里的确切的类
            Type player = assembly.GetType("类库工程.Player");
            // 得到该类的成员
            MemberInfo[] memberInfos = player.GetMembers();
            for (int i = 0; i < memberInfos.Length; ++i)
            {
                Console.WriteLine(memberInfos[i]);
            }
            // 得到类里确切的某个成员方法
            MethodInfo Display = player.GetMethod("Display");
            // 通过类的构造函数来初始化一个类，用object接收
            object newplayer = Activator.CreateInstance(player, "老王", 10, 15, 23, 20);
            // 调用得到的成员方法
            Display.Invoke(newplayer, null);
        }
    }
}
