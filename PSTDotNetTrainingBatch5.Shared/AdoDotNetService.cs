﻿using System.Data;
using System.Data.SqlClient;

namespace PSTDotNetTrainingBatch5.Shared
{
    public class AdoDotNetService
    {
        private readonly string _connectionString;

        public AdoDotNetService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DataTable Query(string query, params SqlParametersModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            return dt;
        }

        public int Execute(string query, params SqlParametersModel[] sqlParameters)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.Name, sqlParameter.Value);
                }
            }

            var result = cmd.ExecuteNonQuery();

            connection.Close();

            return result;
        }
    }

    public class SqlParametersModel
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public SqlParametersModel() { }

        public SqlParametersModel(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }



}
