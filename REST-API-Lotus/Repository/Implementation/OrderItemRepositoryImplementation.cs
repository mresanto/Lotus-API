using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using REST_API_Lotus.Model;
using System;
using System.Data;
using System.Text;

namespace REST_API_Lotus.Repository
{
    public class OrderItemRepositoryImplementation : IOrderItemRepository
    {
        private readonly IConfiguration _configuration;
        internal DBConnection db { get; set; }

        public OrderItemRepositoryImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JsonResult Create(OrderItem orderItem)
        {
            string query = "spInsertOrderItem";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand Cmd = new MySqlCommand(query, mycon))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("varProdBarCode", orderItem.prodBarCode);
                    Cmd.Parameters.AddWithValue("varItemUnitaryPrice", orderItem.itemUnitaryPrice);
                    Cmd.Parameters.AddWithValue("varItemAmount", orderItem.itemAmount);
                    Cmd.Parameters.AddWithValue("varTotalPrice", orderItem.totalPrice);
                    Cmd.Parameters.AddWithValue("varCustCPF", orderItem.custCPF);

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
