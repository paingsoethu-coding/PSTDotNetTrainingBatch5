using Dapper;
using PSTDotNetTrainingBatch5.ConsoleApp.Models;
using PSTDotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetTrainingBatch5.ConsoleApp
{
    internal class DapperExample2
    {
        private readonly string _connectionString = "Data Source= MSI\\SQLEXPRESS2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True;";
        private readonly DapperService _dapperService;
        public DapperExample2()
        {
            _dapperService = new DapperService(_connectionString);
        }
        public void Read()
        {
            string query = @"SELECT * FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";
            var lst = _dapperService.Query<BlogDapperDataModel>(query).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Edit(int Id)
        {
            string query = $@"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                                 FROM [dbo].[Tbl_BLog] 
                                where BlogId = @BlogId and [DeleteFlag] = 0";

            
            var item = _dapperService.QueryFirstOrDefault<BlogDapperDataModel>(query, 
                new BlogDapperDataModel
                {
                    BlogId = Id
                });

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
            
            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            });
            Console.WriteLine(result == 1 ? "Saving Successful." : "Saving Failed.");
            
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

            try
            {
                if (!int.TryParse(id, out int blogId))
                {
                    throw new ArgumentNullException("ID must be a valid integer.", nameof(id));
                }

                int result = _dapperService.Execute(query, new BlogDapperDataModel
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

            int result = _dapperService.Execute(query, new BlogDapperDataModel
            {
                BlogId = int.Parse(id)
            });
            Console.WriteLine(result == 1 ? "Deleting Successful." : "Deleting Failed.");

        }
    }
}
