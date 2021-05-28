using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookReference.Models
{
    public class BookData
    {
        private readonly IConfiguration _configuration;
        public BookData(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Book> AllBookData()
        {
            var books = new List<Book>();

            var conString = _configuration.GetConnectionString("default");


            using (var conn = new SqlConnection(conString))
            {
                conn.Open();

                var cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "SELECT * FROM Books";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var title = reader["Title"].ToString();
                    var id = Convert.ToInt32(reader["BookId"]);

                    books.Add(new Book
                    {
                        Title = title,
                        Id = id
                    });
                }
            }

            return books;
        }

        public Book GetSingleBook(int id)
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand();

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@bookId", Value = id, SqlDbType = SqlDbType.Int });
                cmd.CommandText = "SELECT * FROM Books WHERE BookId = @bookId";

                var reader = cmd.ExecuteReader();

                if(reader.Read())
                {
                    var title = reader["Title"].ToString();
                    var bookId = Convert.ToInt32(reader["BookId"]);

                    return new Book
                    {
                        Title = title,
                        Id = id
                    };
                }
            }
            return null;
        }

        public void CreateNewBook(Book book)
        {
            var connString = _configuration.GetConnectionString("default");
            using (var conn = new SqlConnection(connString))
            {
                conn.Open();

                var cmd = new SqlCommand();

                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Books (Title) VALUES (@title)";
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@title", Value = book.Title, SqlDbType = SqlDbType.NVarChar });
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
