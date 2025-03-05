using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.Database.Models;
using PSTDotNetTrainingBatch5.RestApi4.DataModels;
using PSTDotNetTrainingBatch5.RestApi4.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace PSTDotNetTrainingBatch5.RestApi4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsDapperController : ControllerBase
    {
        private readonly string _connectionString = "Data Source= MSI\\SQLEXPRESS2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True;";

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string query = @"SELECT * FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";
                var lst = db.Query(query).ToList();

                return Ok(lst);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = $@"SELECT [BlogId] ,[BlogTitle] ,[BlogAuthor] ,[BlogContent] ,[DeleteFlag]
            FROM [dbo].[Tbl_BLog] where BlogId = @Id and [DeleteFlag] = 0";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var lst = db.Query(query, new BlogViewModel
                {
                    Id = id
                }).FirstOrDefault();

                if (lst is null) { return Ok("No data found."); }

                return Ok(lst);
            }
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogViewModel blog)
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, blog);
                return Ok(result == 1 ? "Saving Successful." : "Saving Failed.");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            string query = @"UPDATE [dbo].[Tbl_BLog]
                       SET [BlogTitle] = @Title
                          ,[BlogAuthor] = @Author
                          ,[BlogContent] = @Content
                          ,[DeleteFlag] = 0
                     WHERE [BlogId] = @Id and [DeleteFlag] != 1";

            try
            {
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

                using (IDbConnection db = new SqlConnection(_connectionString))
                {
                    #region Table ပေးမယ်ဆိုရင် နှစ်ခုလုံးသုံးလို့ရပါတယ်။
                    //int result = db.Execute(query, new BlogViewModel
                    //{
                    //    Id = id,
                    //    Title = blog.BlogTitle,
                    //    Author = blog.BlogAuthor,
                    //    Content = blog.BlogContent
                    //});
                    #endregion

                    blog.Id = id;

                    int result = db.Execute(query, blog);

                    return Ok(result == 1 ? "Updating Successful." : "Updating Failed.");
                }
            }
            catch (Exception ex)
            {
                return Ok($"An error occurred: {ex.Message}");
                throw;
            }
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlogs(int id, BlogViewModel blog)
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

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                blog.Id = id;

                int result = db.Execute(query, blog);

                return Ok(result > 0 ? "Patching Successful." : "Patching Failed!");
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            string query = @"UPDATE [dbo].[Tbl_BLog]
            SET [DeleteFlag] = 1 WHERE [BlogId] = @Id and [DeleteFlag] = 0";

            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogViewModel
                {
                    Id = id
                });
                return Ok(result == 1 ? "Deleting Successful." : "Deleting Failed.");
            }
        }
    }
}
