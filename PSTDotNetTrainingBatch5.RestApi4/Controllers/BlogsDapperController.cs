using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.RestApi4.DataModels;
using PSTDotNetTrainingBatch5.RestApi4.ViewModels;
using System.Data;
using System.Threading.Tasks;

namespace PSTDotNetTrainingBatch5.RestApi4.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogsDapperController : ControllerBase
{
    private readonly IDbConnection _db;

    public BlogsDapperController(IDbConnection db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IActionResult> GetBlogs()
    {
        string query = @"SELECT * FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";
        var lst = await _db.QueryAsync<BlogDataModel>(query);

        return Ok(lst);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBlog(int id)
    {
        string query = $@"SELECT [BlogId] ,[BlogTitle] ,[BlogAuthor] ,[BlogContent] ,[DeleteFlag]
            FROM [dbo].[Tbl_BLog] where BlogId = @BlogId and [DeleteFlag] = 0";

        var lst = await _db.QueryFirstOrDefaultAsync<BlogDataModel>(query, new BlogDataModel
        {
            BlogId = id
        });

        if (lst is null) { return Ok("No data found."); }

        return Ok(lst);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBlog(BlogViewModel blog)
    {
        string query = $@"INSERT INTO [dbo].[Tbl_BLog]
                  ([BlogTitle]
                  ,[BlogAuthor]
                  ,[BlogContent]
                  ,[DeleteFlag])
            VALUES
                  (@Title
                  ,@Author
                  ,@Content
                  ,'0')";

        int result = await _db.ExecuteAsync(query, blog);
        return Ok(result == 1 ? "Saving Successful." : "Saving Failed.");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBlog(int id, BlogViewModel blog)
    {
        try
        {
            string query = @"UPDATE [dbo].[Tbl_BLog]
                       SET [BlogTitle] = @Title
                          ,[BlogAuthor] = @Author
                          ,[BlogContent] = @Content
                          ,[DeleteFlag] = 0
                     WHERE [BlogId] = @Id and [DeleteFlag] != 1";

            if (string.IsNullOrEmpty(blog.Title))
            {
                throw new Exception("Title is required.");
            }
            if (string.IsNullOrEmpty(blog.Author))
            {
                throw new Exception("Author is required.");
            }
            if (string.IsNullOrEmpty(blog.Content))
            {
                throw new Exception("Content is required.");
            }

            blog.Id = id;

            int result = await _db.ExecuteAsync(query, blog);

            return Ok(result == 1 ? "Updating Successful." : "Updating Failed.");
        }
        catch (Exception ex)
        {
            return Ok($"An error occurred: {ex.Message}");
            throw;
        }
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchBlogs(int id, BlogViewModel blog)
    {
        try
        {
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " [BlogTitle] = @Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " [BlogAuthor] = @Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " [BlogContent] = @Content, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid Parameters!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[Tbl_BLog] SET {conditions} WHERE [BlogId] = @Id and [DeleteFlag] != 1";

            blog.Id = id;

            int result = await _db.ExecuteAsync(query, blog);

            return Ok(result > 0 ? "Patching Successful." : "Patching Failed!");
        }
        catch (Exception ex)
        {
            return Ok($"An error occurred: {ex.Message}");
            throw;
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogs(int id)
    {
        string query = @"UPDATE [dbo].[Tbl_BLog]
            SET [DeleteFlag] = 1 WHERE [BlogId] = @Id and [DeleteFlag] = 0";

        int result = await _db.ExecuteAsync(query, new BlogViewModel
        {
            Id = id
        });
        return Ok(result == 1 ? "Deleting Successful." : "Deleting Failed.");
    }
}
