using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using RailwaySystem.BusinessLayer.AdminLayer;

namespace RailwaySystem.BusinessLayer.UserLayer 
{
    class User_func
    {
        static int fare = 0;
        static int trNo;
        static RailDBEntities Rb = new RailDBEntities();
        static string cls;
        static int notic;
        static int uid;
        //public static object ct;             // defining object ct(class-type)
        
        public static void UserLogin()
        {
            
            user_details us = new user_details();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("\t1. Existing User -> press 1");
            Console.WriteLine("\t2. New User -> Press 2");
            Console.Write("YOUR CHOICE: "); 
            int n = int.Parse(Console.ReadLine());
            Console.WriteLine("-------------------------------------------------");
            if (n == 1)
            {
                ValidateUser();
            }
            else if (n == 2)
            {
                Console.WriteLine("Enter Your UserId, UserName and Password: ");
                uid = int.Parse(Console.ReadLine());  
                us.user_id = uid;
                us.user_name = Console.ReadLine();
               
                us.Password = Admin_func.ReadPassword();

                Rb.user_details.Add(us);
                Rb.SaveChanges();
              

            }
            else
            {
                Console.WriteLine("Invalid Options");
                UserLogin();
            }
            
           
        }
        static void ValidateUser()
        {
            Console.Write("Enter UserId: ");
            uid = int.Parse(Console.ReadLine());
            Console.Write("Enter Password: ");
            string pass = Admin_func.ReadPassword();
            Console.WriteLine();
            bool vl= validate(uid,pass);
            if (vl)
            {
               
                UserOptions();
            }
            else
            {
                Console.WriteLine("============================================================================");
                Console.WriteLine("\tInvalid UserId or Password\n \t\tTRY AGAIN.....");
                Console.WriteLine("============================================================================");

                UserLogin();

            }
        }
        private static bool validate(int uid, string pass)
        {
            var user = Rb.user_details.FirstOrDefault(x => x.user_id == uid && x.Password == pass);
            return user != null;
        }
        public static void UserOptions()
        {
     
            bool flag = true;

            while (flag)
            {

                Console.WriteLine("============================================================================");
                Console.WriteLine("\t\tWelcome To User Portal");
                Console.WriteLine("============================================================================");
                Console.WriteLine("\t1. Book Tickets -> Press 1");
                Console.WriteLine("\t2. Cancel Ticket -> Press 2");
                Console.WriteLine("\t3. Show Booking Details -> Press 3");
                Console.WriteLine("\t4. Show Cancellation Details -> Press 4");
                Console.WriteLine("\t5. Exit");
                Console.Write("YOUR CHOICE: ");
                int n = int.Parse(Console.ReadLine());
                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("-------------------------------------------------");
                            BookTicket(uid);
                            Console.WriteLine("-------------------------------------------------");
                          
                            UserOptions();
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("-------------------------------------------------");
                            ShowBookedTicket(uid);
                            CanceledTicket();
                            Console.WriteLine("-------------------------------------------------");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("-------------------------------------------------");
                            ShowBookedTicket(uid);
                            Console.WriteLine("-------------------------------------------------");
                            break;
                        }
                    case 4:
                        {
                            Console.WriteLine("-------------------------------------------------");

                            CanceledTicket(uid);
                            UserOptions();
                            Console.WriteLine("-------------------------------------------------");
                            break;
                        }
                    case 5:
                        {
                            flag = false;
                            Console.WriteLine("<<<---Thank You--->>>");
                            break;
                        }
                    default:
                        Console.WriteLine("Enter the Valid Options from above");
                       
                        break;
                }

            }
        }


        public static void ShowTrain()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("\t\t---Train Details---");
            Console.WriteLine("-------------------------------------------------");
            var trains = Rb.trains.Where(t => t.isActive == "Active").ToList(); 
            int ct = 1;
            Console.WriteLine($"->\tTrain-No\t\tTrain-Name\t\tSource\t\tDestination");
            foreach (var train in trains)
            {
                Console.WriteLine($"{ct}\t{train.train_no}\t\t\t{train.train_name}\t\t{train.source}\t{train.destination}");
                ct++;
                
            }
            Console.WriteLine("-------------------------------------------------");
        }
        public static void BookTicket(int uid)    
        {

            class_Avail ca = new class_Avail();
            fare f = new fare();
            bookTicket bt = new bookTicket();
            user_details us = new user_details();
            ShowTrain();

            showSeatNCalPrice();
            Console.WriteLine("Press Y to continue and N to exits: ");
            char res = char.Parse(Console.ReadLine());
            if (res == 'Y' || res=='y')
            {
                Console.WriteLine("Please wait for some time....");
                
                Thread.Sleep(5000);
                Console.WriteLine("We are Redirecting to you Booking Details......\n");
                Thread.Sleep(2000);
                Random r = new Random();
                int bookid = r.Next(11111, 99999);      // for generating random number....
                bt.Book_Id = bookid;
                bt.Booking_Date_Time = DateTime.Now;
             
                bt.train_no = trNo;
                bt.Class_type = cls;
                bt.NumberTickets = notic;
                bt.total_fare = fare;
                bt.user_id = uid;
                bt.Status = "Booked";

                Rb.bookTickets.Add(bt);

                Rb.SaveChanges();

                Rb.UpdateBookedTicket(trNo, cls, notic);   // call procedure to update the seats...
                Console.WriteLine("============================================================================");
                Console.WriteLine("\t\t<<--Your Booking Details->>");
                Console.WriteLine("============================================================================");
                Console.WriteLine("------------------------------------------------------------------------------------------------");
                Console.WriteLine($"User ID: {uid} Book ID: {bookid} Booking Date Time: {bt.Booking_Date_Time} Train Number: {trNo}");
                Console.WriteLine($"Class: {cls} Number of Tickets: {notic} Total Fare: {fare} Status: Booked");
                Console.WriteLine("-------------------------------------------------------------------------------------------------");



            }
            else if(res =='N' || res == 'n')
            {
                Environment.Exit(0);
            }



           

           
        }
        public static void showSeatNCalPrice()
        {
            
            bookTicket bt = new bookTicket();
            hey:
            Console.Write("Enter Train Number You want to Book:  ");
            trNo = int.Parse(Console.ReadLine());

         
            var trSeat = Rb.class_Avail.FirstOrDefault(t=>t.train_no==trNo);   
            var seatPrice = Rb.fares.FirstOrDefault(t => t.train_no == trNo);
            var trSeat1 = Rb.trains.Where( t=>t.train_no==trNo && t.isActive=="Active").FirstOrDefault();
          
            if (trSeat1!=null)
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("\tClass With fares");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine($"\tYou Selected Train Number: {trSeat.train_no}\n");
                Console.WriteLine("\tClasses    : Seat Available    : Fare         ");
                Console.WriteLine($"\t1 AC       : {trSeat.C1_AC_seat_Avail}               : {seatPrice.C1_AC_fare}");
                Console.WriteLine($"\t2 AC       : {trSeat.C2_AC_seat_Avail}               : {seatPrice.C2_AC_fare}");
                Console.WriteLine($"\tSleeper    : {trSeat.SL_seat_Avail}               : {seatPrice.SL_fare}");
                Console.WriteLine("\t---------Select Class----------");
                Console.WriteLine("\tType 1AC for 1AC\n\tType 2AC for 2AC\n\tType SL for Sleeper");

               

            }
            else
            {
                Console.WriteLine("No Train Found.. Please check the train Number");
                goto hey;

            }
            // Calculate Fares....

            Console.Write("Type Class: ");
            cls = Console.ReadLine();
            start:           // Defining Goto level
            Console.Write("Enter the Number of Tickets you want to book(Max 5): ");
            notic = int.Parse(Console.ReadLine());          // number of seats...
            if (notic > 5)
            {
                Console.WriteLine("You can book maximum 5 tickets");
                goto start;
            }
            else
            {
                if (cls == "1AC".ToUpper())
                {
                    fare = (int)(notic * seatPrice.C1_AC_fare);
                }
                else if (cls == "2AC".ToUpper())
                {
                    fare = ((int)(notic * seatPrice.C2_AC_fare));
                }
                else if (cls == "SL".ToUpper())
                {
                    fare = (int)(notic * seatPrice.SL_fare);
                }


                Console.WriteLine("Your Total Fare is: " + fare);
            }
           

        }

        public static void CanceledTicket()
        {
            cancelTicket ct = new cancelTicket();
            bookTicket bt = new bookTicket();
            Console.WriteLine("Enter Your Book ID to cancel: ");
            int bid = int.Parse(Console.ReadLine());
            var x=Rb.bookTickets.Find(bid);
            if (x != null)
            {

                ct.Book_Id = x.Book_Id;
                Random r = new Random();
                int cid = r.Next(11111, 99999);
                ct.Cancel_Id = cid;
                ct.user_id = x.user_id;
                ct.train_no = x.train_no;
                ct.Cancelled_date_time = DateTime.Now;
                
                ct.Refund_Amount = (int)x.total_fare/2;            // apply 50% deductionn....


                Console.WriteLine($"Your Refunded Amount will be: {ct.Refund_Amount} After 50% Deduction" );
                Console.WriteLine("Press Y to Continue and N to exit");
                char res = char.Parse(Console.ReadLine());
                if (res == 'Y' || res == 'y')
                {
                    Rb.cancelTickets.Add(ct);          

                    x.Status = "Cancelled";          // here I am changing status of booking details...
                    //calling stored procedure for updating seats
                    var trno = (int)x.train_no;
                    string classtype = x.Class_type;
                    int nutic = x.NumberTickets;
                    Rb.UpdateCancelTicket(trno, classtype, nutic);


                    Rb.SaveChanges();
                    Console.WriteLine("\t\tYour Ticket has been Cancelled");
                    Console.WriteLine("============================================================================");
                    Console.WriteLine("\t\t<<--Your Cancellation Details->>");
                    Console.WriteLine("============================================================================");
                    Console.WriteLine("------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"User ID: {uid} Cancel ID: {cid} Cancelled Date Time: {ct.Cancelled_date_time} Train Number: {x.train_no}");
                    Console.WriteLine($"Class: {x.Class_type} Number of Tickets: {x.NumberTickets} Total Fare: {ct.Refund_Amount} Status: Cancelled");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------");


                }
                else if(res=='N' || res == 'n')
                {
                    Environment.Exit(0);
                }

               

            }
            else
            {
                Console.WriteLine("No Book ID Found....");
            }
        }      


        static void ShowBookedTicket(int uid)
        {
            var booked_tkt = Rb.bookTickets.Where(bt => bt.user_id == uid);
         
            if (booked_tkt.Any())            //The Any() method checks if there are any matching booked tickets.  NOT Working->if(booked_tkt!=null)
            {
                foreach (var bt in booked_tkt)
                {
                    Console.WriteLine("\n-----------------------------------------------------------------------------------------------");
                    Console.WriteLine($"Book ID: {bt.Book_Id}\t\tTrain No: {bt.train_no}\t\tBooking Date&Time :{bt.Booking_Date_Time}\n" +
                        $"Source: {bt.train.source}\tDestination: {bt.train.destination}\nTotal Fare: {bt.total_fare}\t Status: {bt.Status}");
                    Console.WriteLine("-------------------------------------------------------------------------------------------------\n");
                }
            }
            else
            {
                Console.WriteLine("No Booking Details Found");
            }
        }

       
        static void CanceledTicket(int uid)
        {
            var cancel_tkt = Rb.cancelTickets.Where(bt => bt.user_id == uid);
            //var cancel_tkt = Rb.cancelTickets.Find(uid);
            if (cancel_tkt.Any())
            {
                foreach (var bt in cancel_tkt)
                {
                    Console.WriteLine("\n---------------------------------------------------------------------------------------------------");
                    Console.WriteLine($"Cancel ID: {bt.Cancel_Id}\tTrain No: {bt.train_no}\t\tBooking Date&Time :{bt.Cancelled_date_time}\n" +
                        $"Source: {bt.train.source}\tDestination: {bt.train.destination}\nRefund Amount: {bt.Refund_Amount}");
                    Console.WriteLine("------------------------------------------------------------------------------------------------------\n");
                }
            }
            else
            {
                Console.WriteLine("No Cancellation Details Found");
            }
        }

        ////Passenger Details
        //public static void PassDet()
        //{

        //}
    }
}
