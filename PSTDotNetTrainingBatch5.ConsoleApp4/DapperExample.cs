using Dapper;
using PSTDotNetTrainingBatch5.ConsoleApp4.Models;
using System.Data;
using System.Data.SqlClient;

namespace PSTDotNetTrainingBatch5.ConsoleApp4
{
    public class DapperExample : IDapperExample
    {
        private readonly IDbConnection _db;

        public DapperExample(IDbConnection db)
        {
            _db = db;
        }

        public void Read()
        {
            string query = @"SELECT * FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";
            var lst = _db.Query<BlogDapperDataModel>(query).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_BLog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent]
           ,[DeleteFlag])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent
           ,'0')";

            int result = _db.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
        }

        public void Edit(int Id)
        {
            string query = $@"SELECT [BlogId]
      ,[BlogTitle]
      ,[BlogAuthor]
      ,[BlogContent]
      ,[DeleteFlag]
  FROM [dbo].[Tbl_BLog] where BlogId = @BlogId and [DeleteFlag] = 0";

            var item = _db.Query<BlogDapperDataModel>(query, new BlogDapperDataModel
            {
                BlogId = Id
            }).FirstOrDefault();

            if (item is null)
            {
                Console.WriteLine("No data found.");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        public void Update()
        {
            Console.WriteLine("Please Blog ID: ");
            string? id = Console.ReadLine();

            Console.WriteLine("Please Blog Title: ");
            string? title = Console.ReadLine();

            Console.WriteLine("Please Blog Author: ");
            string? author = Console.ReadLine();

            Console.WriteLine("Please Blog Content: ");
            string? content = Console.ReadLine();

            string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
      ,[DeleteFlag] = 0
 WHERE [BlogId] = @BlogId and [DeleteFlag] != 1";

            #region ပြန်ကြည့်ရန် (warning CS8604 ဖြေရှင်းနည်းပါ)

            //  ဒါတွေ အကုန်လုံးကိုလုပ်နေတာ null value ကိုလက်ခံလို့မရတဲ့နေရာက int ကို warning လည်းပျောက်ရမယ်။
            //  တန်ဖိုးကိုလည်း ပေးကိုပေးရန် အတွက်လုပ်ထားတာပါ။ (warning CS8604 ဖြေရှင်းနည်းပါ)

            // ၁။   try catch ကိုရေးထားရတာက error message က စာကိုပြန်စေချင်လို့ပါ console မှာက စာကိုပြန်ဒီတိုင်းပြန်ရင်လဲရတယ်
            //      ဒါပေမယ့် ပိုသေချာချင်လို့ရယ် id ကို null ဖြစ်လာမယ်အခါ null value ကို run သွားပြီး database မှာသွားရှာနေတာကို ကာကွယ်ချင်လို့ပါ

            // ၂။   စာကိုဒီတိုင်းပြန်ပြထားတာပါ အဖြေကတော့ရတယ် လက်မခံတာကို ပြပေးတယ် ရပ်သွားတယ်ဆိုပေမယ့် ရေးထားကုဒ်ကို လုံနေချင်လို့ပါ/

            //using (IDbConnection db = new SqlConnection(_connectionString))
            //{
            //    if (!int.TryParse(id, out int blogId))
            //    {
            //        throw new ArgumentNullException("ID must be a valid integer.", nameof(id));
            //    }
            //    int result = db.Execute(query, new BlogDataModel
            //    {
            //        BlogId = int.Parse(id),
            //        BlogTitle = title,
            //        BlogAuthor = author,
            //        BlogContent = content
            //    });
            //    Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
            //}

            #endregion

            try
            {
                if (!int.TryParse(id, out int blogId))
                {
                    throw new ArgumentNullException("ID must be a valid integer.", nameof(id));
                }
                int result = _db.Execute(query, new BlogDapperDataModel
                {
                    BlogId = int.Parse(id),
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content
                });
                Console.WriteLine(result == 1 ? "Updating Successful." : "Updating Failed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        public void Delete()
        {
            Console.Write("Blog Id: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) { return; }

            string query = @"UPDATE [dbo].[Tbl_BLog]
   SET [DeleteFlag] = 1
 WHERE [BlogId] = @BlogId and [DeleteFlag] = 0";

            int result = _db.Execute(query, new BlogDapperDataModel
            {
                BlogId = int.Parse(id)
            });
            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");
        }

    }
}
