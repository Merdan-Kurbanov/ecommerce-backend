﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Koton.ECommerce.Core.DTOs
{
    public class GetProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; } 
        public decimal DiscountPrice { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Brand { get; set; }
    }
}
