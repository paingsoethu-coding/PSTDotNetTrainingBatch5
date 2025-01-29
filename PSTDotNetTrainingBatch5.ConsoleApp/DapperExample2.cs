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
    }
}
