using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class GameOn
    {
        public void GameOnGoing()
        {
            Console.Clear();
            Wall wall = new Wall();
            wall.Draw();

            Console.ReadKey();
        }
    }
}
