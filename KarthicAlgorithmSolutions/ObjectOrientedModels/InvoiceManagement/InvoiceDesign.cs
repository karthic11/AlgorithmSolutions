using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puzzles.ObjectOrientedModels.InvoiceManagement
{
    public class InvoiceDesign
    {

    }


    //Let me write all the objects that are required for this application and its realationshion
    //1)Client  
    //2)Item - This is a  Base class. Product and Service are derived classes which inherit Item class
    //3)InvoiceLineItem  - Contains the info for the lineitem such as price, quantity etc 
    //4)Invoice   - Each invoice contain one to many invoicelineitems 1:many relationship
    //5)InvoiceService/InvoiceManager  - Manages the db call and returns the business object
    //6)LatePayment - This is a derived class which inherits base class Invoice 
    //7)IFormatter - Interface has method and properties for formatting
    //8)InvoiceFormatter - This class implements Iformatter and provides implementation of the IFormatter
    //9)Order - Each order contain one to many order line item
    //10)OrderLineItem - Line items for order
    //11)OrderInvoiceMap   - Each order can have many invoices and each invoice can contain many orders

    //Enum
    //InvoiceStatus and OrderStatus

    public class Client
    {
        //Properties
        public int ClientID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string Email { get; set; }
        //constructor
        public Client()
        {
        }
        //Methods
        public int GetClientZipCode()
        {
            return ZipCode;
        }
        public string GetClientAddress()
        {
            return Address;
        }
    }

    public class Item
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double UnitPrice { get; set; }
        //Constructor
        public Item()
        {
        }
        //Method
        public double GetPrice()
        {
            return UnitPrice;
        }
        public string GetDecription()
        {
            return Description;
        }
    }

    //Product Inherits from Item Class
    public class Product : Item
    {
        public Product()
        {
        }

    }

    public class Service : Item
    {
    }

    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        //Constuctor
        public InvoiceLineItem()
        {
        }
        //Methods
        public double GetTotalPrice()
        {
            this.TotalPrice = Item.GetPrice() * Quantity;
            return TotalPrice;
        }
    }






    public class Invoice
    {
        public int InvoiceID { get; set; }
        public int ClientID { get; set; }
        public List<InvoiceLineItem> lineitems { get; set; }
        public double TotalSalesTaxAmount { get; set; }
        public double TotalAmountDue { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CreatedDate { get; set; }
        private InvoiceService _Invoiceservice = null;
        public IFormatter formatter = null;
        public InvoiceStatus Status { get; set; }
        //Constructor
        public Invoice(IFormatter invoiceformatter)
        {
            lineitems = new List<InvoiceLineItem>();
            _Invoiceservice = InvoiceService.GetInstance();     
            formatter = invoiceformatter;
 
        }
        //Method 


        public void SendInvoiceToClient()
        {
            //Generate Invoice for all client
            CalculateAmountDue();
            //format the content of the invoice
            FormatInvoiceContent();
            //print the invoice
            //print the mailinglabel

        }
        //This is a virtual method and it is overridden in the PaymentLate derived class
        public virtual void CalculateAmountDue()
        {
            //Calculate Total Amount Due
            double totalamount = 0;
            double salestaxrate = GetSaleTaxRateByClientID(ClientID);
            double salestaxamount = 0;
            foreach (InvoiceLineItem item in this.lineitems)
            {
                if (item.Item is Service)
                {
                    totalamount += item.GetTotalPrice();
                }
                else if (item.Item is Product)
                {
                    totalamount = item.GetTotalPrice() + (item.GetTotalPrice() * salestaxrate);
                    salestaxamount = (item.GetTotalPrice() * salestaxrate);
                }
            }


            this.TotalAmountDue = totalamount;
            this.TotalSalesTaxAmount = salestaxamount;
        }


        public double GetSaleTaxRateByClientID(int clientid)
        {
            Client client = _Invoiceservice.GetClientInfoById(clientid);
            SaleTaxInfo info = _Invoiceservice.GetSalesTaxByZipCode(client.GetClientZipCode());
            return info.SalesTaxPercentage;
        }

        //This function format's the Header, body and footer of invoice with the data
        public String FormatInvoiceContent()
        {
            Client client = _Invoiceservice.GetClientInfoById(this.ClientID);
            String text = this.formatter.formatHeader(client);

            foreach (var lineitem in this.lineitems)
            {
                text += formatter.formatLineItem(lineitem);
            }
            return text;
        }
        //Methods to Add item,remove item and search item
 
        public void AddLineItem(InvoiceLineItem item)
        {
            lineitems.Add(item);
        }

        public void RemoveItem(InvoiceLineItem item)
        {
            lineitems.Remove(item);  
        }

        public InvoiceLineItem Search(int invoicelineitemid)
        {
            foreach (var item in lineitems)
            {
                if (item.InvoiceLineItemId == invoicelineitemid)
                {
                    return item;
                }
            }

            return null;
        }

    
    
    }

    public class InvoiceService
    {

        public static Dictionary<int, SaleTaxInfo> SalesTaxZipCodeMap = null;

        public InvoiceService()
        {
            SalesTaxZipCodeMap = new Dictionary<int, SaleTaxInfo>(); //Populate from database
        }

        private static InvoiceService _instance = null;

        public static InvoiceService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InvoiceService();
            }

            return _instance;
        }


        public void SendInvoiceToClients()
        {
            //Get all client from database
            List<Client> clients = new List<Client>();

            foreach (var client in clients)
            {
                //Get all open invoice for the client
                //We need to query the db in InvoiceOrderMap and get all the invoices who order.Status is open.
                //This is done to avoid generating invoice for canceled orders
                List<Invoice> invoices = new List<Invoice>();
                foreach (Invoice invoice in invoices)
                {
                    //Generate all the invoice
                    invoice.SendInvoiceToClient();
                }

            }
        }
        public SaleTaxInfo GetSalesTaxByZipCode(int zipcode)
        {
            //SalesTaxZipCodeMap dictionary will be populated from the database so that if the government changes the tax rule for any state or zipcode
            //the application shouldn't have any problem with that
            return SalesTaxZipCodeMap[zipcode];
        }

        public Client GetClientInfoById(int clientid)
        {
            Client client = new Client();
            //query the database and Populate the Client object
            return client;
        }

        public Item GetItemByID(int itemid)
        {
            Item item = new Item();
            //query the database and Populate the Item object
            return item;
        }

    }



    public class LatePayment : Invoice
    {

        public double LatePaymentFee { get; set; }
        public DateTime NextPromiseDate { get; set; }
        public LatePayment(IFormatter formatter)
            : base(formatter)
        {

        }


        public override void CalculateAmountDue()
        {
            this.LatePaymentFee = 0;
            //use business logic to calcualte the late fee from total fee and due date 
            base.CalculateAmountDue();
            this.TotalAmountDue = this.TotalAmountDue + this.LatePaymentFee;
        }
    }

    public class SaleTaxInfo
    {
        public int ZipCode { get; set; }
        public double SalesTaxPercentage { get; set; }
        
      
    }


    public interface IFormatter
    {
        /**
        Formats the header of the invoice.
          @return the invoice header
       */
        String formatHeader(Client client);

        /**
             Formats a line item of the invoice.
            @return the formatted line item
         */
        String formatLineItem(InvoiceLineItem item);
        /**
           Formats the footer of the invoice.
           @return the invoice footer
        */
        String formatFooter();
    }

    public class InvoiceFormatter : IFormatter
    {
        private double total;
        public string formatHeader(Client client)
        {
            //print customer name
            //print cutomer address
            return client.FirstName + " " + client.LastName;
        }

        public string formatLineItem(InvoiceLineItem item)
        {
               total += item.GetTotalPrice();

               return String.Format(item.Item.GetDecription(), item.TotalPrice.ToString());
        }

        public string formatFooter()
        {
            throw new NotImplementedException();
        }
    }



    public class Order
    {
        public int OrderNo { get; set; }
        public int ClientID { get; set; }
        public DateTime DatePlaced { get; set; }
        public DateTime DatePromised { get; set; }
        public OrderStatus Status { get; set; }
        List<OrderLineItem> lineitems = null;

        public Order()
        {
            lineitems = new List<OrderLineItem>();
        }

    }

    public class OrderLineItem
    {
        public int OrderLineID { get; set; }
        public int OrderNo { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
    public enum OrderStatus
    {
        Canceled =0,
        Delivered = 1,
        Shipped = 2,
        PreparingShippment = 3,
        Open = 3
    }

    public enum InvoiceStatus
    {
        PaymentReceivedOnTime = 1,
        PaymentReceivedLate = 2,
        PaymentPending = 3,
        PaymentOverdue = 4,

    }

    public class OrderInvoiceMap
    {
        public int OrderNo { get; set; }
        public int InvoiceID { get; set; }
        public OrderInvoiceMap()
        {
        }
    }

}
