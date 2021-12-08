using System;
namespace GoodiesBakery_PL
{
    public class App
    {
        public void mainMenu()
        {
            int choice = 1;
            while (choice != 3)
            {
                Console.WriteLine("\n**********************************************");
                Console.WriteLine("\t\tMAIN MENU");
                Console.WriteLine("**********************************************");
                Console.WriteLine("1 Admin");
                Console.WriteLine("2 Customer");
                Console.WriteLine("3 Exit");
                Console.WriteLine("**********************************************");
                //Get Input from user until choice is not betweeen 1 and 3
                do
                {
                    if (choice < 1 || choice > 3)
                    {
                        Console.WriteLine("...OOPS!You Enter Wrong Choice!");
                    }
                    Console.Write("\nPlease Enter Your Choice(1-3):\t");
                    int.TryParse(Console.ReadLine(), out choice);
                } while (!(choice >= 1 && choice <= 3));    //Not between 1 & 3

                switch (choice)
                {
                    case 1:
                        //Module-01
                        AdminMenu adminMenu = new AdminMenu();
                        adminMenu.login();
                        break;
                    case 2:
                        //Module-02
                        CustomersMenu customersMenu = new CustomersMenu();
                        customersMenu.showCustomerMenu();
                        break;
                    case 3:
                        choice = 3;
                        break;
                }
            }
        }

    }
}