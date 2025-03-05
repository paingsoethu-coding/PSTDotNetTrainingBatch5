namespace PSTDotNetTrainingBatch5.RestApi4.DataModels
{
    public class BlogDataModel
    {
        public int BlogId { get; set; }
        public string BlogTitle { get; set; } = null!;
        public string BlogAuthor { get; set; } = null!;
        public string BlogContent { get; set; } = null!;
        public bool DeleteFlag { get; set; }
        
    }
}
