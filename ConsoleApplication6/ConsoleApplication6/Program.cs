using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shapes;


namespace ConsoleApplication6
{
    class Program:Shape
    {
        private void YU() 
        {
            
        }
        static void Main(string[] args)
        {

            //Shape shape = new Shape();
            //shape.Draw();

            //Triangle triangle = new Triangle();
            //triangle.Draw();

            //Shape shape1 = new Shape(5, 8);
            //shape1.Draw();

           // var triangle = new Triangle();
            // Вызов свойств
            // triangle.A = 9;
           // var a = triangle.A;
            // При наследовании ВСЕГДА БУДЕТ ВЫЗЫВАТЬСЯ БАЗОВЫЙ КОНСТРУКТОР БАЗОВОГО КЛАССА
          //  Console.ReadKey();


            Shape shape = new Shape();
            Console.WriteLine(shape);

            var container = new ShapesConteiner();

            Console.WriteLine(container.Count);
            // Дописать свойства объекта
            //var shape1 = new Shape
            //{
            //    A = 9,
            //    B = 63,
            //    Color = ConsoleColor.Red
            //};

        }
    }
}
