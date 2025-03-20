using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    class GameOn : BaseBeginAndEndSences
    {
        public void GameWorking()
        {
            // 打印墙
            Wall wall = new Wall();
            wall.Display();

            // 生成蛇
            Snake snake = new Snake();
            snake.Display();
            // 生成食物
            Food food = new Food();
            food.FreshFood(snake);
            food.Display();
            // 将蛇和食物装进游戏物体对象
            GameObject game_object = new GameObject(snake, food);

            char order = ' ';
            char tmp_order = ' ';
            E_Move movement;
            order = Console.ReadKey(true).KeyChar;
            while (true)
            {
                // 检测键盘输入
                if (Console.KeyAvailable)
                {
                    tmp_order = Console.ReadKey(true).KeyChar;
                    switch (tmp_order)
                    {
                        case 'W':
                        case 'w':
                            if (order != 's' && order != 'S')// 防止反向
                                order = tmp_order;
                            break;
                        case 'A':
                        case 'a':
                            if (order != 'd' && order != 'D')// 防止反向
                                order = tmp_order;
                            break;
                        case 'S':
                        case 's':
                            if (order != 'w' && order != 'W')// 防止反向
                                order = tmp_order;
                            break;
                        case 'D':
                        case 'd':
                            if (order != 'a' && order != 'A')// 防止反向
                                order = tmp_order;
                            break;
                        default:
                            break;
                    }
                }
                // 蛇移动
                switch (order)
                {
                    case 'W':
                    case 'w':
                        movement = E_Move.UP;
                        break;
                    case 'A':
                    case 'a':
                        movement = E_Move.LEFT;
                        break;
                    case 'S':
                    case 's':
                        movement = E_Move.DOWN;
                        break;
                    case 'D':
                    case 'd':
                        movement = E_Move.RIGHT;
                        break;
                    default:
                        continue;
                }
                game_object.Refresh(movement);
                Thread.Sleep(150);// 蛇每0.2秒移动一次
            }

        }
    }
}