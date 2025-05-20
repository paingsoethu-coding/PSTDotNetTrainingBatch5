namespace PSTDotNetTrainingBatch5.MvcApp.Models;

public class BlogRequestModel
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Content { get; set; } = null!;

    public bool DeleteFlag { get; set; }
}
