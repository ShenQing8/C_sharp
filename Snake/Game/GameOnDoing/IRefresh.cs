using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    // 帧更新
    interface IRefresh
    {
        void Refresh();
        void Refresh(E_Move movement);

    }
}
