using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
   public class Triangle: Shape
    {
        public Triangle() : base(9,9) // указываем базовый конструктор
        {
            Console.WriteLine(nameof(Triangle));
        }
        public void print()
       {
            DrawA();
        }
        protected override void DrawA()
        {
            Console.WriteLine(_b);
        }

        public new void Print() {

        }

        
    }
}
