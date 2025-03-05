using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PSTDotNetTrainingBatch5.ConsoleApp4.Models
{
    public class BlogDapperDataModel
    {
        public int BlogId { get; set; }
        public string? BlogTitle { get; set; }
        public string? BlogAuthor { get; set; }
        public string? BlogContent { get; set; }
        public bool DeleteFlag { get; set; }
    }

    [Table("Tbl_Blog")]
    public class BlogDataModel
    {
        [Key]
        [Column("BlogId")]
        public int BlogId { get; set; }

        [Column("BlogTitle")]
        public string? BlogTitle { get; set; }

        [Column("BlogAuthor")]
        public string? BlogAuthor { get; set; }

        [Column("BlogContent")]
        public string? BlogContent { get; set; }

        [Column("DeleteFlag")]
        public bool DeleteFlag { get; set; }
    }
}
