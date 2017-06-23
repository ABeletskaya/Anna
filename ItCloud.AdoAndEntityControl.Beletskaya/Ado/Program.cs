using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Ado
{
    class Program
    {
        static void Main()
        {
            LibraryLogic library = new LibraryLogic();

            ShowMenuList();

            try
            {
                short action = short.Parse(Console.ReadLine());

                using (SqlConnection connecting = new SqlConnection(@"data source=WIN-0KE04US6F45\SQLEXPRESS;initial catalog=Library;integrated security=True;"))
                {
                    library.Connection = connecting;
                    connecting.Open();
                    if (action == 0 || action == 1 || action == 2)
                        library.ActionWithBook(action);
                    else if (action == 10 || action == 20)
                        library.ActionWithUsers(action);
                    else if (action == 1111 || action == 2222)
                        library.LibraryAction(action);
                    else if (action == 100 || action == 101 || action == 102)
                        library.LibraryQuery(action);
                    else
                        Main();
                }

                Console.WriteLine("___________________________________________________________\nDo you want to continue?Y/N");
                string answer = Console.ReadLine();
                if (answer == "Y" || answer == "y")
                    Main();
            }

            catch
            {
                Main();
            }
        }

        static void ShowMenuList()
        {
            Console.WriteLine(@"With Ado.Net

Press 
0 - To add the new book
1 - To update the book
2 - To Remove the book

10 - To add the new user
20 - To remove the user

100 - To get list of books by the user
101 - To get number of author's books
102 - To find information by the name of the book

1111 - To take the book
2222 - To return the book
");
        }
    }
}
