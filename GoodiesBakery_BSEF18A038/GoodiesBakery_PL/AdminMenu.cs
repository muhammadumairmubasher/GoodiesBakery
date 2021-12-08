using System;
using System.Collections.Generic;
using GoodiesBakery_BLL;
using GoodiesBakery_BO;

namespace GoodiesBakery_PL
{
    public class AdminMenu
    {
        public void login()
        {
            Console.Write("Please Enter Your Username:\t");
            string uname = Console.ReadLine();
            Console.Write("Please Enter Your Password:\t");
            string pwd = Console.ReadLine();

            AdminBO adminBo = new AdminBO
            {
                Username = uname,
                Password = pwd
            };

            AdminBLL adminBLL = new AdminBLL();
            if (adminBLL.isValidCrediential(adminBo))
            {
                Console.WriteLine($"Successfully Logined!");
                showItemMenu();
            }
            else
            {
                Console.WriteLine("...OOPs! Login Failed!");
            }
        }
        private int showItemMenu()
        {
            int choice = 1;
            while (choice != 6)
            {
                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("\t\tITEMS MENU");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("1 Add new Item");
                Console.WriteLine("2 Update an Item");
                Console.WriteLine("3 Delete an Items");
                Console.WriteLine("4 View All Selling Record");
                Console.WriteLine("5 View Inventory Details");
                Console.WriteLine("6 Back to main menu");
                Console.WriteLine("----------------------------------------------");
                do
                {
                    if (!(choice >= 1 && choice <= 6))
                    {
                        Console.WriteLine("...OOPS!You Enter Wrong Choice!");
                    }
                    Console.Write("Please Enter Item Menu Choice(1-5):\t");
                    int.TryParse(Console.ReadLine(), out choice);
                } while (!(choice >= 1 && choice <= 6));

                switch (choice)
                {
                    case 1:
                        addNewItem();
                        break;
                    case 2:
                        updateItem();
                        break;
                    case 3:
                        deleteItem();
                        break;
                    case 4:
                        printAllSellingRecord();
                        break;
                    case 5:
                        printInventorydetails();
                        break;
                    case 6:
                        choice = 6;
                        break;
                }
            }
            return choice;
        }
        private void addNewItem()
        {
            int quantity;
            decimal price;

            Console.Write("\nEnter Item Name:\t");
            string name = Console.ReadLine();
            Console.Write("Enter Item Quantity:\t");
            int.TryParse(Console.ReadLine(), out quantity);
            Console.Write("Enter Item Price:\t");
            decimal.TryParse(Console.ReadLine(),out price);

            ItemBO itemBO = new ItemBO()
            {
                Name = name,
                Quantity = quantity,
                Price = price
            };

            ItemBLL itemBLL = new ItemBLL();
            if (itemBLL.isInsertionSuccessful(itemBO))
            {
                Console.WriteLine("\nInsertion Successfull!");
            }
            else
            {
                Console.WriteLine("\n...OOPS! Insertion Failed!");
            }
        }
        private void updateItem()
        {
            int id;
            Console.Write("Enter Item ID You Want to Update:\t");
            int.TryParse(Console.ReadLine(), out id);
            
            ItemBLL itemBLL = new ItemBLL();
            if (itemBLL.isIDExist(id))
            {
                int qty;
                decimal pr;
                Console.Write("Enter Item Name:\t");
                string nam = Console.ReadLine();
                Console.Write("Enter Item Quantity:\t");
                int.TryParse(Console.ReadLine(), out qty);
                Console.Write("Enter Item Price:\t");
                decimal.TryParse(Console.ReadLine(), out pr);
                
                Console.Write("\nDo you Really Want to Update (Y/N):\t");
                ConsoleKeyInfo confirm = Console.ReadKey();
                if (confirm.KeyChar == 'Y' || confirm.KeyChar == 'y')
                {
                    {
                        ItemBO itemBO = new ItemBO()
                        {
                            ID = id,
                            Name = nam,
                            Quantity = qty,
                            Price = pr
                        };

                        if (itemBLL.isUpdationSuccessful(itemBO))
                        {
                            Console.WriteLine("\nItem Updated Successfull!");
                        }
                        else
                        {
                            Console.WriteLine("\n...OOPS! Updation Failed!");
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("...OOPS! Item Not Exist! OR Invalid ID");
            }

        }

        private void deleteItem()
        {
            int id;
            Console.Write("Enter Item ID You Want to Delete\n");
            int.TryParse(Console.ReadLine(), out id);

            ItemBLL itemBLL = new ItemBLL();
            if (itemBLL.isIDExist(id))
            {
                Console.Write("\nDo you Really Want to Delete (Y/N):\t");
                ConsoleKeyInfo confirm = Console.ReadKey();
                if (confirm.KeyChar == 'Y' || confirm.KeyChar == 'y')
                {
                    if (itemBLL.isDeletionSuccessful(id))
                    {
                        Console.WriteLine("\nItem Deleted Successfull!");
                    }
                    else
                    {
                        Console.WriteLine("\n...OOPS! Deletion Failed!");
                    }
                }
            }
            else
            {
                Console.WriteLine("...OOPS! Item Not Exist! OR Invalid ID");
            }       
        }
        private void printAllSellingRecord()
        {
            Console.WriteLine("\n-----------------------------------------------------------");
            Console.WriteLine("\t\t LIST OF SELLING RECORDS");
            Console.WriteLine("-----------------------------------------------------------");
            Console.WriteLine("{0,8}\t{1,8}\t{2,10:N0}\t{3,10:C}", "CUST. ID", "ITEM ID", "SOLD QTY.", "AMOUNT");

            Console.WriteLine("-----------------------------------------------------------");


            CartBLL cartBll = new CartBLL();
            List<CartBO> cartListOfBO = cartBll.getAllSellingDetails();

            foreach (CartBO i in cartListOfBO)
            {       
                Console.WriteLine("{0,8}\t{1,8}\t{2,10:0}\t{3,10:C}", i.CustomerID, i.ItemID, i.Quantity, i.Amount);
            }
            Console.WriteLine("-----------------------------------------------------------");
        }
        private void printInventorydetails()
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("\t\tITEM LISTS OR DETAILS");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("{0,6}\t{1,10}\t{2,10:C}\t{3,10:N0}", "ITEM ID", "ITEM NAME", "PRICE", "QUANTITY");
            Console.WriteLine("---------------------------------------------------");

            ItemBLL itemBLL = new ItemBLL();
            List<ItemBO> itemListOfBO = itemBLL.getItemsDetails();
            
            foreach(ItemBO i in itemListOfBO)
            { 
                    Console.WriteLine("{0,6}\t{1,10}\t{2,10:C}\t{3,10:N0}", i.ID, i.Name , i.Price, i.Quantity);
            }
            Console.WriteLine("---------------------------------------------------");
        }
    }
}