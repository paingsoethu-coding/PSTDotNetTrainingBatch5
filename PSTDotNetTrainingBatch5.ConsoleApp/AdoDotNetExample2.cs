using PSTDotNetTrainingBatch5.Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetTrainingBatch5.ConsoleApp
{
    internal class AdoDotNetExample2
    {
        private readonly string _connectionString = "Data Source= MSI\\SQLEXPRESS2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True";

        private readonly AdoDotNetService _adoDotNetService;

        public AdoDotNetExample2()
        {
            _adoDotNetService = new AdoDotNetService(_connectionString);
        }

        public void Read()
        {
            string query = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlag]
                              FROM [dbo].[Tbl_BLog] where Tbl_Blog.[DeleteFlag] = 0";

            var dt = _adoDotNetService.Query(query);

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }
        }

        public void Edit()
        {
            Console.Write("Blog Id: ");
            string? id = Console.ReadLine();


            string query = $@"SELECT [BlogId]
                                    ,[BlogTitle]
                                    ,[BlogAuthor]
                                    ,[BlogContent]
                                    ,[DeleteFlag]
                                    FROM [dbo].[Tbl_BLog] where BlogId = @BlogId and [DeleteFlag] = 0";

            #region Old code
            //SqlParametersModel[] SqlParameters = new SqlParametersModel[1];
            //SqlParameters[0] = new SqlParametersModel 
            //{ 
            //    Name = "@BlogId",
            //    Value = id 
            //};

            //var dt = _adoDotNetService.Query(query, SqlParameters);

            //var dt = _adoDotNetService.Query(query, new SqlParametersModel
            //{
            //    Name = "@BlogId",
            //    Value = id
            //});
            #endregion

            var dt = _adoDotNetService.Query(query, 
                new SqlParametersModel("@BlogId", id));

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
        }

        public void Create()
        {
            Console.WriteLine("Please Blog Title: ");
            string title = Console.ReadLine() ?? string.Empty;
            if (string.IsNullOrEmpty(title))
            {
                Console.WriteLine("We didn`t allow the null value.");
                return;
            }

            Console.WriteLine("Please Blog Author: ");
            string? author = Console.ReadLine();
            if (string.IsNullOrEmpty(author))
            {
                Console.WriteLine("We didn`t allow the null value.");
                return;
            }

            Console.WriteLine("Please Blog Content: ");
            string? content = Console.ReadLine();
            if (string.IsNullOrEmpty(content))
            {
                Console.WriteLine("We didn`t allow the null value.");
                return;
            }

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

            int result = _adoDotNetService.Execute(query, 
                new SqlParametersModel("@BlogTitle", title),
                new SqlParametersModel("@BlogAuthor", author),
                new SqlParametersModel("@BlogContent", content));
            
            Console.WriteLine(result == 1 ? "Insert Successful." : "Inesert Failed.");

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

            int result = _adoDotNetService.Execute(query,
                new SqlParametersModel("@BlogTitle", id),
                new SqlParametersModel("@BlogTitle", title),
                new SqlParametersModel("@BlogAuthor", author),
                new SqlParametersModel("@BlogContent", content));

            Console.WriteLine(result == 1 ? "Update Successful." : "Update Failed.");
        }

        public void Delete()
        {
            Console.Write("Blog Id: ");
            string? id = Console.ReadLine();

            if (string.IsNullOrEmpty(id)) { return; }

            string query = @"UPDATE [dbo].[Tbl_BLog]
                               SET [DeleteFlag] = 1
                             WHERE [BlogId] = @BlogId and [DeleteFlag] = 0";

            int result = _adoDotNetService.Execute(query,
                new SqlParametersModel("@BlogId", id));

            Console.WriteLine(result == 1 ? "Delete Successful." : "Delete Failed.");
        }


    }
}
