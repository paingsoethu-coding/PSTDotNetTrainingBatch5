using Refit;

namespace PSTDotNetTrainingBatch5.ConsoleApp3;

public interface IBlogApi
{
    [Get("/api/blogs")]
    Task<List<BlogModel>> GetBlogs();

    [Get("/api/blogs/{id}")]
    Task<BlogModel> GetBlog(int id);

    [Post("/api/blogs")]
    Task<BlogModel> CreateBlog(BlogModel model);

    [Put("/api/blogs/{id}")]
    Task<BlogModel> UpdateBlog(int id, BlogModel model);

    [Patch("/api/blogs/{id}")]
    Task<BlogModel> PatchBlogs(int id, BlogModel model);

    [Delete("/api/blogs/{id}")]
    Task<BlogModel> DeleteBlogs(int id);
}

public class BlogModel
{
    public int BlogId { get; set; }

    public string BlogTitle { get; set; } = null!;

    public string BlogAuthor { get; set; } = null!;

    public string BlogContent { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}