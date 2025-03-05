using Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal;
using System.Data;
using System.Data.SqlClient;
using System.Formats.Tar;

namespace PSTDotNetTrainingBatch5.ConsoleApp4
{
    public class AdoDotNetExample : IAdoDotNetExample
    {
        //I decided to use the connection string from the appsettings.cs file and declare in program.cs file.
        //private readonly string connectionString = AppSettings.ConnectionString;
        SqlConnection _connection;


        public AdoDotNetExample(SqlConnection connection)
        {
            // This way also ok to use it.
            //_connection = new SqlConnection(_connectionString);
            _connection = connection;
        }

        public async void Read()
        {
            try
            {
                _connection.Open();

                string query = @"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";

                SqlCommand cmd = new SqlCommand(query, _connection);
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                reader.Read();
                while (reader.Read())
                {
                    Console.WriteLine(reader["BlogId"]);
                    Console.WriteLine(reader["BlogTitle"]);
                    Console.WriteLine(reader["BlogAuthor"]);
                    Console.WriteLine(reader["BlogContent"]);
                }

                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async void Create()
        {
            try
            {
                _connection.Open();

                Console.WriteLine("Please Blog Title: ");

                string title = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                Console.WriteLine("Please Blog Author: ");
                string? author = Console.ReadLine();
                if (string.IsNullOrEmpty(author))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                Console.WriteLine("Please Blog Content: ");
                string? content = Console.ReadLine();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                string query = $@"INSERT INTO [dbo].[Tbl_BLog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,'0')";

                SqlCommand cmd = new SqlCommand(query, _connection);

                cmd.Parameters.AddWithValue("@BlogTitle", title);
                cmd.Parameters.AddWithValue("@BlogAuthor", author);
                cmd.Parameters.AddWithValue("@BlogContent", content);

                int result = await cmd.ExecuteNonQueryAsync();

                Console.WriteLine(result == 1 ? "Insert Successful." : "Inesert Failed.");

                _connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void Edit()
        {
            try
            {
                Console.Write("Blog Id: ");
                string? id = Console.ReadLine();

                if (string.IsNullOrEmpty(id)) { return; }

                _connection.Open();

                string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_BLog] where BlogId = @BlogId and [DeleteFlag] = 0";

                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@BlogId", id);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                adapter.Fill(dt);

                _connection.Close(); // data ကိုယူတဲ့ ကိစ္စပြီးတာနဲ _connection ပိတ်တာကကောင်းပါတယ်။

                if (dt.Rows.Count is 0) // row ရဲ့ count က ၀ နဲညီနေရင် data မပါလာလို့ပါ။
                {
                    Console.WriteLine("No data found...");
                    return;
                }

                DataRow dr = dt.Rows[0];
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async void Update()
        {
            try
            {
                _connection.Open();

                Console.WriteLine("Please Blog ID: ");
                string? id = Console.ReadLine();
                if (string.IsNullOrEmpty(id))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                Console.WriteLine("Please Blog Title: ");
                string? title = Console.ReadLine();
                if (string.IsNullOrEmpty(title))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                Console.WriteLine("Please Blog Author: ");
                string? author = Console.ReadLine();
                if (string.IsNullOrEmpty(author))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                Console.WriteLine("Please Blog Content: ");
                string? content = Console.ReadLine();
                if (string.IsNullOrEmpty(content))
                {
                    Console.WriteLine("We didn`t allow the null value.");
                    return;
                }

                string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE [BlogId] = @BlogId and [DeleteFlag] != 1";

                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@BlogId", id);
                cmd.Parameters.AddWithValue("@BlogTitle", title);
                cmd.Parameters.AddWithValue("@BlogAuthor", author);
                cmd.Parameters.AddWithValue("@BlogContent", content);

                int result = await cmd.ExecuteNonQueryAsync();

                _connection.Close();

                Console.WriteLine(result == 1 ? "Update Successful." : "Update Failed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public async void Delete()
        {
            try
            {
                Console.Write("Blog Id: ");
                string? id = Console.ReadLine();

                if (string.IsNullOrEmpty(id)) { return; }

                _connection.Open();

                string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [DeleteFlag] = 1
 WHERE [BlogId] = @BlogId and [DeleteFlag] = 0";

                SqlCommand cmd = new SqlCommand(query, _connection);
                cmd.Parameters.AddWithValue("@BlogId", id);

                int result = await cmd.ExecuteNonQueryAsync();

                _connection.Close();

                Console.WriteLine(result == 1 ? "Delete Successful." : "Delete Failed.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
