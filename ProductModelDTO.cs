using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiApiRestTest
{
    public class ProductModelDTO
    {
        public required string Name { get; set; }
        public decimal Price { get; set; }
    }
}