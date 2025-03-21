using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    struct Grid
    {
        public Vector2 vector;
        public char shap;
        public ConsoleColor color;
        public Grid(Vector2 vector, char shap, ConsoleColor color)
        {
            this.vector = vector;
            this.shap = shap;
            this.color = color;
        }
    }
}
