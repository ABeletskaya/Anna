using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCloud.AdoAndEntityControl.Beletskaya
{
    class LibraryLogic
    {
        public LibraryContext Context { get; set; }

        private string BookName;
        private string BookAuthor;
        private string BookPublisher;
        private int BookYear;
        private int Id;
        private string UserName;
        private int UserAge;

        public void ActionWithBook(short indexAction)
        {
            if (indexAction == 0) // Add book
            {
                AddBook();
            }
            else if (indexAction == 1) // Update book
            {
                UpdateBook();
            }
            else if (indexAction == 2) // Remove book
            {
                RemoveBook();
            }
        }

        public void ActionWithUsers(short indexAction)
        {
            if (indexAction == 10)
                AddUser();
            else if (indexAction == 20)
                RemoveUser();
        }

        public void LibraryAction(short indexAction)
        {
            if (indexAction == 1111)
                TakeBook();
            else if (indexAction == 2222)
                ReturnBook();
        }

        public void LibraryQuery(short indexAction)
        {
            if (indexAction == 100)
                QueryUsersAllBook();
            if (indexAction == 101)
                QueryCountBookAuthor();
            if (indexAction == 102)
                QueryBookInfo();
        }

        private void EnterBookInfo()
        {
            try
            {
                Console.WriteLine("Enter book name:");
                BookName = Console.ReadLine();
                Console.WriteLine("Enter book author:");
                BookAuthor = Console.ReadLine();
                Console.WriteLine("Enter book publisher:");
                BookPublisher = Console.ReadLine();
                Console.WriteLine("Enter book year:");
                BookYear = int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void AddBook()
        {
            try
            {
                Console.WriteLine("To add the book, enter it name, author, publisher, year");
                EnterBookInfo();

                Context.Books.Add(new Book
                {
                    Name = BookName,
                    Author = BookAuthor,
                    Publisher = BookPublisher,
                    Year = BookYear
                });

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "AddBook method   " + ex.ToString());
            }
        }

        private void UpdateBook()
        {
            try
            {
                Console.WriteLine("To update the book, enter it id: ");
                Id = int.Parse(Console.ReadLine());

                EnterBookInfo();

                var updateBook = Context.Books.Where(b => b.Id == Id).FirstOrDefault();

                updateBook.Name = BookName;
                updateBook.Author = BookAuthor;
                updateBook.Publisher = BookPublisher;
                updateBook.Year = BookYear;

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "UpdateBook method   " + ex.ToString());
            }
        }

        private void RemoveBook()
        {
            try
            {
                Console.WriteLine("To remove the book enter it id:");
                Id = int.Parse(Console.ReadLine());

                var removeBook = Context.Books.Where(b => b.Id == Id).FirstOrDefault();
                Context.Books.Remove(removeBook);

                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "RemoveBook method   " + ex.ToString());
            }

        }

        private void AddUser()
        {
            try
            {
                Console.WriteLine("To add the Book user, enter his name, age");

                Console.WriteLine("Enter Book user name:");
                UserName = Console.ReadLine();
                Console.WriteLine("Enter Book user age:");
                UserAge = int.Parse(Console.ReadLine());


                System.Data.SqlClient.SqlParameter paramName = new System.Data.SqlClient.SqlParameter("@name", UserName);
                System.Data.SqlClient.SqlParameter paramAge = new System.Data.SqlClient.SqlParameter("@age", UserAge);

                var numberOfRowInserted = Context.Database.ExecuteSqlCommand("INSERT INTO LibraryUsers(Name, Age)" +
               "VALUES(@name, @age);", paramName, paramAge);
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "AddUser method   " + ex.ToString());
            }
        }

        private void RemoveUser()
        {
            try
            {
                Console.WriteLine("To remove the book enter it id:");
                Id = int.Parse(Console.ReadLine());

                var numberOfRowInserted = Context.Database.ExecuteSqlCommand($"DELETE FROM LibraryUsers WHERE Id = {Id};");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "RemoveUser method   " + ex.ToString());
            }
        }

        private void TakeBook()
        {
            try
            {
                Console.WriteLine("Enter user's id");
                Id = int.Parse(Console.ReadLine());
                Console.WriteLine("Enter the book name:");
                BookName = Console.ReadLine();

                System.Data.SqlClient.SqlParameter paramName = new System.Data.SqlClient.SqlParameter("@name", BookName);
                var numberOfRowInserted = Context.Database.ExecuteSqlCommand($"UPDATE Books SET UserId = {Id} WHERE Name = '{BookName}'; ");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "TakeBook method   " + ex.ToString());
            }
        }

        private void ReturnBook()
        {
            try
            {
                Console.WriteLine("To return the book enter it name:");
                BookName = Console.ReadLine();

                System.Data.SqlClient.SqlParameter paramName = new System.Data.SqlClient.SqlParameter("@name", BookName);
                var numberOfRowInserted = Context.Database.ExecuteSqlCommand($"UPDATE Books SET UserId = null WHERE Name = '{BookName}'; ");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "ReturnBook method   " + ex.ToString());
            }
        }

        private void QueryUsersAllBook()
        {
            try
            {
                Console.WriteLine("Enter user's id");
                Id = int.Parse(Console.ReadLine());

                System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@id", Id);
                var books = Context.Database.SqlQuery<Book>("SELECT * FROM Books WHERE UserId = @id; ", param);
                foreach (var book in books)
                    Console.WriteLine(book);
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "QueryUsersAllBook   " + ex.ToString());
            }
        }

        private void QueryCountBookAuthor()
        {
            try
            {
                Console.WriteLine("Enter author");
                BookAuthor = Console.ReadLine();

                int countBook = Context.Books.Count(b => b.Author == BookAuthor);

                Console.WriteLine($"countBook of {BookAuthor} - {countBook}");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "QueryUsersAllBook   " + ex.ToString());
            }
        }

        private void QueryBookInfo()
        {
            try
            {
                Console.WriteLine("Enter the book name");
                BookName = Console.ReadLine();

                Book bookInfo = Context.Books.First(b => b.Name == BookName);
                Console.WriteLine(bookInfo);
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "QueryBookInfo  " + ex.ToString());
            }
        }
    }
}
