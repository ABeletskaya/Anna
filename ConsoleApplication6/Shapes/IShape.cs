using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public interface IShape
    {
        int X { get;  } // в интерфейсе чаще только get
        int Y { get; set; }
        void Print();
    }
}
