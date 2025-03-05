using PSTDotNetTrainingBatch5.Database.Models;

namespace PSTDotNetTrainingBatch5.Domain.Features.Blog;

public interface IBlogService
{
    TblBlog CreateBlog(TblBlog blog);
    bool? DeleteBlog(int id);
    TblBlog GetBlog(int id);
    List<TblBlog> GetBlogs();
    TblBlog PatchBlog(int id, TblBlog blog);
    TblBlog UpdateBlog(int id, TblBlog blog);
}

public interface IBlogServiceV2
{
    Task<TblBlog> CreateBlog(TblBlog blog);
    Task<bool?> DeleteBlog(int id);
    Task<TblBlog?> GetBlog(int id);
    Task<List<TblBlog>> GetBlogs();
    Task<TblBlog?> PatchBlog(int id, TblBlog blog);
    Task<TblBlog?> UpdateBlog(int id, TblBlog blog);
}