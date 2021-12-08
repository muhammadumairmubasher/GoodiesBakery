using System;
using GoodiesBakery_BO;
using GoodeiesBakery_DAL;

namespace GoodiesBakery_BLL
{
    public class AdminBLL
    {
        public bool isValidCrediential(AdminBO adminBO)
        {
            AdminDAL adminDAL = new AdminDAL();
            return adminDAL.isLogin(adminBO);
        }
    }
}
