using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApiRestTest.Model
{
        public class ProductModel
    {

        public string Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}