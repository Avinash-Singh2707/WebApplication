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
      
        static void Main(string[] args)
        {
           

            bool flag = true;

            while (flag)
            {
                Console.WriteLine("============================================================================");
                Console.WriteLine("\t\tWelcome To Indian Railway Reservation System");
                Console.WriteLine("============================================================================");

                Console.WriteLine("\t1. Admin -> Press 1");
                Console.WriteLine("\t2. User -> Press 2");
                Console.WriteLine("\t3. Exit -> Press 3");
                Console.Write("YOUR CHOICE: ");
                try
                {
                    int n = int.Parse(Console.ReadLine());
                    switch (n)
                    {
                        case 1:
                            {
                                Console.Clear();
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

                               
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Thank You For Your Time....");
                                flag = false;
                                break;
                            }
                        default:
                            Console.WriteLine("Enter Valid Number");
                            break;
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            
            }
            
                





          Console.Read();
        }

    }
}
