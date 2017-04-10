using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
   public class ShapesConteiner
    {
        private List<Shape> _shapes;

        public int Count => _shapes.Count;

        public ShapesConteiner() 
        {
            _shapes = new List<Shape>();
        }


        // Индексатор создаем
        public Shape this[int index] 
        {
            get 
            {
                return _shapes.ElementAt(index);
            }
            set 
            {
                var element = _shapes.ElementAt(index);
                _shapes.Remove(element);
                _shapes.Add(value);
            }
        }

        public void Add(Shape shape)
        {
            _shapes.Add(shape);
        }
    }
}
