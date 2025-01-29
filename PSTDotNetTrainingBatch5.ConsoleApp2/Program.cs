// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;
using PSTDotNetTrainingBatch5.Database.Models;
using System.Text.Json.Nodes;

Console.WriteLine("Hello, World!");

//AppDbContext db = new AppDbContext();
//var lst = db.TblBlogs.ToList();

var blog = new BlogModel
{
    BlogId = 1,
    BlogTitle = "First Blog",
    BlogAuthor = "Author 1",
    BlogContent = "This is the first blog content."
};

// Encode, Decode, Encryption, Decryption

//string jsonStr = JsonConvert.SerializeObject(Blog, Formatting.Indented); //C# to JSON
string jsonStr = blog.ToJson();

Console.WriteLine(jsonStr);
//Console.ReadLine();


string JsonStr = """{"BlogId": 1,"BlogTitle": "First Blog","BlogAuthor": "Author 1","BlogContent": "This is the first blog content."}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(JsonStr); //JSON to C#

Console.WriteLine(blog2.BlogTitle);
Console.ReadLine();

public class BlogModel
{
    public int BlogId { get; set; }
    public string? BlogTitle { get; set; }
    public string? BlogAuthor { get; set; }
    public string? BlogContent { get; set; }
}

public static class Extension // DevCode
{
    public static string ToJson(this object obj) 
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}