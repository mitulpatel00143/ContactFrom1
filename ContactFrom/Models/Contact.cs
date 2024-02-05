using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ContactFrom.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool Complaint { get; set; }
        public bool Suggestion { get; set; }
        public bool Praise { get; set; }

        SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ToString());

        public bool InsertContact(Contact data)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spInsertUpdateContact", cn);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@id", data.Id);
                cmd.Parameters.AddWithValue("@name", data.Name);
                cmd.Parameters.AddWithValue("@phone", data.Phone);
                cmd.Parameters.AddWithValue("@email", data.Email);
                cmd.Parameters.AddWithValue("@desc", data.Description);
                cmd.Parameters.AddWithValue("@reclamacao", data.Complaint);
                cmd.Parameters.AddWithValue("@sugestao", data.Suggestion);
                cmd.Parameters.AddWithValue("@elogio", data.Praise);
                cn.Open();
                var result = cmd.ExecuteNonQuery();
                cn.Close();
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