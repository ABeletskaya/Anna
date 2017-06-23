using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ItCloud.AdoAndEntityControl.Beletskaya
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
                using (var context = new LibraryContext())
                {
                    library.Context = context;

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

                    // Создала базу:
                    //context.Users.Add(new LibraryUser
                    //{
                    //    Name = "User1",
                    //    Age = 10
                    //});

                    //context.Users.Add(new LibraryUser
                    //{
                    //    Name = "User2",
                    //    Age = 12
                    //});

                    //context.Users.Add(new LibraryUser
                    //{
                    //    Name = "User3",
                    //    Age = 23
                    //});

                    //context.Books.Add(new Book
                    //{
                    //    Name = "Book1",
                    //    Author = "Author1",
                    //    Publisher = "C17-01",
                    //    Year = 1999
                    //});

                    //context.Books.Add(new Book
                    //{
                    //    Name = "Book2",
                    //    Author = "Author1",
                    //    Publisher = "C17-01",
                    //    Year = 1998
                    //});

                    //context.Books.Add(new Book
                    //{
                    //    Name = "Book3",
                    //    Author = "Author1",
                    //    Publisher = "C17-01",
                    //    Year = 1989
                    //});

                    //context.Books.Add(new Book
                    //{
                    //    Name = "Book4",
                    //    Author = "Author2",
                    //    Publisher = "world",
                    //    Year = 2017
                    //});

                    //context.Books.Add(new Book
                    //{
                    //    Name = "Book5",
                    //    Author = "Author3",
                    //    Publisher = "world",
                    //    Year = 2017
                    //});

                    //context.Books.Add(new Book
                    //{
                    //    Name = "Book6",
                    //    Author = "Author3",
                    //    Publisher = "world",
                    //    Year = 2015
                    //});

                    //context.SaveChanges();
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
            Console.WriteLine(@"With Entity Framework

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
