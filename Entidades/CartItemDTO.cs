using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Entidades
{
    public class CartItemDTO
    {

        [DataMember]
        [DisplayName("Id")]
        [Browsable(false)]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Cart Id")]
        public int CartId { get; set; }

        [DataMember]
        [DisplayName("Product Id")]
        public int ProductId { get; set; }
     
        [DataMember]
        [DisplayName("Title")]
        public string Title { get; set; }

        [DataMember]
        [DisplayName("Price")]
        public double Price { get; set; }

        [DataMember]
        [DisplayName("Quantity")]
        public int Quantity { get; set; }

          [DataMember]
        [DisplayName("Cart Date")]
        public DateTime CartDate { get; set; }

        [DataMember]
        [DisplayName("Item Count")]
        public int ItemCount { get; set; }
        
    }
}