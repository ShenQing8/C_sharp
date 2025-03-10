using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Game.StaticMembers;

namespace Game
{
    // 坐标
    struct Vector2
    {
        public int x;
        public int y;

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Vector2 vector1, Vector2 vector2)
        {
            if (vector1.x == vector2.x && vector1.y == vector2.y)
                return true;
            return false;
        }
        public static bool operator !=(Vector2 vector1, Vector2 vector2)
        {
            if (vector1.x == vector2.x && vector1.y == vector2.y)
                return false;
            return true;
        }
    }

    // 方块
    class Grid : IDisplay
    {
        // 位置
        public Vector2 vector;
        // 颜色
        public ConsoleColor color;
        // 形状
        public char shap;

        // 构造函数
        public Grid(Vector2 vector, ConsoleColor color, char shap)
        {
            this.vector = vector;
            this.color = color;
            this.shap = shap;
        }

        // 实现绘制接口
        public void Display()
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(vector.x, vector.y);
            Console.Write(shap);
        }
    }

    // 墙
    class Wall : IDisplay
    {
        public void Display()
        {
            Console.Clear();
            // 设置颜色
            Console.ForegroundColor = (ConsoleColor)E_GridColor.Wall;
            // 横墙
            for (int i = 2; i < LENGTH - 2; i += 2)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write((char)E_GridShap.Wall);
                Console.SetCursorPosition(i, WIDTH - 2);
                Console.Write((char)E_GridShap.Wall);
            }
            // 竖墙
            for (int i = 0; i < WIDTH - 1; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write((char)E_GridShap.Wall);
                Console.SetCursorPosition(LENGTH - 2, i);
                Console.Write((char)E_GridShap.Wall);
            }
        }
    }

    // 蛇
    class Snake : IDisplay, IMove
    {
        public Grid[] body = new Grid[(LENGTH / 2 - 2) * (WIDTH - 3)];
        public int length = 0;

        // 构造函数
        public Snake()
        {
            body[0] = new Grid(new Vector2(BirthX, BirthY), (ConsoleColor)E_GridColor.Head, (char)E_GridShap.Head);
            ++length;
        }

        // 打印蛇
        public void Display()
        {
            for (int i = 0; i < length; ++i)
            {
                Console.ForegroundColor = body[i].color;
                Console.SetCursorPosition(body[i].vector.x, body[i].vector.y);
                Console.Write(body[i].shap);
            }
        }

        // 擦除蛇
        public void Rub()
        {
            for (int i = 0; i < length; ++i)
            {
                Console.SetCursorPosition(body[i].vector.x, body[i].vector.y);
                Console.Write("  ");
            }
        }

        // 判断蛇死亡
        public void IsDeath()
        {
            // 撞墙死
            if (body[0].vector.x < 2 || body[0].vector.x >= LENGTH - 2 || body[0].vector.y < 1 || body[0].vector.y >= WIDTH - 2)
                (baseSences as Program).GameOpenEnd(E_GameSences.End);
            // 撞自己身体死
            for (int i = 4; i < length; ++i)
            {
                if (body[0].vector == body[i].vector)
                    (baseSences as Program).GameOpenEnd(E_GameSences.End);
            }
        }

        // 判断胜利
        public void IsVictory()
        {
            if (body[body.Length - 1] != null)
            {
                Console.Clear();
                Console.SetCursorPosition(LENGTH / 2, WIDTH / 2);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("恭喜你！！！胜利了！！！");
                Console.ReadKey(true);
                (baseSences as Program).GameOpenEnd(E_GameSences.End);
            }
        }

        // 移动
        public void Move(E_Move move)
        {
            for (int i = length - 1; i > 0; --i)
            {
                body[i].vector = body[i - 1].vector;
            }
            switch (move)
            {
                case E_Move.UP:
                    --body[0].vector.y;
                    break;
                case E_Move.DOWN:
                    ++body[0].vector.y;
                    break;
                case E_Move.LEFT:
                    body[0].vector.x -= 2;
                    break;
                case E_Move.RIGHT:
                    body[0].vector.x += 2;
                    break;
            }
        }

        // 吃食物
        public bool EatFood(Food food)
        {
            if (body[0].vector == food.food.vector)
            {
                Grow();
                return true;
            }
            return false;
        }

        // 长身体
        public void Grow()
        {
            body[length] = new Grid(body[length - 1].vector, (ConsoleColor)E_GridColor.Body, (char)E_GridShap.Body);
            ++length;
        }
    }

    // 食物
    class Food:IDisplay
    {
        public Grid food;

        public Food()
        {
            food = new Grid(new Vector2(0, 0), (ConsoleColor)E_GridColor.Food, (char)E_GridShap.Food);
        }

        // 随机生成食物
        public void FreshFood(Snake snake)
        {
            Random random = new Random();
            while (true)
            {
                food.vector.x = random.Next(1, LENGTH / 2 - 2) * 2;
                food.vector.y = random.Next(1, WIDTH - 2);
                bool isFoodinSnake = false;

                for (int i = 0; i < snake.length; ++i)
                {
                    if (food.vector == snake.body[i].vector)
                    {
                        isFoodinSnake = true;
                        break;
                    }
                }
                if (!isFoodinSnake)
                    break;
            }
        }

        // 复写打印操作
        public void Display()
        {
            Console.ForegroundColor = food.color;
            Console.SetCursorPosition(food.vector.x, food.vector.y);
            Console.Write(food.shap);
        }

    }

    // 游戏物体
    class GameObject : IRefresh
    {
        private Snake snake;
        private Food food;

        public GameObject(Snake snake, Food food)
        {
            this.snake = snake;
            this.food = food;
        }

        public void Refresh()
        {
        }
        public void Refresh(E_Move movement)
        {
            snake.Rub();
            if (snake.EatFood(food))
            {
                food.FreshFood(snake);
                food.Display();
            }
            snake.Move(movement);
            snake.IsDeath();
            snake.IsVictory();
            snake.Display();
        }
    }
}
