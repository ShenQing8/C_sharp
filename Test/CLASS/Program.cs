using System.Reflection.Metadata.Ecma335;

namespace CLASS
{
    class Student
    {
        private string name;
        private string sex;
        private int age;
        private int CsharpGrate;
        private int UnityGrate;
        private int Sum_Grate;
        private float Ave_Grate;

        public Student(string name, string sex, int age, int csharpGrate, int unityGrate)
        {
            this.name = name;
            this.sex = sex;
            this.age = age;
            CsharpGrate = csharpGrate;
            UnityGrate = unityGrate;
            Sum_Grate = CsharpGrate + unityGrate;
            Ave_Grate = Sum_Grate / 2;
        }

        public void IntroduceMe()
        {
            Console.Write($"我叫{name},今年{age}岁了，是{sex}同学");
        }
        public void GetGrate()
        {
            Console.Write($"总分：{Sum_Grate}，平均分：{Ave_Grate}");
        }

        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    value = value < 0 ? 0 : 150;
                }
                age = value;
            }
        }
        public int Grate
        {
            get
            {
                return CsharpGrate;
            }
            set
            {
                if (value < 0 || value > 150)
                {
                    value = value < 0 ? 0 : 100;
                }
                CsharpGrate = value;
            }
        }
        public string Sex
        {
            get
            {
                return sex;
            }
            set
            {
                if (sex != "男" && sex != "女")
                {
                    value = "人妖";
                }

                sex = value;
            }
        }
    }

    class Array
    {
        private int[] arr;
        private int ValueIndex;
        private int ArrAcomdation;

        public Array(int[] arr)
        {
            this.arr = arr;
            ValueIndex = arr.Length;
            ArrAcomdation = arr.Length;
        }
        
        public void Add(int value)
        {
            if (ArrAcomdation == ValueIndex)
            {
                int[] new_arr = new int[ArrAcomdation * 2];
                for (int i = 0; i < ValueIndex; ++i)
                {
                    new_arr[i] = arr[i];
                }
                arr = new_arr;
                arr[ValueIndex] = value;

                ++ValueIndex;
                ArrAcomdation *= 2;
            }
            else
            {
                arr[ValueIndex] = value;
                ++ValueIndex;
            }
        }
        public void Del(int index)
        {
            for (int i = index; i < ValueIndex - 1; ++i)
            {
                arr[i] = arr[i + 1];
            }
            ValueIndex--;
        }
        public int this[int index]
        {
            get
            {
                return arr[index];
            }
            set
            {
                arr[index] = value;
            }
        }
    }

    /*拓展方法用静态类*/
    static class AppendFunc
    {
        public static float GetSqure(this int value)
        {
            return value * value;
        }
    }

    #region 继承
    class Worker
    {
        public string kind;
        public string neirong;

        public Worker(string kind, string neirong)
        {
            this.kind = kind;
            this.neirong = neirong;
        }

        public void Working()
        {
            Console.WriteLine($"{kind}{neirong}中······");
        }
    }
    class Coder : Worker
    {
        public Coder(string kind, string neirong):base(kind, neirong)
        {
           
        }
    }
    class Cehua : Worker
    {
        public Cehua(string kind, string neirong) : base(kind, neirong)
        {

        }
    }
    class Art : Worker
    {
        public Art(string kind, string neirong) : base(kind, neirong)
        {

        }
    }
    #endregion

    #region 多态
    class RealDuck
    {
        public virtual void Voice()
        {
            Console.WriteLine("嘎嘎叫~");
        }
    }
    class WoodDuck : RealDuck
    {
        public override void Voice()
        {
            Console.WriteLine("吱吱叫~");
        }
    }
    class PiDuck : RealDuck
    {
        public override void Voice()
        {
            Console.WriteLine("嘟嘟叫~");
        }
    }
    #endregion

    #region 接口
    interface ICheckUp
    {
        void CheckUp();
    }
    class People : ICheckUp
    {
        public void CheckUp()
        {
            Console.WriteLine("人到派出所登记");
        }
    }
    class Car : ICheckUp
    {
        public void CheckUp()
        {
            Console.WriteLine("车到车管所登记");
        }
    }
    class House : ICheckUp
    {
        public void CheckUp()
        {
            Console.WriteLine("房子到房管局登记");
        }
    }
    // 飞、游泳、走
    interface IFly
    {
        void Fly();
    }
    interface ISwim
    {
        void Swim();
    }
    interface IWalk
    {
        void Walk();
    }
    class LittleBird : IFly, IWalk
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Walk()
        {
            throw new NotImplementedException();
        }
    }
    class TuoNiao : IWalk
    {
        public void Walk()
        {
            throw new NotImplementedException();
        }
    }
    class QiE : ISwim, IWalk
    {
        public void Swim()
        {
            throw new NotImplementedException();
        }

        public void Walk()
        {
            throw new NotImplementedException();
        }
    }
    // 模拟电脑传输数据
    interface IUSB
    {

    }

    interface IMemory : IUSB
    {
        void memory();
    }
    interface IDisplay : IUSB
    {
        void display();
    }
    class MoveMemoory : IMemory
    {
        public void memory()
        {
            Console.WriteLine("移动硬盘存储");
        }
    }
    class UPan : IMemory
    {
        public void memory()
        {
            Console.WriteLine("U盘存储");
        }
    }
    class Mp3 : IDisplay
    {
        public void display()
        {
            Console.WriteLine("Mp3播放");
        }
    }
    class Computer
    {
        public IUSB equip;
        public Computer()
        {

        }
        public Computer(IUSB equip)
        {
            this.equip = equip;
        }
    }

    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            //Student s1 = new Student("张三", "男", 18, 100, 100);
            //s1.IntroduceMe();
            //s1.GetGrate();
            //s1.Age = 160;
            //s1.Grate = 110;
            //s1.Sex = "不男不女";
            //s1.IntroduceMe();
            //s1.GetGrate();

            //Array int_arr = new Array(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);
            //int_arr.Add(100);

            //int i = 12;
            //Console.WriteLine(i.GetSqure());

            //Coder coder = new Coder("程序员", "敲代码");
            //Cehua cehua = new Cehua("策划", "策划东西");
            //Art art = new Art("美术", "画画");
            //coder.Working();
            //cehua.Working();
            //art.Working();

            //RealDuck duck1 = new WoodDuck();
            //RealDuck duck2 = new PiDuck();
            //duck1.Voice();
            //duck2.Voice();

            //ICheckUp people = new People();
            //ICheckUp car = new Car();
            //ICheckUp house = new House();
            //people.CheckUp();
            //car.CheckUp();
            //house.CheckUp();
            //

            Computer computer = new Computer(new MoveMemoory());
            (computer.equip as MoveMemoory).memory();
            computer.equip = new Mp3();
            (computer.equip as Mp3).display();

        }
    }
}
