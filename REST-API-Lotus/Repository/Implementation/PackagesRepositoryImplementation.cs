using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using REST_API_Lotus.Model;
using System.Data;
using System.Text;

namespace REST_API_Lotus.Repository
{
    public class PackagesRepositoryImplementation : IPackagesRepository
    {
        private readonly IConfiguration _configuration;
        internal DBConnection db { get; set; }

        public PackagesRepositoryImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string FindAll()
        {
            string vview = "SelectPackage";
            string query = @"Select * from "+ vview +";";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;

            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }
            };


            return DataTableToJsonObj(table);
        }

        //public string FindByCategory(string category)
        //{
        //    string query;
        //    query = @"Select * from SelectProduct;";
        //    if (category != "Todos")
        //    query = @"Select * from SelectProduct where CatName ='" + category + "';";
        //
        //    DataTable table = new DataTable();
        //    string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
        //    MySqlDataReader myReader;
        //
        //    using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
        //    {
        //        mycon.Open();
        //        using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
        //        {
        //            myReader = myCommand.ExecuteReader();
        //            table.Load(myReader);
        //
        //            myReader.Close();
        //            mycon.Close();
        //        }
        //    };
        //
        //
        //    return DataTableToJsonObj(table);
        //}
        //
        public string FindByCode(int packcode)
        {
            string vview = "SelectPackage";
            string query = @"Select * from " + vview + " where PackCode = " + packcode + ";";
        
            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;
        
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, mycon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
        
                    myReader.Close();
                    mycon.Close();
                }
            };
        
        
            return DataTableToJsonObj(table);
        }

        public string DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }

    }
}
