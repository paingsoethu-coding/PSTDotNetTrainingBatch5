using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.RestApi4.ViewModels;
using System.Data;

namespace PSTDotNetTrainingBatch5.RestApi4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsAdoDotNetController : ControllerBase
{
    private readonly string _connectionString;
    private readonly SqlConnection _connection;

    public BlogsAdoDotNetController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DbConnection")!;
        //_connectionString = configuration;
        _connection = new SqlConnection(_connectionString);
    }

    private readonly List<BlogViewModel> lst = new List<BlogViewModel>();

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
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
            var item = new BlogViewModel
            {
                Id = Convert.ToInt32(reader["BlogId"]),
                Title = Convert.ToString(reader["BlogTitle"]),
                Author = Convert.ToString(reader["BlogAuthor"]),
                Content = Convert.ToString(reader["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
            };
            lst.Add(item);

        }

        _connection.Close();

        return Ok(lst);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
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

        _connection.Close();

        if (dt.Rows.Count is 0) { return Ok("No data found..."); }

        DataRow dr = dt.Rows[0];
        lst.Add(new BlogViewModel
        {
            Id = Convert.ToInt32(dr["BlogId"]),
            Title = Convert.ToString(dr["BlogTitle"]),
            Author = Convert.ToString(dr["BlogAuthor"]),
            Content = Convert.ToString(dr["BlogContent"]),
            DeleteFlag = Convert.ToBoolean(dr["DeleteFlag"])
        });

        return Ok(lst);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(BlogViewModel blog)
    {
        _connection.Open();

        if (string.IsNullOrEmpty(blog.Title))
        {
            return Ok("We didn`t allow Title null value.");
        }

        if (string.IsNullOrEmpty(blog.Author))
        {
            return Ok("We didn`t allow Author null value.");
        }

        if (string.IsNullOrEmpty(blog.Content))
        {
            return Ok("We didn`t allow Content null value.");
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

        cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
        cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

        int result = await cmd.ExecuteNonQueryAsync();

        _connection.Close();

        return Ok(result == 1 ? "Insert Successful." : "Inesert Failed.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog(int id, BlogViewModel blog)
    {
        _connection.Open();

        string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE [BlogId] = @BlogId and [DeleteFlag] != 1";

        SqlCommand cmd = new SqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
        cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
        cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

        int result =  await cmd.ExecuteNonQueryAsync();

        _connection.Close();

        return Ok(result == 1 ? "Update Successful." : "Update Failed.");
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchBlogs(int id, BlogViewModel blog)
    {
        string conditions = string.Empty;

        if (!string.IsNullOrEmpty(blog.Title))
        {
            conditions += " [BlogTitle] = @BlogTitle, ";
        }
        if (!string.IsNullOrEmpty(blog.Author))
        {
            conditions += " [BlogAuthor] = @BlogAuthor, ";
        }
        if (!string.IsNullOrEmpty(blog.Content))
        {
            conditions += " [BlogContent] = @BlogContent, ";
        }

        if (conditions.Length == 0)
        {
            return BadRequest("Invalid Parameters!");
        }

        conditions = conditions.Substring(0, conditions.Length - 2);

        _connection.Open();

        string query = $@"UPDATE [dbo].[Tbl_BLog] SET {conditions} WHERE [BlogId] = @BlogId and [DeleteFlag] != 1";

        SqlCommand cmd = new SqlCommand(query, _connection);

        cmd.Parameters.AddWithValue("@BlogId", id);
        if (!string.IsNullOrEmpty(blog.Title))
        {
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
        }
        if (!string.IsNullOrEmpty(blog.Author))
        {
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
        }
        if (!string.IsNullOrEmpty(blog.Content))
        {
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
        }

        int result = await cmd.ExecuteNonQueryAsync();

        _connection.Close();

        return Ok(result > 0 ? "Patching Successful." : "Patching Failed!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogs(int id)
    {
        _connection.Open();

        string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [DeleteFlag] = 1
 WHERE [BlogId] = @BlogId and [DeleteFlag] = 0";

        SqlCommand cmd = new SqlCommand(query, _connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        int result = await cmd.ExecuteNonQueryAsync();

        _connection.Close();

        return Ok(result == 1 ? "Delete Successful." : "Delete Failed.");
    }
}
