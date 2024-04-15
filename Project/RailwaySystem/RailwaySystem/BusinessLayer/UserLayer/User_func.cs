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
        public static object ct;             // defining object ct(class-type)
        
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
                uid = int.Parse(Console.ReadLine());  //?
                us.user_id = uid;
                us.user_name = Console.ReadLine();
                //us.Password = Console.ReadLine();
                us.Password = Admin_func.ReadPassword();

                Rb.user_details.Add(us);
                Rb.SaveChanges();
                //Console.WriteLine("Hey"+us.user_id);

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
            //string pass = Console.ReadLine();
            string pass = Admin_func.ReadPassword();
            Console.WriteLine();
            bool vl= validate(uid,pass);
            if (vl)
            {
                //Console.WriteLine();
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
            //Console.WriteLine("1. Show all Train Press 1");
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
            bool flag = true;
            //while (flag)
            //{
                switch (n)
                {
                    case 1:
                        {
                            Console.WriteLine("-------------------------------------------------");
                            BookTicket(uid);
                            Console.WriteLine("-------------------------------------------------");
                            //BookTicket(newUserId);
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
                            //flag = false;
                            break;
                        }
                    default:
                        Console.WriteLine("Enter the Valid Options from above");
                        //UserOptions();
                        break;
                }
            
            
        }


        public static void ShowTrain()
        {
            Console.WriteLine();
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("\t\t---Train Details---");
            Console.WriteLine("-------------------------------------------------");
            var trains = Rb.trains.Where(t => t.isActive == "Active").ToList(); 
            //var trains = Rb.trains.ToList();
            int ct = 1;
            Console.WriteLine($"->\tTrain-No\t\tTrain-Name\t\tSource\t\tDestination");
            foreach (var train in trains)
            {
                Console.WriteLine($"{ct}\t{train.train_no}\t\t\t{train.train_name}\t\t{train.source}\t{train.destination}");
                ct++;
                
            }
            Console.WriteLine("-------------------------------------------------");
        }
        public static void BookTicket(int uid)    // we can add status booked or cancelled....????
        {

            class_Avail ca = new class_Avail();
            fare f = new fare();
            bookTicket bt = new bookTicket();
            user_details us = new user_details();
            //Console.WriteLine(us.user_id);
            ShowTrain();

            showSeatNCalPrice();
            Console.WriteLine("Press Y to continue and N to exits: ");
            char res = char.Parse(Console.ReadLine());
            if (res == 'Y' || res=='y')
            {
                Console.WriteLine("Please wait for some time....");
                //Task.Delay()   
                Thread.Sleep(5000);
                Console.WriteLine("We are Redirecting to you Booking Details......\n");
                Thread.Sleep(2000);
                Random r = new Random();
                int bookid = r.Next(11111, 99999);      // for generating random number....
                bt.Book_Id = bookid;
                bt.Booking_Date_Time = DateTime.Now;
                //bt.user_id = us.user_id;
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
                Console.WriteLine($"User ID: {uid} Book IDL: {bookid} Booking Date Time: {bt.Booking_Date_Time} Train Number: {trNo}");
                Console.WriteLine($"Class: {cls} Number of Tickets: {notic} Total Fare: {fare} Status: Booked");
                Console.WriteLine("-------------------------------------------------------------------------------------------------");



            }
            else if(res =='N' || res == 'n')
            {
                Environment.Exit(0);
            }



           

            //bt.user_id = Rb.user_details.Where(u=>u.user_id==UriIdnScope)     // here...   user-id = null

            //bt.user_id = Rb.user_details
            //bt.user_id = Rb.user_details.Find(us.user_id);
            //bt.user_id = Rb.user_details.Where(u => u.user_id = u.user_id).ToList();

            //List<user_details> user_details = Rb.user_details.Where(u => u.user_id == u.user_id).ToList();
            //string userNamefromDB = user_details.ToString();
            //bt.user_id = userNamefromDB;

                 // ERROR


            //Console.WriteLine("Enter class and Number of tickets You want to book: ");
            //string cls = Console.ReadLine();
            //int notic = int.Parse(Console.ReadLine());
            //if(cls=="1AC")
            //{

            //}
            //bt.total_fare = notic;
            //Rb.bookTickets.Add(bt);



            //Rb.UpdateBookedTicket(trNo,cls,notic);   // calling the stored procedure.....
            //Rb.SaveChanges();




            // call proc
            //
           
           
            //Task.Delay(4000);      // then show msg generating ur tickets.. then show the tickets...

        }
        public static void showSeatNCalPrice()
        {
            //int fare=0;
            bookTicket bt = new bookTicket();
            Console.Write("Enter Train Number You want to Book:  ");
            trNo = int.Parse(Console.ReadLine());

            //var trSeat = Rb.class_Avail.Where(t => t.train_no == trNo);
            var trSeat = Rb.class_Avail.FirstOrDefault(t=>t.train_no==trNo);    //??? why find not work....
            var seatPrice = Rb.fares.FirstOrDefault(t => t.train_no == trNo);

            if (trSeat != null)
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

                //Console.WriteLine("")
                //foreach (var v in trSeat)
                //{
                //    Console.WriteLine(v.train_no + " " + v.C1_AC_seat_Avail);
                //}

            }
            else
            {
                Console.WriteLine("No Train Found.. Please check the train Number");
            }
            // Calculate Fares....

            Console.Write("Type Class: ");
            cls = Console.ReadLine();
            Console.Write("Enter the Number of Tickets you want to book: ");
            notic = int.Parse(Console.ReadLine());          // number of seats...
            if (cls == "1AC".ToUpper())
            {
                fare = (int)(notic * seatPrice.C1_AC_fare);
            }
            else if (cls == "2AC".ToUpper())
            {
                fare = ((int)(notic * seatPrice.C2_AC_fare));
            }
            else if(cls=="SL".ToUpper())
            {
                fare = (int)(notic * seatPrice.SL_fare);
            }
            
            
            Console.WriteLine("Your Total Fare is: "+ fare);
            //Console.WriteLine("Press Y to continue and N to exits");
            

            
            //Rb.bookTickets.Add(bt);
           
            
            //Console.WriteLine("Your Ticked has been Booked");
            //Rb.SaveChanges();
            //Rb.bookTickets.Add(bt);



            // Rb.UpdateBookedTicket(trNo, cls, notic);    // call proc

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
                    Rb.cancelTickets.Add(ct);           // bookId(fk in cancel ticket) is showing null as u remove primary key from booktable,

                    x.Status = "Cancelled";          // here I am changing status of booking details...
                    //calling stored procedure for updating seats
                    var trno = (int)x.train_no;
                    string classtype = x.Class_type;
                    int nutic = x.NumberTickets;
                    Rb.UpdateCancelTicket(trno, classtype, nutic);


                    //Rb.SaveChanges();
                    Rb.SaveChanges();
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

        //public static void ShowBookedTickets()
        //{
        //    Console.WriteLine("Enter Your book-Id: ");
        //    int bookid = int.Parse(Console.ReadLine());
        //    var bt = Rb.bookTickets.SingleOrDefault(b => b.Book_Id==bookid);       // Find not work....
        //    if (bt != null)
        //    {
        //        Console.WriteLine("Your Booking Details: ");
        //        Console.WriteLine($"Book ID: {bt.Book_Id} Train Number: {bt.train_no} Booking-Date-Time: {bt.Booking_Date_Time} Class_Type: " +
        //            $"{bt.Class_type} Number of Tickets: {bt.NumberTickets} Total Fare: {bt.total_fare}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("No Book ID found");
        //    }


        //}

        static void ShowBookedTicket(int uid)
        {
            var booked_tkt = Rb.bookTickets.Where(bt => bt.user_id == uid);
            //var booked_tkt = Rb.bookTickets.Find(uid);
            if (booked_tkt!=null)            //The Any() method checks if there are any matching booked tickets.  NOT Working->if(booked_tkt!=null)
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

        //public static void ShowcCancelTickets()
        //{
        //    Console.WriteLine("Enter Your Cancel-Id: ");
        //    int cd = int.Parse(Console.ReadLine());
        //    var cid = Rb.cancelTickets.SingleOrDefault(c=>c.Cancel_Id==cd);       // Find not work....
        //    if (cid != null)
        //    {
        //        Console.WriteLine("Your Cancellation Details: ");
        //        // here we are just doing inner join of booktickets and cancel tickets..

        //        var query = from bookTicket in Rb.bookTickets
        //                    join cancelTicket in Rb.cancelTickets
        //                    on bookTicket.user_id equals cancelTicket.user_id
        //                    select new
        //                    {
        //                        ct = bookTicket.Class_type,             // here we are just retrieving the properties...
        //                        nt = bookTicket.NumberTickets,


        //                        //CustomerName = customer.Name,
        //                        //OrderId = order.OrderId,
        //                        //OrderDate = order.OrderDate
        //                    };

        //        var res = query.ToList();
        //        Console.WriteLine($"Class Type: {ct}");
        //        //Console.WriteLine($"Book ID: {bt.Book_Id} Train Number: {bt.train_no} Booking-Date-Time: {bt.Booking_Date_Time} Class_Type: " +
        //        //    $"{bt.Class_type} Number of Tickets: {bt.NumberTickets} Total Fare: {bt.total_fare}");
        //    }
        //    else
        //    {
        //        Console.WriteLine("No Book ID found");
        //    }
        //}

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
    }
}
