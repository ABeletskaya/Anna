using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ItCloud.AdoAndEntityControl.Beletskaya
{
    class LibraryLogic
    {
        private string _connectionString;
        public LibraryLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddBook(Book book)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        Context.Books.Add(new Book
                        {
                            Name = book.Name,
                            Author = book.Author,
                            Publisher = book.Publisher,
                            Year = book.Year
                        });
                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   AddBook method   " + ex.ToString());
            }
            return isSuccess;
        }

        public bool UpdateBook(Book book, int Id)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        var updateBook = Context.Books.Where(b => b.Id == Id).FirstOrDefault();

                        updateBook.Name = book.Name;
                        updateBook.Author = book.Author;
                        updateBook.Publisher = book.Publisher;
                        updateBook.Year = book.Year;
                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   UpdateBook method   " + ex.ToString());
            }
            return isSuccess;
        }

        public bool RemoveBook(int Id)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        var removeBook = Context.Books.Where(b => b.Id == Id).FirstOrDefault();
                        Context.Books.Remove(removeBook);
                        Context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   RemoveBook method   " + ex.ToString());
            }
            return isSuccess;
        }

        public bool AddUser(LibraryUser user)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        SqlParameter Name = new SqlParameter("@name", user.Name);
                        SqlParameter Age = new SqlParameter("@age", user.Age);

                        var numberOfRowInserted = Context.Database.ExecuteSqlCommand("INSERT INTO LibraryUsers(Name, Age) VALUES(@name, @age);", Name, Age);
                        if (numberOfRowInserted == 0)
                            isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   AddUser method   " + ex.ToString());
            }
            return isSuccess;
        }

        public bool RemoveUser(int Id)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        var numberOfRowInserted = Context.Database.ExecuteSqlCommand($"DELETE FROM LibraryUsers WHERE Id = {Id};");
                        if (numberOfRowInserted == 0)
                            isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   RemoveUser method   " + ex.ToString());
            }
            return isSuccess;
        }

        public bool TakeBook(int Id, string bookName)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        SqlParameter paramName = new SqlParameter("@name", bookName);
                        var numberOfRowInserted = Context.Database.ExecuteSqlCommand($"UPDATE Books SET UserId = {Id} WHERE Name = '{bookName}'; ");
                        if (numberOfRowInserted == 0)
                            isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   TakeBook method   " + ex.ToString());
            }
            return isSuccess;
        }

        public bool ReturnBook(string bookName)
        {
            bool isSuccess = true;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        var numberOfRowInserted = Context.Database.ExecuteSqlCommand($"UPDATE Books SET UserId = null WHERE Name = '{bookName}'; ");
                        if (numberOfRowInserted == 0)
                            isSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   ReturnBook method   " + ex.ToString());
            }
            return isSuccess;
        }

        public List<Book> QueryUserAllBook(int userId, out bool isSuccess)
        {
            List<Book> _books = new List<Book>();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        SqlParameter param = new SqlParameter("@id", userId);
                        _books = Context.Database.SqlQuery<Book>("SELECT * FROM Books WHERE UserId = @id; ", param).ToList();
                    }
                }
                isSuccess = (_books.Count == 0) ? false : true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   QueryUsersAllBook   " + ex.ToString());
            }
            return _books;
        }

        public int QueryCountBookAuthor(string author, out bool isSuccess)
        {
            int countBook = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        countBook = Context.Books.Count(b => b.Author == author);
                    }
                }
                if (countBook == 0)
                    isSuccess = false;
                else
                    isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   QueryUsersAllBook   " + ex.ToString());
            }
            return countBook;
        }

        public Book QueryBookInfo(string bookName, out bool isSuccess)
        {
            Book book = new Book();
            book.Name = bookName;
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var Context = new LibraryContext())
                    {
                        book = Context.Books.First(b => b.Name == bookName);
                    }
                }
                isSuccess = (book.Id == 0) ? false : true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception Entity log.txt", DateTime.Now.ToString() + "   QueryBookInfo  " + ex.ToString());
            }
            return book;
        }
    }
}
