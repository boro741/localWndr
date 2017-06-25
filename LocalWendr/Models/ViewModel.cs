using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalWendr.Models
{
    public class ViewModel
    {
        public decimal Seller_Id { get; set; }
        public decimal Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string ImageName { get; set; }
        public string Product_Description { get; set; }
        public Nullable<decimal> Product_Price { get; set; }
        public string Availability { get; set; }
        public string Category { get; set; }        
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Longitude { get; set; }
        public string Store_Name { get; set; }
        public string LandmarkName { get; set; }
        public string Location { get; set; }
    }
}