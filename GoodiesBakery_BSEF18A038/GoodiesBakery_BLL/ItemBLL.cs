using System;
using GoodiesBakery_BO;
using GoodeiesBakery_DAL;
using System.Collections.Generic;
using System.Text;

namespace GoodiesBakery_BLL
{
    public class ItemBLL
    {
        public bool isInsertionSuccessful(ItemBO itemBO)
        {
            ItemDAL itemDAL = new ItemDAL();
            return itemDAL.isInserted(itemBO);
        }

        public bool isUpdationSuccessful(ItemBO itemBO)
        {
            ItemDAL itemDAL = new ItemDAL();
            return itemDAL.isUpdated(itemBO);
        }
        public bool isIDExist(int id)
        {
            ItemDAL itemDAL = new ItemDAL();
            return itemDAL.isValidID(id);
        }
        public bool isDeletionSuccessful(int id)
        {
            ItemDAL itemDAL = new ItemDAL();
            return itemDAL.isDeleted(id);
        }
        public List<ItemBO> getItemsDetails()
        {
            ItemDAL itemDAL = new ItemDAL();
            return itemDAL.getAllItems();
        }
        public bool isValidQuantityOfItem(int id, int qty)
        {
            ItemDAL itemDAL = new ItemDAL();
            return (qty <= itemDAL.getQuantity(id)) ? true : false;
        }
        public string getItemName(int itemID)
        {
            ItemDAL itemDAL = new ItemDAL();
            return itemDAL.getItemName(itemID);
        }
    }
}
