﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Enitites
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set;}
        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; } // navegitional prop [one]

       // [ForeignKey("ProductType")]
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }// navegitional prop [one]
    }
}
