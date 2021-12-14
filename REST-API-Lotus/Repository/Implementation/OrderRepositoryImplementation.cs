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
            if (order.payOption != null)
            {
                MySqlDataReader myReader;
                using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
                {

                    mycon.Open();
                    using (MySqlCommand Cmd = new MySqlCommand(query, mycon))
                    {
                        Cmd.CommandType = CommandType.StoredProcedure;
                        Cmd.Parameters.AddWithValue("varOrdDate", order.ordDate);
                        Cmd.Parameters.AddWithValue("varTotalPrice", order.totalPrice);
                        Cmd.Parameters.AddWithValue("varStatusOrder", order.statusOrder);
                        Cmd.Parameters.AddWithValue("varCustCPF", order.custCPF);
                        Cmd.Parameters.AddWithValue("varPayDate", order.payDate);
                        Cmd.Parameters.AddWithValue("varIsDeleted", order.isDeleted);
                        Cmd.Parameters.AddWithValue("varStatusPayment", order.statusPayment);
                        Cmd.Parameters.AddWithValue("varPayOption", order.payOption);

                        myReader = Cmd.ExecuteReader();
                        table.Load(myReader);

                        myReader.Close();
                        mycon.Close();
                    }
                }
            }

            return new JsonResult("Added Successfully");
        }

        public string FindAll()
        {
            string query = @"SELECT * FROM SelectOrder Where StatusOrder = 'U'";

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

        public string FindByCode(string email)
        {
            string query = @"select * from SelectOrder where  StatusOrder = 'U' AND custEmail ='" + email + "';";

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
                    //Cmd.CommandType = CommandType.StoredProcedure;
                    //Cmd.Parameters.AddWithValue("varOrdCode ", order.ordcode);
                    //Cmd.Parameters.AddWithValue("varProdBarCode ", order.prodbarcode);
                    //Cmd.Parameters.AddWithValue("varItemUnitaryPrice ", order.itemunitaryprice);
                    //Cmd.Parameters.AddWithValue("varItemAmount ", order.ItemAmount);
                    //Cmd.Parameters.AddWithValue("varItemTotalPrice  ", order.ItemTotalPrice);
        
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
