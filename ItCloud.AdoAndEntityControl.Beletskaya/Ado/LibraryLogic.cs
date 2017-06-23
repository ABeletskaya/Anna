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
        public SqlConnection Connection { get; set; }

        private string BookName;
        private string BookAuthor;
        private string BookPublisher;
        private int BookYear;
        private int Id;
        private string UserName;
        private int UserAge;

        private List<Book> Books = new List<Book>();

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
            Console.WriteLine("To add the book, enter it name, author, publisher, year");
            EnterBookInfo();
            try
            {
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $@"INSERT INTO Books (Name, Author, Publisher,Year) VALUES(@name, @author, @publisher, @year);";

                    System.Data.SqlClient.SqlParameter name = new System.Data.SqlClient.SqlParameter("@name", BookName);
                    command.Parameters.Add(name);
                    System.Data.SqlClient.SqlParameter author = new System.Data.SqlClient.SqlParameter("@author", BookAuthor);
                    command.Parameters.Add(author);
                    System.Data.SqlClient.SqlParameter publisher = new System.Data.SqlClient.SqlParameter("@publisher", BookPublisher);
                    command.Parameters.Add(publisher);
                    System.Data.SqlClient.SqlParameter year = new System.Data.SqlClient.SqlParameter("@year", BookYear);
                    command.Parameters.Add(year);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    AddBook method   " + ex.ToString());
            }
        }

        private void UpdateBook()
        {
            try
            {
                Console.WriteLine("To update the book, enter it id: ");
                Id = int.Parse(Console.ReadLine());

                EnterBookInfo();

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $@"UPDATE Books SET Name = '{BookName}', Author =  '{BookAuthor}', Publisher = '{BookPublisher}', Year = '{BookYear}'  WHERE Id = @id; ";
                    System.Data.SqlClient.SqlParameter id = new System.Data.SqlClient.SqlParameter("@id", Id);
                    command.Parameters.Add(id);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    UpdateBook method   " + ex.ToString());
            }

        }

        private void RemoveBook()
        {
            try
            {
                Console.WriteLine("To remove the book enter it id:");
                Id = int.Parse(Console.ReadLine());

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $@"DELETE FROM Books WHERE Id = @id;";
                    System.Data.SqlClient.SqlParameter id = new System.Data.SqlClient.SqlParameter("@id", Id);
                    command.Parameters.Add(id);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    RemoveBook method   " + ex.ToString());
            }
        }

        private void AddUser()
        {
            Console.WriteLine("To add the user, enter his name, age");

            Console.WriteLine("Enter Book user name:");
            UserName = Console.ReadLine();
            Console.WriteLine("Enter Book user age:");
            UserAge = int.Parse(Console.ReadLine());
            try
            {
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $@"INSERT INTO LibraryUsers(Name, Age)	VALUES(@name, @age);";
                    System.Data.SqlClient.SqlParameter name = new System.Data.SqlClient.SqlParameter("@name", UserName);
                    command.Parameters.Add(name);
                    System.Data.SqlClient.SqlParameter age = new System.Data.SqlClient.SqlParameter("@age", UserAge);
                    command.Parameters.Add(age);

                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    AddUser method   " + ex.ToString());
            }
        }

        private void RemoveUser()
        {
            try
            {
                Console.WriteLine("To remove the book enter it id:");
                Id = int.Parse(Console.ReadLine());

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM LibraryUsers WHERE Id = {Id};";
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    RemoveUser method   " + ex.ToString());
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

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Books SET UserId = @id WHERE Name = @name;";
                    System.Data.SqlClient.SqlParameter name = new System.Data.SqlClient.SqlParameter("@name", BookName);
                    command.Parameters.Add(name);
                    System.Data.SqlClient.SqlParameter id = new System.Data.SqlClient.SqlParameter("@id", Id);
                    command.Parameters.Add(id);
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    TakeBook method   " + ex.ToString());
            }
        }

        private void ReturnBook()
        {
            try
            {
                Console.WriteLine("To return the book enter it name:");
                BookName = Console.ReadLine();

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $"UPDATE Books SET UserId = null WHERE Name = @name;";
                    System.Data.SqlClient.SqlParameter name = new System.Data.SqlClient.SqlParameter("@name", BookName);
                    command.Parameters.Add(name);
                    command.ExecuteNonQuery();
                }
                Console.WriteLine("The command completed successfully");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    ReturnBook method   " + ex.ToString());
            }
        }

        private void QueryUsersAllBook()
        {
            try
            {
                Console.WriteLine("Enter user's id");
                Id = int.Parse(Console.ReadLine());

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM Books WHERE UserId = {Id};";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Books.Add(new Book
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
                if (Books.Count > 0)
                {
                    foreach (var book in Books)
                    {
                        Console.WriteLine(book);
                    }
                    Books.Clear();
                }
                else
                    Console.WriteLine("The user with this Id does not have a book");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    QueryUsersAllBook method   " + ex.ToString());
            }
        }

        private void QueryCountBookAuthor()
        {
            try
            {
                Console.WriteLine("Enter author");
                BookAuthor = Console.ReadLine();
                int countBook;

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $"SELECT COUNT(1) FROM Books WHERE Author = '{BookAuthor}'; ";
                    countBook = (int)command.ExecuteScalar();
                }
                Console.WriteLine($"countBook of {BookAuthor} is {countBook}");
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception ADO.NET log.txt   ", DateTime.Now.ToString() + "    QueryCountBookAuthor   " + ex.ToString());
            }
        }

        private void QueryBookInfo()
        {
            try
            {
                Console.WriteLine("Enter the book name");
                BookName = Console.ReadLine();

                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM  Books WHERE Name = '{BookName}';";
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{(int)reader["Id"]} {(string)reader["Name"]} {(string)reader["Author"]}"
                                + $"{(string)reader["Publisher"]} {(int)reader["Year"]} {(reader["UserId"] != DBNull.Value ? (int)reader["UserId"] : 0) }");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                File.WriteAllText(@"Exception log.txt", DateTime.Now.ToString() + "QueryBookInfo  " + ex.ToString());
            }
        }
    }
}
