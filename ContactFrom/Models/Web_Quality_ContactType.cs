using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactFrom.Models
{
    public class Web_Quality_ContactType
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<Web_Quality_ContactType> Web_Quality_GetContactType()
        {
            try
            {
                DataSet dataSet = GetDataSet("spWeb_Quality_GetContactType");
                List<Web_Quality_ContactType> types = new List<Web_Quality_ContactType>();

                foreach (DataRow dr in dataSet.Tables[0].Rows)
                {

                    Web_Quality_ContactType obj = new Web_Quality_ContactType()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Name = Convert.ToString(dr["Name"]),
                    };
                    types.Add(obj);
                }

                return types;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataSet GetDataSet(string sql)
        {
            var result = new DataSet();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ToString()))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = sql;

                    try
                    {
                        con.Open();
                        var reader = command.ExecuteReader();

                        do
                        {
                            var tb = new DataTable();
                            tb.Load(reader);
                            result.Tables.Add(tb);
                        } while (!reader.IsClosed);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
            }

            return result;
        }


    }
}