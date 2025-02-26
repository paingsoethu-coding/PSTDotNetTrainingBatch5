using Refit;
using System.Collections.Generic;

namespace PSTDotNetTrainingBatch5.ConsoleApp3;

public class RefitExample
{
    public async Task RunGet()
    {
        var blogApi = RestService.For<IBlogApi>("https://localhost:7240");
        var lst = await blogApi.GetBlogs();

        foreach (var item in lst)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
        }

        //var item2 = await blogApi.GetBlog(1);

        try
        {
            var item3 = await blogApi.GetBlog(100);
        }
        catch (ApiException ex)
        {
            
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found.");
            }
        }
    }

    public async Task RunCreate()
    {
        var blogApi = RestService.For<IBlogApi>("https://localhost:7240");
        
        var item = await blogApi.CreateBlog(new BlogModel()
        {
            BlogTitle = "New Blog Create Refit",
            BlogAuthor = "Author",
            BlogContent = "Content"
        });

        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
    }

    public async Task RunUpdate()
    {
        try
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7240");

            var model = new BlogModel
            {
                BlogTitle = "New Blog Update Refit",
                BlogAuthor = "Author",
                BlogContent = "Content"
            };

            var item = await blogApi.UpdateBlog(24, model);

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found.");
            }
        }
    }

    public async Task RunPatch()
    {
        try
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7240");
            
            var model = new BlogModel
            {
                BlogTitle = "New Blog Patch Refit",
                BlogAuthor = "Author",
                BlogContent = "Content"
            };

            var item = await blogApi.PatchBlogs(24, model);

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found.");
            }
        }

    }

    public async Task RunDelete()
    {
        try
        {
            var blogApi = RestService.For<IBlogApi>("https://localhost:7240");

            var item = await blogApi.DeleteBlogs(23);
            
            Console.WriteLine("Deleting Successful");
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
            Console.WriteLine(item.DeleteFlag);
        }
        catch (ApiException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No Data Found.");
            }
        }

    }
}
