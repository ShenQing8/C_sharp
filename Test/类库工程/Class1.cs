using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 类库工程
{
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


    public class Player
    {
        [Obsolete("非法操作，随意修改name的值", false)]
        [MyCustom("为Player类中的name成员变量添加了一个特性")]
        public string name;
        public int hp;
        public int atk;
        public int def;
        public int position;

        public Player() { }
        public Player(string name, int hp, int atk, int def, int position)
        {
            this.name = name;
            this.hp = hp;
            this.atk = atk;
            this.def = def;
            this.position = position;
        }

        public void Display()
        {
            Console.WriteLine(name);
            Console.WriteLine(hp);
            Console.WriteLine(atk);
            Console.WriteLine(def);
            Console.WriteLine(position);
        }
    }

    public class Class1
    {
    }
}
