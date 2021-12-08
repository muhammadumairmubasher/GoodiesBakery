using System;
using GoodiesBakery_PL;
namespace GoodiesBakery
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*************** WELCOME TO GOODIES BAKERY ***************");
            App app = new App();
            app.mainMenu();
        }
    }
}
