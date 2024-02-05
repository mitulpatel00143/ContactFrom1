using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactFrom.Models
{
    public class Web_Quality_Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Customer_Number { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public int Web_QualityContactType_Id { get; set; }
        public string User_IP { get; set; }


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ToString());


        public bool Web_Quality_InsertContact(Web_Quality_Contact data)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spWeb_Quality_InsertContact", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", data.Name);
                cmd.Parameters.AddWithValue("@Customer_Number", data.Customer_Number);
                cmd.Parameters.AddWithValue("@Email", data.Email);
                cmd.Parameters.AddWithValue("@Description", data.Description);
                cmd.Parameters.AddWithValue("@Web_QualityContactType_Id", data.Web_QualityContactType_Id);
                cmd.Parameters.AddWithValue("@User_IP", data.User_IP);
                con.Open();
                var result = cmd.ExecuteNonQuery();
                con.Close();
                if (Convert.ToInt32(result) > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

    }
}