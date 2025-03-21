using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.StaticMembers;


namespace Game
{
    class Wall : IDraw
    {
        public void Draw()
        {
            Console.ForegroundColor = (ConsoleColor)E_GridColor.Wall;

            // 横墙
            for (int i = 0; i < LENGTH; i += 2)
            {
                Console.SetCursorPosition(i, WIDTH - 1);
                Console.Write((char)E_GridShape.Shape);
            }
            // 竖墙
            for (int i = 0; i < WIDTH - 1; ++i)
            {
                Console.SetCursorPosition(0, i);
                Console.Write((char)E_GridShape.Shape);
                Console.SetCursorPosition(LENGTH - 2, i);
                Console.Write((char)E_GridShape.Shape);
            }
        }
    }
}
