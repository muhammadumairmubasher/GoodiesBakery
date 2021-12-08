using System;
using GoodiesBakery_BO;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Text;

namespace GoodeiesBakery_DAL
{
    public class ItemDAL
    {
        public bool isInserted(ItemBO itemBO)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"INSERT INTO item values (@name,@quantity,@price)";

                SqlParameter p1 = new SqlParameter("@name", itemBO.Name);
                SqlParameter p2 = new SqlParameter("@quantity", itemBO.Quantity);
                SqlParameter p3 = new SqlParameter("@price", itemBO.Price);


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
        public bool isValidID(int id)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                string query = $"SELECT * FROM item WHERE Id=@id";
                SqlParameter p1 = new SqlParameter("@id", id);
               
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);
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
                con.Close();
            }
        }
        public int getQuantity(int id)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                string query = $"SELECT quantity FROM item WHERE Id=@id";
                SqlParameter p1 = new SqlParameter("@id", id);

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
        
        public decimal getPrice(int id)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                string query = $"SELECT price FROM item WHERE Id=@id";
                SqlParameter p1 = new SqlParameter("@id", id);

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return System.Convert.ToDecimal(dr[0]);
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
        public bool updateQuantity(int itemID, int qty)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"UPDATE item SET quantity=@qty where Id=@id";

                SqlParameter p1 = new SqlParameter("@id", itemID);
                SqlParameter p2 = new SqlParameter("@qty", qty);

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);

                con.Open();
                if (cmd.ExecuteNonQuery() >= 1)
                {
                    return true;
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
            return false;
        }
        public bool isUpdated(ItemBO itemBO) 
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"UPDATE item SET name=@nam,quantity=@qty,price=@pr where Id=@id";

                SqlParameter p1 = new SqlParameter("@id", itemBO.ID);
                SqlParameter p2 = new SqlParameter("@nam", itemBO.Name);
                SqlParameter p3 = new SqlParameter("@qty", itemBO.Quantity);
                SqlParameter p4 = new SqlParameter("@pr", itemBO.Price);

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);

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
        public bool isDeleted(int id)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"DELETE FROM item WHERE Id=@id";

                SqlParameter p1 = new SqlParameter("@id", id);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);

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
        public List<ItemBO> getAllItems()
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            List<ItemBO> ListOfItemBO = new List<ItemBO>();
            try
            {
                con.Open();
                string query = "SELECT * FROM item";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ItemBO itemBO = new ItemBO();
                    itemBO.ID = System.Convert.ToInt32(dr[0]);
                    itemBO.Name = System.Convert.ToString(dr[1]);
                    itemBO.Quantity = System.Convert.ToInt32(dr[2]);
                    itemBO.Price = System.Convert.ToDecimal(dr[3]);

                    ListOfItemBO.Add(itemBO);               
                }
                return ListOfItemBO;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return ListOfItemBO;
            }
            finally
            {
                con.Close();
            }
        }
        public string getItemName(int itemID)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            List<ItemBO> ListOfItemBO = new List<ItemBO>();
            try
            {
                con.Open();
                string query = $"SELECT name FROM item where Id=@id";
                
                SqlParameter p1 = new SqlParameter("@id", itemID);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);

                SqlDataReader dr = cmd.ExecuteReader();
                string itemName=string.Empty;
                while (dr.Read())
                {
                    itemName = System.Convert.ToString(dr[0]);
                }
                return itemName;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return "";
            }
            finally
            {
                con.Close();
            }
        }
    }
}
