using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.ResturantReservation
{
    //Core objects
    //Resturant   resturant has List<Table>, list of cutomers, list of employee, list of party, meal et
    //Customer
    //Table
    //Server       - serves a particula table at a time
    //Employee
    //Party          - party should have array of guest - it can occupy one or more tables
    //order
    //Meal
    //Food

    //Reservation
    //Request


    public class Resturant
    {
        //time of operation
        //Handle request and place reservation
        //singletom class
        public Resturant()
        {
        }

        private static Resturant _restuarnt = null;

        public static Resturant Getinstance()
        {
             if(_restuarnt == null)
             {
                 return new Resturant();
             }
             else
             {
                 return _restuarnt;

             }

        }


        public List<Table> tables = new List<Table>();
        public List<Reservation> booking { get; set; }
        //key tableid..value bookingid
        public Dictionary<int, int> _reservedbooking = new Dictionary<int, int>();
        public Queue<Request> waitingcutomers = new Queue<Request>();
        public Queue<Waiters> availablewaiters = new Queue<Waiters>();


    }
    public class Reservation
    {

        public int ReservationID { get; set; }
        public int OrderID { get; set; }
        public List<Table> _bookedtable { get; set; }
        public int NoofPeople { get; set; }
        public DateTime BookedTime { get; set; }
        public DateTime Checkin { get; set; }
        public DateTime Checkout { get; set; }
     
        public void AddReservation(Request request)
        {

        }

        public void AddToWaitingList()
        {
        }

        public void RemoveReservation()
        {
        }

        public bool IsTableAvailable(int countofperson, DateTime CheckIntime, DateTime Checkout)
        {

            // Look throught all the table we have
                //check if there are any booking for the given time..if not reserver it

            //nothing found put the request on waiting list

           
            return true;
        }


    }
   
    public class Request : Customer
    {
        public DateTime CheckIN { get; set; }
        public DateTime Checkout { get; set; }
        public int NoofPeople { get; set; }
        public int Type { get; set; }  //enum booth, regualr table   (smoking zone, pool side, garden side etc
    }

    public class ToGoRequest : Customer
    {
        public DateTime PickUpTime { get; set; }
        List<Order> orders;
    }


    public class Waiters
    {
        public int WaiterID { get; set; }
    }


    public class Table
    {
        public Table()
        {
        }
        public int ID { get; set; }
        public int SeatCount { get; set; }
        public int Type { get; set; }  //enum booth, regualr table   (smoking zone, pool side, garden side etc
        public int AssignedToWaiter { get; set; }

        public List<Reservation> GetAllBooking(Table table, DateTime dt)
        {
            return new List<Reservation>();
        }
    }


    public class Order
    {
        public int id { get; set; }
        public int customerid { get; set; }
    }

    public class OrderLineItem
    {
        //itemid, quant, total price
    }

    public class Food
    {
        //id, des, price
    }
    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ContactNumber { get; set; }
    }
}
