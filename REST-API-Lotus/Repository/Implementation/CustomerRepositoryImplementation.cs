using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using REST_API_Lotus.Model;
using System.Data;
using System.Text;

namespace REST_API_Lotus.Repository
{
    public class CustomerRepositoryImplementation : ICustomerRepository
    {
        private readonly IConfiguration _configuration;
        internal DBConnection db { get; set; }

        public CustomerRepositoryImplementation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public JsonResult Create(Customer customer)
        {
            string query = "spInsertCustomer";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;
            using(MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand Cmd = new MySqlCommand(query, mycon))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("varCustCPF", customer.custcpf);
                    Cmd.Parameters.AddWithValue("varCustName", customer.custname);
                    Cmd.Parameters.AddWithValue("varCustDtNasc", customer.custdtnasc);
                    Cmd.Parameters.AddWithValue("varCustGender", customer.custgender);
                    Cmd.Parameters.AddWithValue("varCustTel", customer.custtel);
                    Cmd.Parameters.AddWithValue("varCustEmail", customer.custemail);
                    Cmd.Parameters.AddWithValue("varCustPassword", customer.custpassword);
                    Cmd.Parameters.AddWithValue("varCustNumberAddress", customer.custnumberaddress);
                    Cmd.Parameters.AddWithValue("varCEP", customer.cepaddress);

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
            string query = @"SELECT * FROM tbcustomer";

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

        public string FindByEmail(string email)
        {
            string query = @"select * from SelectCustomer where custemail ='" + email +"';";

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

        public string Validation(string cpf, string email, string password)
        {
            string query = @"SELECT F_Validation_Log('"+ email +"','" + password +"') as msg;";
            if (cpf != "0")
            { 
                query = @"SELECT F_Validation_Cad('" + cpf + "','" + email + "') as msg;";
            }

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
            }

            return DataTableToJsonObj(table);
        }

        public JsonResult Update(Customer customer)
        {
            string query = "spUpdateCustomer";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration["MySQLConnection:MySQLConnectionString"];
            MySqlDataReader myReader;
            using (MySqlConnection mycon = new MySqlConnection(sqlDataSource))
            {
                mycon.Open();
                using (MySqlCommand Cmd = new MySqlCommand(query, mycon))
                {
                    Cmd.CommandType = CommandType.StoredProcedure;
                    Cmd.Parameters.AddWithValue("varCustEmail", customer.custemail);
                    Cmd.Parameters.AddWithValue("varCustName", customer.custname);
                    Cmd.Parameters.AddWithValue("varCustDtNasc", customer.custdtnasc);
                    Cmd.Parameters.AddWithValue("varCustGender", customer.custgender);
                    Cmd.Parameters.AddWithValue("varCustPassword", customer.custpassword);
                    Cmd.Parameters.AddWithValue("varCustNumberAddress", customer.custnumberaddress);
                    Cmd.Parameters.AddWithValue("varCEP", customer.cepaddress);

                    myReader = Cmd.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    mycon.Close();
                }


            }

            return new JsonResult("Added Successfully");
        }

        public bool Exists(long id)
        {
            return false;//_configuration.Any(p => p.Id.Equals(id));
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
