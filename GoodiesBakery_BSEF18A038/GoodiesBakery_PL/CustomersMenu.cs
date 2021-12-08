using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using GoodiesBakery_BLL;
using GoodiesBakery_BO;
using GoodiesBakery_DAL;
using Microsoft.Data.SqlClient;
namespace GoodiesBakery_PL
{
    public class CustomersMenu
    {
        public void showCustomerMenu()
        { 
            int choice = 1;
            while (choice != 3)
            {
                Console.WriteLine("\n----------------------------------------------");
                Console.WriteLine("\t\tCUSTOMER MENU");
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("1 Show Items List or Details ");
                Console.WriteLine("2 Add Item to Cart");
                Console.WriteLine("3 Back to main menu");
                Console.WriteLine("----------------------------------------------");
                do
                {
                    if (!(choice >= 1 && choice <= 3))
                    {
                        Console.WriteLine("...OOPS!You Enter Wrong Choice!");
                    }
                    Console.Write("Please Enter Customer Menu Choice(1-3):\t");
                    int.TryParse(Console.ReadLine(), out choice);
                } while (!(choice >= 1 && choice <= 3));

                switch (choice)
                {
                    case 1:
                        this.printItemDetails();
                        break;
                    case 2:
                        this.addItemInCartAgainstCustomer();
                        break;
                    case 3:
                        choice = 3;
                        break;
                }
            }
        }
        private void printItemDetails()
        {
            Console.WriteLine("\n---------------------------------------------------");
            Console.WriteLine("\t\tITEM LISTS OR DETAILS");
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("{0,6}\t{1,10}\t{2,10:C}\t{3,10:N0}", "ID", "NAME", "PRICE", "QUANTITY");
            Console.WriteLine("---------------------------------------------------");

            ItemBLL itemBLL = new ItemBLL();
            List<ItemBO> itemListOfBO = itemBLL.getItemsDetails();

            foreach (ItemBO i in itemListOfBO)
            {
                Console.WriteLine("{0,6}\t{1,10}\t{2,10:C}\t{3,10:N0}", i.ID, i.Name, i.Price, i.Quantity);
            }
            Console.WriteLine("---------------------------------------------------");
        }

        private void addItemInCartAgainstCustomer()
        {
            ConsoleKeyInfo choice;
            do
            {
                //Input (ITEM ID) from user
                int itemId;
                Console.Write("\nEnter Item ID:\t");
                int.TryParse(Console.ReadLine(), out itemId);

                //Check is ITEM ID valid or not, If not Valid then display the Error Meaasage
                ItemBLL itemBLL = new ItemBLL();
                if (itemBLL.isIDExist(itemId))
                {
                    //Input QUANTITY from use
                    int qty;
                    Console.Write("Enter Quantity:\t");
                    int.TryParse(Console.ReadLine(), out qty);

                    // Check Quantity is valid or not i.e. If More than avaialble stock then display the Error Meaasage 
                    if (itemBLL.isValidQuantityOfItem(itemId, qty))
                    {
                        //This will customer that is useful later for us
                        CustomerBO custBO = new CustomerBO()
                        {
                            Name = "",
                            PhoneNo = default
                        };
                        CustomerBLL custBLL = new CustomerBLL();
                        CustomerBLL customerBLL = new CustomerBLL();
                        customerBLL.addTheCustomer(custBO);
                        
                        //Get ID of Current Customer from database
                        custBO.CustomerID = customerBLL.getCustomerID(custBO);

                        CartBLL cart = new CartBLL();
                        //This will add (ITEM ID) and (Quantity) in CART table. If insertion successfull then ask for more inputs and generate Bill otherwise give an error
                        if (cart.isItemAddedToCart(custBO.CustomerID, itemId, qty))
                        {
                            Console.WriteLine("\nItem Added Successfull!");
                            Console.Write("\nDo Want to Add More Items To Cart? (y/n):\t");
                            choice = Console.ReadKey();

                            if (char.ToLower(choice.KeyChar) !='y')
                            {
                                generateBillAgainstCustomer(custBO);
                                return;
                            }
                        }
                        else
                        {
                            Console.WriteLine("...OOPS! Insertin Failed!");
                        }

                    }
                    else
                    {
                        Console.WriteLine("...OOPS! Required Quantity is not available in Stock! You may try less quantity");
                    }
                }
                else
                {
                    Console.WriteLine("...OOPS! Item Not Exist! OR Invalid ID");
                }
            } while (choice.KeyChar == 'Y' || choice.KeyChar == 'y');
        }
        private void generateBillAgainstCustomer(CustomerBO custBO)
        {
            Console.WriteLine("\n----------------------------------------------");
            Console.WriteLine("\t\tGENERATING BILL");
            Console.WriteLine("----------------------------------------------");
            string custName;
            decimal phoneNo;
            //Input customer name untill if it's not correct 
            do
            {
                Console.Write("\nEnter Customer Name:*\t");
                custName = Console.ReadLine();
            } while (custName=="" || custName == default);

            //Input Phone No untill if it's not correct 
            do
            {
                Console.Write("Enter Phone Number:*\t");
                decimal.TryParse(Console.ReadLine(), out phoneNo);
            } while (phoneNo == default);

            //Update the customer in database
            custBO.Name = custName;
            custBO.PhoneNo = phoneNo;
            CustomerBLL custBLL = new CustomerBLL();
            custBLL.updateTheCustomer(custBO);

            Console.WriteLine("----------------------------------------------");
            CartBLL cartBLL = new CartBLL();
            Console.WriteLine("YOUR TOTAL BILL: {0,30:C}", cartBLL.getBillAgainstCustomer(custBO.CustomerID));
            
            printInvoice(custBO);
        }
        private void printInvoice(CustomerBO custBO)
        {
            Console.WriteLine("\n------------------------------------------------------------------------");
            Console.WriteLine("\t\t\tYOUR INVOICE");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("CUST. ID: {0} {1,55}", custBO.CustomerID, "CUST. NAME: "+custBO.Name);
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("{0,6}\t{1,10}\t{2,10:C}\t{3,10:N0}\t{4,10:N0}", "ITEM ID", "ITEM NAME", "PRICE/Item", "QUANTITY", "AMOUNT");

            CartBLL cartBLL = new CartBLL();
            List<CartBO> cartListOfBO = cartBLL.getInvoice(custBO.CustomerID);

            ItemBLL itemBLL = new ItemBLL();
            foreach (CartBO i in cartListOfBO)
            {
                string itemName = itemBLL.getItemName(i.ItemID);
                Console.WriteLine("{0,6}\t{1,10}\t{2,10:C}\t{3,10:N0}\t{4,10:N0}", i.ItemID, itemName, i.PricePerItem, i.Quantity, i.Amount);
            }
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("YOUR Paid Amount: {0,50:C}", cartBLL.getBillAgainstCustomer(custBO.CustomerID));
        }
    }
}
