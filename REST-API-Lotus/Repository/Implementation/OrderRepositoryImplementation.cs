using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using REST_API_Lotus.Model;
using System.Data;
using System.Text;

namespace REST_API_Lotus.Repository
{
    public class OrderRepositoryImplementation : IOrderRepository
    {
        private readonly IConfiguration _configuration;
        internal DBConnection db { get; set; }

        public OrderRepositoryImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JsonResult Create(Order order)
        {
            string query = "spInsertOrder";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand Cmd = new MySqlCommand(query, mycon))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("varOrderCode", order.ordercode);
                    Cmd.Parameters.AddWithValue("varOrderDate", order.orderdata);
                    Cmd.Parameters.AddWithValue("varOrderValue", order.ordervalue);
                    Cmd.Parameters.AddWithValue("varCustCpf", order.custcpf);

                    myReader = Cmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }


            }

            return new JsonResult("Added Successfully");
        }

        public void Delete(long id)
        {

        }

        public string FindAll()
        {
            string query = @"SELECT * FROM tbOrder";

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
        public JsonResult Update(Order order)
        {
            string query = "spUpdateOrder";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand Cmd = new MySqlCommand(query, mycon))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("varOrderCode", order.ordercode);
                    Cmd.Parameters.AddWithValue("varOrderDate", order.orderdata);
                    Cmd.Parameters.AddWithValue("varOrderValue", order.ordervalue);
                    Cmd.Parameters.AddWithValue("varCustCpf", order.custcpf);

                    myReader = Cmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }


            }

            return new JsonResult("Added Successfully");
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
