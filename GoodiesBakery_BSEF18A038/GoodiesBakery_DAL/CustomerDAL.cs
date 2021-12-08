using System;
using GoodiesBakery_BO;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using GoodeiesBakery_DAL;

namespace GoodiesBakery_DAL
{
    public class CustomerDAL
    {
        public bool isCustomerAdded(CustomerBO custBO)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"INSERT INTO customer (name, phoneNo) values (@name,@phoneNo)";

                SqlParameter p1 = new SqlParameter("@name", custBO.Name);
                SqlParameter p2 = new SqlParameter("@phoneNo", custBO.PhoneNo);

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);

                con.Open();
                if (cmd.ExecuteNonQuery() >= 1)
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
                con.Close();
            }
        }
        public int getCusomerID(CustomerBO custBO)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                string query = $"SELECT customerID FROM customer WHERE name=@nam";
                SqlParameter p1 = new SqlParameter("@nam", custBO.Name);

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return System.Convert.ToInt32(dr[0]);
                }
                else
                {
                    return -999999;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return -99999;
            }
            finally
            {
                con.Close();
            }
        }
        public bool isCustomerUpdated(CustomerBO custBO)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"UPDATE customer SET name=@nam, phoneNo=@phone where customerID=@id";

                SqlParameter p1 = new SqlParameter("@id", custBO.CustomerID);
                SqlParameter p2 = new SqlParameter("@nam", custBO.Name);
                SqlParameter p3 = new SqlParameter("@phone", custBO.PhoneNo);

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);

                con.Open();
                if (cmd.ExecuteNonQuery() >= 1)
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
                con.Close();
            }
        }
    }
}
