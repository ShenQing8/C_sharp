using System.Reflection;

namespace 反射
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Assembly assembly = Assembly.LoadFrom(@"D:\github Code\C_sharp\Test\类库工程\bin\Debug\类库工程");

            Type[] types = assembly.GetTypes();
            for (int i = 0; i < types.Length; ++i)
            {
                Console.WriteLine(types[i]);
            }

            Type player = assembly.GetType("类库工程.Player");
            MemberInfo[] memberInfos = player.GetMembers();
            for (int i = 0; i < memberInfos.Length; ++i)
            {
                Console.WriteLine(memberInfos[i]);
            }

            object newplayer = Activator.CreateInstance(player, "老王", 10, 15, 23, 20);
            MethodInfo Display = player.GetMethod("Display");
            Display.Invoke(newplayer, null);
        }
    }
}
