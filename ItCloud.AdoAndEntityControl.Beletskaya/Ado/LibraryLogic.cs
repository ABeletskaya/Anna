using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ado
{
    class LibraryLogic
    {
        private List<Book> _books = new List<Book>();
        private string _connectionString;
        public LibraryLogic(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool AddBook(Book book)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $@"INSERT INTO Books (Name, Author, Publisher,Year) VALUES(@name, @author, @publisher, @year);";

                        SqlParameter name = new SqlParameter("@name", book.Name);
                        command.Parameters.Add(name);
                        SqlParameter author = new SqlParameter("@author", book.Author);
                        command.Parameters.Add(author);
                        SqlParameter publisher = new SqlParameter("@publisher", book.Publisher);
                        command.Parameters.Add(publisher);
                        SqlParameter year = new SqlParameter("@year", book.Year);
                        command.Parameters.Add(year);

                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    AddBook method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public bool UpdateBook(Book book, int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $@"UPDATE Books SET Name = '{book.Name}', Author =  '{book.Author}',
                                                Publisher = '{book.Publisher}', Year = '{book.Year}'  WHERE Id = @id; ";
                        SqlParameter id = new SqlParameter("@id", Id);
                        command.Parameters.Add(id);
                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    UpdateBook method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public bool RemoveBook(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $@"DELETE FROM Books WHERE Id = @id;";
                        SqlParameter id = new SqlParameter("@id", Id);
                        command.Parameters.Add(id);
                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    RemoveBook method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public bool AddUser(LibraryUser user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $@"INSERT INTO LibraryUsers(Name, Age)	VALUES(@name, @age);";

                        SqlParameter name = new SqlParameter("@name", user.Name);
                        command.Parameters.Add(name);
                        SqlParameter age = new SqlParameter("@age", user.Age);
                        command.Parameters.Add(age);

                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    AddUser method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public bool RemoveUser(int Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"DELETE FROM LibraryUsers WHERE Id = {Id};";
                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    RemoveUser method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public bool TakeBook(int Id, string bookName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE Books SET UserId = @id WHERE Name = @name;";
                        SqlParameter name = new SqlParameter("@name", bookName);
                        command.Parameters.Add(name);
                        SqlParameter id = new SqlParameter("@id", Id);
                        command.Parameters.Add(id);
                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    TakeBook method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public bool ReturnBook(string bookName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE Books SET UserId = null WHERE Name = @name;";
                        SqlParameter name = new SqlParameter("@name", bookName);
                        command.Parameters.Add(name);
                        if (command.ExecuteNonQuery() == 0)
                            return false;
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    ReturnBook method   " + ex.ToString() + "\n\n");
                return false;
            }
            return true;
        }

        public List<Book> QueryUserAllBook(int userId, out bool isSuccess)
        {
            _books.Clear();
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT * FROM Books WHERE UserId = {userId};";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                _books.Add(new Book
                                {
                                    Id = (int)reader["Id"],
                                    Name = (string)reader["Name"],
                                    Author = (string)reader["Author"],
                                    Publisher = (string)reader["Publisher"],
                                    Year = (int)reader["Year"],
                                    UserId = reader["UserId"] != DBNull.Value ? (int)reader["UserId"] : 0
                                });
                            }
                        }
                    }
                }
                isSuccess = (_books.Count == 0) ? false : true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    QueryUsersAllBook method   " + ex.ToString() + "\n\n");
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
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT COUNT(1) FROM Books WHERE Author = '{author}'; ";
                        countBook = (int)command.ExecuteScalar();
                    }
                    if (countBook == 0)
                        isSuccess = false;
                    else
                        isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception ADO.NET log.txt", DateTime.Now.ToString() + "    QueryCountBookAuthor   " + ex.ToString() + "\n\n");
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
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"SELECT * FROM  Books WHERE Name = '{bookName}';";
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                book.Id = (int)reader["Id"];
                                book.Name = (string)reader["Name"];
                                book.Author = (string)reader["Author"];
                                book.Publisher = (string)reader["Publisher"];
                                book.Year = (int)reader["Year"];
                                book.UserId = reader["UserId"] != DBNull.Value ? (int)reader["UserId"] : 0;
                            }
                        }
                        if (book.Id == 0)
                            isSuccess = false;
                        else
                            isSuccess = true;
                    }
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "QueryBookInfo  " + ex.ToString() + "\n\n");
            }
            return book;
        }
    }
}
