using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetTrainingBatch5.Shared
{
    public class DapperService
    {
        private readonly string _connectionString;
        private readonly IDbConnection _db;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;

            _db = new SqlConnection(_connectionString);
        }

        public List<T> Query<T>(string query, object? param = null)
        {
            //using IDbConnection db = new SqlConnection(_connectionString);
            var lst = _db.Query<T>(query, param).ToList();

            return lst;
        }

        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            //using IDbConnection db = new SqlConnection(_connectionString);
            var item = _db.QueryFirstOrDefault<T>(query, param);

            return item;
        }

        public int Execute(string query, object? param = null)
        {
            //using IDbConnection db = new SqlConnection(_connectionString);
            var result = _db.Execute(query, param);

            return result;
        }

        #region Open_Close Principle
        //  ရှိပြီးသား code ကို modify မလုပ်ရဘူး features အသစ်ထပ်ထည့်နိုင်ရမယ်။
        #endregion
    }
}
