using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 类库工程
{
    public class Player
    {
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
