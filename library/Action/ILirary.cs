using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Action
{
    public interface ILirary
    {
     int PrintBooksCount();
        void RemoveBook();
        void AddBook();
    }
}
