
using System;
using System.Text.Json.Serialization;

namespace AcmeCorpCustomerAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public decimal Total { get; set; }

        public int CustomerId { get; set; }
        [JsonIgnore]
        public Customer Customer { get; set; }

    }
}
