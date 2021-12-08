using GoodiesBakery_BO;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace GoodiesBakery_DAL
{
    public class CustomerBLL
    {
        
        public void addTheCustomer(CustomerBO custBO)
        {
            CustomerDAL custDAL = new CustomerDAL();
            custDAL.isCustomerAdded(custBO);
        }
        public int getCustomerID(CustomerBO custBO)
        {
            CustomerDAL custDAL = new CustomerDAL();
            return custDAL.getCusomerID(custBO);
        }
        public void updateTheCustomer(CustomerBO custBO)
        {
            CustomerDAL custDAL = new CustomerDAL();
            custDAL.isCustomerUpdated(custBO);
        }
    }
}
