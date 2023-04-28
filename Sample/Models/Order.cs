﻿namespace Sample.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal? Total { get; set; }
        public DateTime Date { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
