using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class InputThread
    {
        // 线程成员变量
        private Thread movethread;
        // 输入检测事件
        public event Action moveaction;

        private static InputThread inputThread = new InputThread();

        public static InputThread Instance
        {
            get
            {
                return inputThread;
            }
        }

        private InputThread() 
        {
            movethread = new Thread(MoveThread);
            movethread.IsBackground = true;
            movethread.Start();
        }

        private void MoveThread()
        {
            while (true)
            {
                moveaction?.Invoke();
            }
        }
    }
}
