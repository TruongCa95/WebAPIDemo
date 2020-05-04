using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Data.Entities
{
    [Table("Products")]
    public class Products
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }
        public double Price { get; set; }

    }
}
