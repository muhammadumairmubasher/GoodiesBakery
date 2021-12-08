using GoodiesBakery_BO;
using GoodiesBakery_DAL;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodiesBakery_BLL
{
    public class CartBLL
    {
        public bool isItemAddedToCart(int custID, int id, int qty)
        {
            CartDAL cartDAL = new CartDAL();
            return cartDAL.isItemAddToCart(custID, id, qty);
        }
        public List<CartBO> getAllSellingDetails()
        {
            CartDAL cartDAL = new CartDAL();
            return cartDAL.getSellingRecords();
        }
        public decimal getBillAgainstCustomer(int custID)
        {
            CartDAL cartDAL = new CartDAL();
            return cartDAL.generateBill(custID);
        }
        public List<CartBO> getInvoice(int custID)
        {
            CartDAL cartDAL = new CartDAL();
            return cartDAL.generateInvoice(custID);
        }
    }
}
