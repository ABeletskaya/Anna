using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.IO;

namespace ItCloud.AdoAndEntityControl.Beletskaya
{
    class Program
    {
        static void Main()
        {
            LibraryLogic Library = new LibraryLogic(@"data source=WIN-0KE04US6F45\SQLEXPRESS;initial catalog=Library;integrated security=True;");
            LibraryUser User = new LibraryUser();
            Book Book = new Book();
            List<Book> Books;

            int Id;
            string BookName;
            bool isSuccess = true;

            ShowMenuList();

            try
            {
                short action = short.Parse(Console.ReadLine());



                switch (action)
                {
                    case 0: // Add book
                        {
                            Console.WriteLine("To add the book, enter it name, author, publisher, year");
                            if (CorrectEnterBookInfo(Book))
                                isSuccess = Library.AddBook(Book);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                    case 1: // Update book
                        {
                            Console.WriteLine("To update the book, enter it id: ");
                            Id = int.Parse(Console.ReadLine());

                            if (CorrectEnterBookInfo(Book))
                                isSuccess = Library.UpdateBook(Book, Id);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                    case 2: // Remove book
                        {
                            Console.WriteLine("To remove the book enter it id:");
                            Id = int.Parse(Console.ReadLine());
                            isSuccess = Library.RemoveBook(Id);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                    case 10: //Add User
                        {
                            Console.WriteLine("To add the user, enter his name, age");
                            Console.WriteLine("Enter User name:");
                            User.Name = Console.ReadLine();
                            Console.WriteLine("Enter User age:");
                            User.Age = int.Parse(Console.ReadLine());
                            isSuccess = Library.AddUser(User);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                    case 20: // Remove User
                        {
                            Console.WriteLine("To remove User enter his id:");
                            Id = int.Parse(Console.ReadLine());
                            isSuccess = Library.RemoveUser(Id);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                    case 100: //QueryUserAllBook(); 
                        {
                            Console.WriteLine("Enter user's id");
                            Id = int.Parse(Console.ReadLine());

                            Books = new List<Book>(Library.QueryUserAllBook(Id, out isSuccess));
                            if (isSuccess)
                            {
                                foreach (var book in Books)
                                {
                                    Console.WriteLine(book);
                                }
                            }
                            else
                                Console.WriteLine("Information not found");
                            break;
                        }
                    case 101: //QueryCountBookAuthor();
                        {
                            Console.WriteLine("Enter author");
                            string author = Console.ReadLine();
                            int countBook = Library.QueryCountBookAuthor(author, out isSuccess);

                            if (isSuccess)
                                Console.WriteLine($"countBook of {author} is {countBook}");
                            else
                                Console.WriteLine("Information not found");
                            break;
                        }
                    case 102: // QueryBookInfo();
                        {
                            Console.WriteLine("Enter the book name");
                            BookName = Console.ReadLine();
                            Book = Library.QueryBookInfo(BookName, out isSuccess);

                            if (isSuccess)
                                Console.WriteLine(Book.ToString());
                            else
                                Console.WriteLine("Information not found");
                            break;
                        }
                    case 1111: //Take Book
                        {
                            Console.WriteLine("Enter user's id");
                            Id = int.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the book name:");
                            BookName = Console.ReadLine();
                            isSuccess = Library.TakeBook(Id, BookName);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                    case 2222: //Return Book
                        {
                            Console.WriteLine("To return the book enter it name:");
                            BookName = Console.ReadLine();
                            isSuccess = Library.ReturnBook(BookName);
                            ShowResultMethod(isSuccess);
                            break;
                        }
                }

                
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
            catch (Exception ex)
            {
                Console.WriteLine("Invalid user input");
                Console.WriteLine(@" Main \n" + ex.ToString());
            }

            ToContinueWork();
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

        static bool CorrectEnterBookInfo(Book book) // подумать как сделать флаг, если неудачный пользовательский ввод, чтобы и на добавление книги не шло
        {
            bool isSuccess = true;
            try
            {
                Console.WriteLine("Enter book name:");
                book.Name = Console.ReadLine();
                Console.WriteLine("Enter book author:");
                book.Author = Console.ReadLine();
                Console.WriteLine("Enter book publisher:");
                book.Publisher = Console.ReadLine();
                Console.WriteLine("Enter book year:");
                book.Year = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                isSuccess = false;
                Console.WriteLine("Invalid user input");
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   CorrectEnterBookInfo method   " + ex.ToString() + "\n\n");
            }
            return isSuccess;
        }

        static void ToContinueWork()
        {
            Console.WriteLine("___________________________________________________________\nDo you want to continue? (Y/N)");
            string answer = Console.ReadLine();
            if (answer == "Y" || answer == "y")
                Main();
        }

        static void ShowResultMethod(bool isSuccess)
        {
            if (isSuccess)
                Console.WriteLine("The action completed successfully");
            else
                Console.WriteLine("The action completed unsuccessfully");
        }
    }
}
