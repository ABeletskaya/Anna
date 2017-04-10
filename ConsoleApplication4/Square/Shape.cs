using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Shape // по умолчанию internal
    {
        int _a;
        int _b;
        Color _color;

        public int CalculateSquare(int a, int b) 
        {
            return a * b;
        }


    }
}
