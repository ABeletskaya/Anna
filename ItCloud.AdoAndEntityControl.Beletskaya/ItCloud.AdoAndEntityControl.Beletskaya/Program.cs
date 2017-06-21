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
        static void Main(string[] args)
        {
            using (var context = new LibraryContext())
            {
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
        }

        static void ActionWithBook(Book book, short indexAction)
        {
            using (var context = new LibraryContext())
            {
                if (indexAction == 0) // Add book
                {
                    context.Books.Add(new Book
                    {
                        Name = book.Name,
                        Author = book.Author,
                        Publisher = book.Publisher,
                        Year = book.Year
                    });
                }
                else if (indexAction == 1) // Remove book
                {
                    var removeBook = context.Books.Where(b => b.Id == book.Id).FirstOrDefault();
                    context.Books.Remove(removeBook);
                }

                else if (indexAction ==2) // Update book
                {
                    var updateBook = context.Books.Where(b => b.Id == book.Id).FirstOrDefault();
                   
                    Console.WriteLine("Enter book name:");
                    string Name = Console.ReadLine();
                    Console.WriteLine("Enter book author:");
                    string Author = Console.ReadLine();
                    Console.WriteLine("Enter book publisher:");
                    string Publisher = Console.ReadLine();
                    Console.WriteLine("Enter book year:");
                    int Year = int.Parse(Console.ReadLine());

                    updateBook.Name = Name;
                    updateBook.Author = Author;
                    updateBook.Publisher = Publisher;
                    updateBook.Year = Year;
                }

                context.SaveChanges();
            }
        }

        static void ActionWithUser(short indexAction)
        {
            using (var context = new LibraryContext())
            {
                if(indexAction ==0)
                {
                    Console.WriteLine("Enter new user name:");
                    string Name = Console.ReadLine();
                    
                    Console.WriteLine("Enter new user old:");
                    int Age = int.Parse(Console.ReadLine());

                    System.Data.SqlClient.SqlParameter paramName = new System.Data.SqlClient.SqlParameter("@name", Name);
                    System.Data.SqlClient.SqlParameter paramAge = new System.Data.SqlClient.SqlParameter("@age", Age);

                    var numberOfRowInserted = context.Database.SqlQuery<LibraryUser>("INSERT INTO LibraryUsers(Name, Age)"+
"VALUES(@name, @age);", paramName, paramAge);
                }
            }
        }
    
        
    }
}
