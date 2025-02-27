using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

namespace TestEnum
{
    /// <summary>
    /// QQ在线状态
    /// </summary>
    enum E_QQtype
    {
        /// <summary>
        /// 在线
        /// </summary>
        Online,
        /// <summary>
        /// 忙碌
        /// </summary>
        Busy,
        /// <summary>
        /// 离开
        /// </summary>
        Leave,
        /// <summary>
        /// 隐身
        /// </summary>
        Unvisible
    }
    class Program
    {
        static void Main(string[] args)
        {
            #region 
            try
            {
                Console.WriteLine("请选择在线状态(0：在线，1：忙碌，2：离开，3：隐身):");
                E_QQtype qqtype = (E_QQtype)int.Parse(Console.ReadLine());
                Console.WriteLine(qqtype);
            }
            catch
            {
                Console.WriteLine("请输入数字：");
            }
            #endregion

        }
    }
}
