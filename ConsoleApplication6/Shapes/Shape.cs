using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
   public class Shape : IShape
    {
        internal protected int H; // доступ к нему имеют все из этой сборки и...
        private int _a;
        protected int _b;
        private Color _color;

        public int X=3;
        public int Y=2;


        //// Свойства поля класса:
        //public int A {
        //    get { return _a; }
        //    set { if(value>0)
        //            _a = value; } // свойства set чаще не бывает
        //}

        // Автосвойство:
        //  public int A { get; set; }

            public int A { get; private set; }

        // конструктор по умолчанию
        public Shape():this(4,4)
        {
            Console.WriteLine(nameof(Shape));

        }

        public Shape(int a, int b)
            //:this() // Для данного конструктора вызвался сначала пустой конструктор, а затем тот , что ниже
        {
            this._a = a; // this используется если бы _а было названо а, тогда бы передали значение через this.a=a;
            _b = b;
            Draw();
        }

        // деструктор, не тождественен дестуктору из С++, служит для чистки неуправляемых ресурсов
        ~Shape() 
        {

        }

        public int Square() 
        {
            return _a * _b;
        }

        public void Draw() 
        {
            Console.WriteLine($"A={_a} B={_b}");
        }
        
        //перегрузка по возвращаемым значениям  - не реализуется
        // только по типу или количеству параметров

        public void Draw(int t)
        {
            Console.WriteLine($"A={_a} B={_b}");
        }

        public void Draw(string t) {
            Console.WriteLine($"A={_a} B={_b}");
        }

        public static void DrawPerfect()
        {
            Console.WriteLine("Cool!");
        }

        private void DrawB() {
            Console.WriteLine(_b);
        }

        protected virtual void DrawA() {
            Console.WriteLine(_a);
        }

        public override int GetHashCode() 
        {
            return _a.GetHashCode()*347^_b.GetHashCode()*347;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            var shape = obj as Shape;

            if (shape == null)
                return false;
            // Тут еще 3я ветка
            return true;
        }

        public void SetShapeSize(ref int a, out int b) {
            a = 9;
            b = 63;
        }

        public void Print()
        {
            Console.WriteLine($"X:{X} Y:{Y} Color");
        }
    }
}
