using System;
using GoodiesBakery_BO;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
namespace GoodeiesBakery_DAL
{
    public class AdminDAL
    {
        public bool isLogin(AdminBO adminBO)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection conn = new SqlConnection(conString);
            try
            {
                conn.Open();
                string query = "SELECT username, password FROM admin WHERE username=@u and password=@p";
                SqlParameter p1 = new SqlParameter("u", adminBO.Username);
                SqlParameter p2 = new SqlParameter("p",adminBO.Password);

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
