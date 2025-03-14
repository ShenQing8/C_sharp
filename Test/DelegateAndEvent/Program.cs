namespace DelegateAndEvent
{
    class WaterBurner
    {
        private event Action myEvent;


        public WaterBurner()
        {
            myEvent += WaterTemper;
            myEvent += Screen;

        }
        public void Burn()
        {
            Console.WriteLine("开始加热~~~");
            Thread.Sleep(5000);
            myEvent();
        }
        private void WaterTemper()
        {
            Console.WriteLine("水的温度达到了95℃");
        }
        private void Screen()
        {
            Console.WriteLine("水烧开了");
        }
    }

    #region 匿名函数
    class Dedddddd
    {
        // 闭包
        public Func<int,int> Cheng(int a)
        {
            return delegate (int b)
            {
                return a * b;
            };
        }

        public Action OneToTen()
        {
            Action ret = null;
            for (int i = 1; i < 11; ++i)
            {
                int index = i;
                ret += () =>
                {
                    Console.WriteLine(index);
                };
            }
            return ret;
        }
    }
    #endregion

    #region List里的Sort排序
    class BagItems
    {
        public int money;
        public BagItems(int money)
        {
            this.money = money;
        }
    }

    class Items : IComparable<Items>
    {
        int LeiXing;
        int PinZhi;
        int MingZi;
        public Items(int leiXing, int pinZhi, int mingZi)
        {
            LeiXing = leiXing;
            PinZhi = pinZhi;
            MingZi = mingZi;
        }

        public int CompareTo(Items? other)
        {
            // 类型 > 品质 > 名字
            if (this.LeiXing != other.LeiXing)
            {
                return this.LeiXing > other.LeiXing ? 1 : -1;
            }
            else if (this.PinZhi != other.PinZhi)
            {
                return this.PinZhi > other.PinZhi ? 1 : -1;
            }
            else
            {
                return this.MingZi > other.MingZi ? 1 : -1;
            }
        }
    }
    #endregion

    internal class Program
    {
        static void Main(string[] args)
        {
            //WaterBurner waterBurner = new WaterBurner();
            //waterBurner.Burn();

            //Dedddddd test = new Dedddddd();
            //Console.WriteLine(test.Cheng(6)(6));
            //test.OneToTen()();

            //List<BagItems> bag = new List<BagItems>();
            //bag.Add(new BagItems(2));
            //bag.Add(new BagItems(5));
            //bag.Add(new BagItems(3));
            //bag.Add(new BagItems(1));
            //bag.Add(new BagItems(6));
            //bag.Add(new BagItems(4));
            //bag.Sort((BagItems a, BagItems b) => { return a.money > b.money ? 1 : -1; });
            //for (int i = 0; i < bag.Count; ++i)
            //{
            //    Console.WriteLine(bag[i].money);
            //}
        }
    }
}
