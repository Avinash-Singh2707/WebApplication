using RailwaySystem.BusinessLayer.UserLayer;
using RailwaySystem.BusinessLayer.AdminLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace RailwaySystem
{
    class Program
    {
        //static RailDBEntities Rb = new RailDBEntities();     // object of data context 
        static void Main(string[] args)
        {
            Console.WriteLine("============================================================================");
            Console.WriteLine("\t\tWelcome To Indian Railway Reservation System");
            Console.WriteLine("============================================================================");

            Console.WriteLine("\t1. Admin -> Press 1");
            Console.WriteLine("\t2. User -> Press 2");
            Console.WriteLine("\t3. Exit -> Press 3");
            Console.Write("YOUR CHOICE: ");
            int n = int.Parse(Console.ReadLine());
            switch (n) 
            {
                case 1:
                    {
                        Console.WriteLine("-------------------------------------------------");
                        Console.WriteLine("Enter Your Admin Crediantials..... ");
                        Admin_func.ValidateAdmin();
                        Console.WriteLine("-------------------------------------------------");
                        break;
                    }
                case 2:
                    {
                        Console.WriteLine("Enter Your User Details:");
                        User_func.UserLogin();

                        //Console.WriteLine("\tRegistration Done Successfully");


                        User_func.UserOptions();
                        break;
                    }
                case 3:
                    {
                        Console.WriteLine("Thank You For Your Time....");
                        break;
                    }
                default:
                    Console.WriteLine("Enter Valid Number");
                    break;
            }
            
                





          Console.Read();
        }

    }
}
