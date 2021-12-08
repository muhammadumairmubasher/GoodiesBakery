using GoodeiesBakery_DAL;
using GoodiesBakery_BO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodiesBakery_DAL
{
    public class CartDAL
    {
        public bool isItemAddToCart(int custID, int itemID, int qty)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                string query = $"INSERT INTO cart (customerID, itemID, quantity, pricePerItem,amount) values (@custID,@itemID,@quantity,@price,@amount)";

                SqlParameter p1 = new SqlParameter("@custID", custID);
                SqlParameter p2 = new SqlParameter("@itemID", itemID);
                SqlParameter p3 = new SqlParameter("@quantity", qty);

                ItemDAL itemDAL = new ItemDAL();
                decimal price = itemDAL.getPrice(itemID);
                SqlParameter p4 = new SqlParameter("@price", price);
                SqlParameter p5 = new SqlParameter("@amount", price * qty);

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.Parameters.Add(p4);
                cmd.Parameters.Add(p5);
                con.Open();
                if (cmd.ExecuteNonQuery() >= 1)
                {
                    int updatedQty = itemDAL.getQuantity(itemID) - qty;
                    itemDAL.updateQuantity(itemID, updatedQty);
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
        public List<CartBO> getSellingRecords()
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            List<CartBO> ListOfCartBO = new List<CartBO>();
            try
            {
                con.Open();
                string query = "SELECT * FROM cart";
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CartBO cartBO = new CartBO();
                    cartBO.CustomerID = System.Convert.ToInt32(dr[0]);
                    cartBO.ItemID= System.Convert.ToInt32(dr[1]);
                    cartBO.Quantity = System.Convert.ToInt32(dr[2]);
                    cartBO.PricePerItem = System.Convert.ToDecimal(dr[3]);
                    cartBO.Amount= System.Convert.ToDecimal(dr[4]);

                    ListOfCartBO.Add(cartBO);
                }
                return ListOfCartBO;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return ListOfCartBO;
            }
            finally
            {
                con.Close();
            }
        }
        public decimal generateBill(int custID)
        {
            decimal totalBill = 0;
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            try
            {
                con.Open();
                string query = "SELECT * FROM cart where customerID=@custID";
                SqlParameter p1 = new SqlParameter("@custID", custID);
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    totalBill=totalBill+ System.Convert.ToDecimal(dr[4]);
                }
                return totalBill;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return totalBill;
            }
            finally
            {
                con.Close();
            }
        }
        public List<CartBO> generateInvoice(int custID)
        {
            string conString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GoodiesBakery;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            SqlConnection con = new SqlConnection(conString);
            List<CartBO> ListOfCartBO = new List<CartBO>();
            try
            {
                con.Open();
                string query = "SELECT * FROM cart where customerID=@custID";
                SqlParameter p1 = new SqlParameter("@custID", custID);

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.Add(p1);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CartBO cartBO = new CartBO();
                    cartBO.CustomerID = System.Convert.ToInt32(dr[0]);
                    cartBO.ItemID = System.Convert.ToInt32(dr[1]);
                    cartBO.Quantity = System.Convert.ToInt32(dr[2]);
                    cartBO.PricePerItem = System.Convert.ToDecimal(dr[3]);
                    cartBO.Amount = System.Convert.ToDecimal(dr[4]);

                    ListOfCartBO.Add(cartBO);
                }
                return ListOfCartBO;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return ListOfCartBO;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
