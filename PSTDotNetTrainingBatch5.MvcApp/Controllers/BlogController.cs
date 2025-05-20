using Microsoft.AspNetCore.Mvc;
using PSTDotNetTrainingBatch5.Database.Models;
using PSTDotNetTrainingBatch5.Domain.Features.Blog;
using PSTDotNetTrainingBatch5.MvcApp.Models;
using System.Reflection.Metadata;

namespace PSTDotNetTrainingBatch5.MvcApp.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var lst = _blogService.GetBlogs();
            return View(lst);
        }

        [ActionName("Create")]
        public IActionResult CreateBlog()
        {

            return View("CreateBlog");
        }

        [HttpPost]
        [ActionName("Save")]
        public IActionResult SaveBlog(BlogRequestModel requestModel)
        {
            try
            {
                _blogService.CreateBlog(new TblBlog
                {
                    BlogTitle = requestModel.Title,
                    BlogAuthor = requestModel.Author,
                    BlogContent = requestModel.Content,

                });

                // Can`t work in next page
                //ViewBag.IsSuccess = true;
                //ViewBag.Message = "Create Blog Successfully";

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Create Blog Successfully";


                //return View("Index"); // can not use this because of the data is not binded

                //var lst = _blogService.GetBlogs();
                //return View("Index", lst);
            }
            catch (Exception ex)
            {
                //ViewBag.IsSuccess = false;
                //ViewBag.Message = ex.ToString(); // Detail message
                //ViewBag.Message = ex.Message; // Summary message

                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public IActionResult DeleteBlog(int id)
        {
            try
            {
                var blog = _blogService.DeleteBlog(id);
                TempData["IsSuccess"] = true;
                TempData["Message"] = "Blog Deleted";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = true;
                TempData["Message"] = ex.Message;
            }
            return RedirectToAction("Index");
        }

        [ActionName("Edit")]
        public IActionResult EditBlog(int id)
        {
            var blog = _blogService.GetBlog(id);
            BlogRequestModel blogrequestModel = new BlogRequestModel
            {
                Id = blog.BlogId,
                Title = blog.BlogTitle,
                Author = blog.BlogAuthor,
                Content = blog.BlogContent
            };

            return View("EditBlog", blogrequestModel);
        }

        [HttpPost]
        [ActionName("Update")]
        public IActionResult UpdateBlog(int id, BlogRequestModel requestModel)
        {
            try
            {
                _blogService.UpdateBlog(id, new TblBlog
                {
                    BlogTitle = requestModel.Title,
                    BlogAuthor = requestModel.Author,
                    BlogContent = requestModel.Content,
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Update Blog Successfully";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }

            return RedirectToAction("Index");
        }
    }
}
